using System;
using System.Threading.Tasks;
using MediatrCQRS.Logic;

namespace MediatrCQRS.Interfaces
{
    public interface IValidationHandler { }

    public interface IValidationHandler<T> : IValidationHandler
    {
        Task<ValidationResult> Validate(T request);
    }
}
