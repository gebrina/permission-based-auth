using FluentValidation.Results;

namespace Auth.Api.Utils;

public class FormatErrorMessage
{
    public static IDictionary<string, List<string>> Generate(List<ValidationFailure> errors)
    {
        var formattedErrors = new Dictionary<string, List<string>>();
        foreach (var error in errors)
        {

            if (!formattedErrors.TryGetValue(error.PropertyName, out List<string>? value))
            {
                formattedErrors.Add(error.PropertyName, [error.ErrorMessage]);
            }
            else
            {
                value.Add(error.ErrorMessage);
            }
        }

        return formattedErrors;
    }
}