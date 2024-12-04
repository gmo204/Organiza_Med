using eAgenda.WebApi.Config.AutomapperConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NoteKeeper.Dominio.Compartilhado;
using OrganizaMed.Aplicacao.Medico;
using OrganizaMed.Dominio.ModuloAtividade;
using OrganizaMed.Dominio.ModuloMedico;
using OrganizaMed.Infra.Orm.Compartilhado;
using OrganizaMed.InfraOrm.ModuloAtividade;
using OrganizaMed.InfraOrm.ModuloMedico;
using OrganizaMed.WebApi.Config.Mappiing;
using OrganizaMed.WebApi.Config.Mapping;
using OrganizaMed.WebApi.Filters;
using Serilog;
using static AtividadeProfile;

namespace OrganizaMed.WebApi
{
    public static class DepedencyInjection
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["SQL_SERVER_CONNECTION_STRING"];

            services.AddDbContext<IContextoPersistencia, OrganizaMedDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(connectionString, dbOptions =>
                {
                    dbOptions.EnableRetryOnFailure();
                });
            });
        }

        public static void ConfigureCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositorioMedico, RepositorioMedicoOrm>();
            services.AddScoped<ServicoMedico>();
            
            services.AddScoped<IRepositorioAtividade, RepositorioAtividadeOrm>();
            services.AddScoped<ServicoAtividade>();

            services.AddScoped<FormsAtividadeMappingAction>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services) 
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<MedicoProfile>();
                config.AddProfile<AtividadeProfile>();
                config.AddProfile<UsuarioProfile>();
            });

            services.AddTransient<UsuarioResolver>();
            services.AddTransient<FormsAtividadeMappingAction>();
        }

        public static void ConfigureCors(this IServiceCollection services, string politica)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: politica, policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }


        public static void ConfigureSerilog(this IServiceCollection services, ILoggingBuilder logging)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            logging.ClearProviders();

            services.AddLogging(builder => builder.AddSerilog(dispose: true));
        }

        public static void ConfigureControllersWithFilters(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<ResponseWrapperFilter>();
            });
        }

        public static void ConfigureSwaggerAuthorization(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "organiza-med-api", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Por favor informe o token no padrão {Bearer token}",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
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
                        new string[]{}
                    }
                });
            });
        }
    }
}
