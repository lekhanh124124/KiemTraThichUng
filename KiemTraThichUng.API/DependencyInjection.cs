// File: KiemTraThichUng.API/DependencyInjection.cs
using KiemTraThichUng.Application;
using KiemTraThichUng.Application.Common.Exceptions;
using KiemTraThichUng.Application.Common.Responses;
using KiemTraThichUng.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace KiemTraThichUng.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddApplicationDI();
            services.AddInfrastructureDI(configuration);

            services.AddCorsPolicy();
            services.AddJwtAuthentication(configuration);
            services.AddSwaggerWithJwt();

            return services;
        }

        // ---------------- CORS ----------------

        private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });

            return services;
        }

        // ---------------- JWT ----------------

        private static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtKey = configuration["Jwt:Key"]
                ?? throw new ConflictException("Jwt:Key is missing");

            var issuer = configuration["Jwt:Issuer"]
                ?? throw new ConflictException("Jwt:Issuer is missing");

            var audience = configuration["Jwt:Audience"]
                ?? throw new ConflictException("Jwt:Audience is missing");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse(); 

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        var response = ApiResponse<object>.Failure(new List<ApiMessage>
                        {
                            new("UNAUTHORIZED", "Bạn chưa đăng nhập hoặc token không hợp lệ.")
                        });

                        await context.Response.WriteAsJsonAsync(response);
                    },

                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var response = ApiResponse<object>.Failure(new List<ApiMessage>
                        {
                            new("FORBIDDEN", "Bạn không có quyền truy cập tài nguyên này.")
                        });

                        await context.Response.WriteAsJsonAsync(response);
                    }
                };
            });

            return services;
        }

        // ---------------- Swagger ----------------

        private static IServiceCollection AddSwaggerWithJwt(
            this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Format: Bearer {your token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                options.CustomSchemaIds(type => GetCustomSchemaId(type));

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "KiemTraThichUng API",
                    Version = "v1",
                    Description = "API documentation for Adaptive Testing System"
                });
            });

            return services;
        }

        // ---------------- Schema Id ----------------

        private static string GetCustomSchemaId(Type type)
        {
            if (type.IsGenericType)
            {
                var genericName = type.Name.Split('`')[0];
                var genericArguments = string.Join(
                    "And",
                    type.GetGenericArguments().Select(GetCustomSchemaId));

                return $"{genericName}.{genericArguments}";
            }

            var fullName = type.FullName ?? string.Empty;
            var parts = fullName.Split('.');

            if (parts.Length >= 3)
            {
                var className = type.Name.Replace("+", "_");
                var parentNamespace = parts[^2];

                if (parentNamespace == "DTOs" && parts.Length >= 4)
                    parentNamespace = parts[^3];

                return $"{parentNamespace}.{className}";
            }

            return type.Name;
        }
    }
}