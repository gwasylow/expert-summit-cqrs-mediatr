using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatrCQRS.Interfaces;
using MediatrCQRS.Logic;
using MediatrCQRS.ViewModels;

namespace MediatrCQRS.Commands
{
    public static class AddToDoCommand
    {
        // Command
        public record Command(string Name) : IRequest<CQRSCommandResponse>;


        // Validator
        // Handles all DOMAIN validation
        // Does NOT ensure request is formed correctly (E.G. required fields are filled out), that should be in the API layer.
        public class Validator : IValidationHandler<Command>
        {
            private readonly IToDoRepository _toDoRepository;

            public Validator(IToDoRepository toDoRepository)
            {
                _toDoRepository = toDoRepository;
            }

            public async Task<ValidationResult> Validate(Command request)
            {
                var itemFound = _toDoRepository.GetByName(request.Name);

                if (itemFound != null)
                    return ValidationResult.Fail($"Validation error - Todo with `{request.Name}` already exists.");

                return ValidationResult.Success;
            }
        }

        // Handler
        public class Handler : IRequestHandler<Command, CQRSCommandResponse>
        {
            private readonly IToDoRepository _toDoRepository;

            public Handler(IToDoRepository toDoRepository)
            {
                _toDoRepository = toDoRepository;
            }

            public async Task<CQRSCommandResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var id = _toDoRepository.SaveNote(new ToDoViewModel { Name = request.Name, Completed = false });

                if(id >0)
                    return new CQRSCommandResponse { StatusCode= HttpStatusCode.OK, ReturnedId = id };

                return new CQRSCommandResponse { StatusCode = HttpStatusCode.NotFound, ErrorMessage = "Could not save ToDo Item" };
            }
        }
    }
}
