﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Seges.TeamX.AspNetCore.Swagger
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDefaultSwagger<T>(this IServiceCollection services) where T: class
        {
            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                    // integrate xml comments
                    var xmlCommentsFile = XmlCommentsFilePath<T>();
                    if (xmlCommentsFile.Exists)
                    {
                        options.IncludeXmlComments(xmlCommentsFile.FullName);
                    }
                    //TODO: Log this

                });
            return services;
        }

        private static FileInfo XmlCommentsFilePath<T>()
        {
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var fileName = $"{typeof(T).GetTypeInfo().Assembly.GetName().Name}.xml";
            return new FileInfo(Path.Combine(basePath, fileName));
        }
    }
}
