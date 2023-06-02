using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var errors = new List<string>();
            foreach (var item in modelState)
            {
                var erros = item.Value.Errors.Select(e => e.ErrorMessage);
                errors.AddRange(erros);
            }
            return errors;
        }
    }
}
