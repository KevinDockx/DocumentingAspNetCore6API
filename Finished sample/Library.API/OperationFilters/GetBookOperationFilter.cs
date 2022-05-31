using Library.API.Models;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Library.API.OperationFilters
{
    public class GetBookOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation,
            OperationFilterContext context)
        {
            if (operation.OperationId != "GetBook")
            {
                return;
            }
            if (operation.Responses.Any(response =>
                response.Key == StatusCodes.Status200OK.ToString()))
            {
                var schema = context.SchemaGenerator.GenerateSchema(
                    typeof(BookWithConcatenatedAuthorName),
                    context.SchemaRepository);

                operation.Responses[StatusCodes.Status200OK.ToString()]
                    .Content.Add(
                        "application/vnd.marvin.bookwithconcatenatedauthorname+json",
                        new OpenApiMediaType() { Schema = schema });

            }
        }
    }
}