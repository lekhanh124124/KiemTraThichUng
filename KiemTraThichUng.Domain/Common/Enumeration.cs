// File: KiemTraThichUng.Domain/Common/Enumeration.cs
using KiemTraThichUng.Domain.Exceptions;
using System.Reflection;

namespace KiemTraThichUng.Domain.Common
{
    public abstract class Enumeration : IComparable
    {
        public int Id { get; }
        public string Name { get; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public override bool Equals(object? obj)
        {
            if (obj is not Enumeration other)
                return false;

            return GetType() == obj.GetType() && Id == other.Id;
        }

        public override int GetHashCode()
            => HashCode.Combine(Id, GetType());

        public int CompareTo(object? other)
            => Id.CompareTo(((Enumeration)other!).Id);

        public static IEnumerable<T> GetAll<T>()
            where T : Enumeration
        {
            var fields = typeof(T)
                .GetFields(BindingFlags.Public |
                           BindingFlags.Static |
                           BindingFlags.DeclaredOnly);

            return fields
                .Select(f => f.GetValue(null))
                .Cast<T>();
        }

        public static T FromId<T>(
            int id,
            Func<int, Exception>? exceptionFactory = null)
            where T : Enumeration
        {
            var matchingItem = GetAll<T>()
                .FirstOrDefault(item => item.Id == id);

            if (matchingItem is null)
            {
                if (exceptionFactory is not null)
                    throw exceptionFactory(id);

                throw new DomainValidationException(
                    $"'{id}' không phải giá trị hợp lệ của {typeof(T).Name}");
            }

            return matchingItem;
        }

        public static T FromName<T>(string name)
            where T : Enumeration
        {
            var matchingItem = GetAll<T>()
                .FirstOrDefault(item =>
                    string.Equals(item.Name, name,
                        StringComparison.OrdinalIgnoreCase));

            if (matchingItem is null)
                throw new DomainValidationException(
                    $"'{name}' không phải giá trị hợp lệ của {typeof(T).Name}");

            return matchingItem;
        }
    }
}
