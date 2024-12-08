using System.ComponentModel.DataAnnotations;

namespace Api.Domain.RequestMessages;

public class RequestMessageBase
{
    public string Validate()
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(this);
        bool isValid = Validator.TryValidateObject(this, context, results, true);

        if (!isValid)
        {
            return results?.FirstOrDefault()?.ErrorMessage;
        }

        return null;
    }
}