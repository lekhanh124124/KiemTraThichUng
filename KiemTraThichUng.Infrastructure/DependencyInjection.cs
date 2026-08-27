// File: KiemTraThichUng.Infrastructure/DependencyInjection.cs
using KiemTraThichUng.Application.Abstractions.Persistence;
using KiemTraThichUng.Application.Abstractions.Services;
using KiemTraThichUng.Domain.Options;
using KiemTraThichUng.Infrastructure.Identity;
using KiemTraThichUng.Infrastructure.Persistence;
using KiemTraThichUng.Infrastructure.Persistence.Repositories;
using KiemTraThichUng.Infrastructure.Services.AnswerEvaluatorService;
using KiemTraThichUng.Infrastructure.Services.UpdateTheta;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace KiemTraThichUng.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStringOptions>(configuration.GetSection(ConnectionStringOptions.SectionName));
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                var conn = provider
                    .GetRequiredService<IOptions<ConnectionStringOptions>>()
                    .Value.DefaultConnection;

                options.UseSqlServer(conn);
            });

            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());
            services.AddScoped<INganHangCauHoiRepository, NganHangCauHoiRepository>();
            services.AddScoped<ICauHinhDanhMucRepository, CauHinhDanhMucRepository>();
            services.AddScoped<IPhienKiemTraRepository, PhienKiemTraRepository>();
            services.AddScoped<ICauHinhDeKiemTraRepository, CauHinhDeKiemTraRepository>();

            services.AddScoped<IAnswerEvaluationService, AnswerEvaluationService>();
            services.AddScoped<IAnswerEvaluator, TracNghiemDonEvalutor>();
            services.AddScoped<IAnswerEvaluator, TracNghiemNhomEvalutor>();

            services.AddScoped<IUpdateThetaService, UpdateThetaService>();  

            return services;
        }
    }
}
