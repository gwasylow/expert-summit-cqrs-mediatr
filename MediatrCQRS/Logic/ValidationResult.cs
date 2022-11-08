using System;
namespace MediatrCQRS.Logic
{
    public class ValidationResult
    {
        public bool IsSuccessful { get; set; } = true;
        public string Error { get; init; }

        public static ValidationResult Success => new ValidationResult();
        public static ValidationResult Fail(string error) => new ValidationResult { IsSuccessful = false, Error = error };
    }
}
