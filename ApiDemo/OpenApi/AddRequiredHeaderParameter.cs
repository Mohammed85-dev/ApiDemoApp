using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiDemo.OpenApi;

public class RequiredHeadersFromAttributesFilter : IOperationFilter, IDocumentFilter {
    // Store discovered header names so we can register schemes in the document filter
    private static readonly HashSet<string> _discoveredHeaders = new();

    // This runs after all operations are processed — add the security schemes here
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context) {
        swaggerDoc.Components ??= new OpenApiComponents();
        swaggerDoc.Components.SecuritySchemes ??= new Dictionary<string, OpenApiSecurityScheme>();

        foreach (var headerName in _discoveredHeaders.Where(headerName => !swaggerDoc.Components.SecuritySchemes.ContainsKey(headerName)))
            swaggerDoc.Components.SecuritySchemes[headerName] = new OpenApiSecurityScheme {
                Name = headerName,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = $"Value for the {headerName} header",
            };
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context) {
        operation.Parameters ??= new List<OpenApiParameter>();

        foreach (var parameter in context.MethodInfo.GetParameters()) {
            var fromHeaderAttr = parameter
                .GetCustomAttributes(typeof(FromHeaderAttribute), false)
                .Cast<FromHeaderAttribute>()
                .FirstOrDefault();

            if (fromHeaderAttr == null) continue;
            var headerName = fromHeaderAttr.Name ?? parameter.Name!;
            _discoveredHeaders.Add(headerName);

            // operation.Parameters.Add(new OpenApiParameter
            // {
            //     Name = headerName,
            //     In = ParameterLocation.Header,
            //     Required = true,
            //     Schema = new OpenApiSchema
            //     {
            //         Type = "string"
            //     }
            // });

            operation.Security ??= new List<OpenApiSecurityRequirement>();

            var scheme = new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = headerName,
                },
            };

            operation.Security.Add(new OpenApiSecurityRequirement {
                [scheme] = new List<string>(),
            });
        }
    }
}