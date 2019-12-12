using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace RMBTestRestAPI.Installers
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //Setup Swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "RMB Test Rest API",
                        Version = "v1",
                        Description = "This page lists all the rest apis for RMB Test",
                        TermsOfService = "None",
                        Contact = new Contact
                        {
                            Name = "Ibikunle .A Adeoluwa",
                            Email = "dev.ibikunle@gmail.com",
                            Url = "https://www.linkedin.com/in/ibikunle-adeoluwa"
                        },
                        License = new License
                        {
                            Name = "Use under MIT Licence",
                            Url = "https://github.com/dotnet/core/blob/master/LICENSE.TXT"
                        }
                    });
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[0] }
                };
                x.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization Header using bearer scheme",
                    Name = "Authorization",
                    In = "Header",
                    Type = "apiKey"
                });
                x.AddSecurityRequirement(security);

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);
            });
        }
    }
}
