using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Library.API
{
#nullable disable
    public static class CustomConventions
    {
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ApiConventionNameMatch(
            ApiConventionNameMatchBehavior.Prefix)]
        public static void Insert(
            [ApiConventionNameMatch(
                ApiConventionNameMatchBehavior.Any)]
            [ApiConventionTypeMatch(
                ApiConventionTypeMatchBehavior.Any)] 
            object model)
        { }
    }
#nullable restore
}
