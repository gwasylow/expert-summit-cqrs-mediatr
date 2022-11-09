using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatrCQRS.Commands;
using MediatrCQRS.Logic.Notifications;
using MediatrCQRS.Queries;
using MediatrCQRS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MediatrCQRS.Controllers
{
    //TODO: #8 CQRS and MediatR usage in Controller        
    [ApiController]
    [Route("[controller]/[action]")]
    public class ToDoController : ControllerBase
    {
        private readonly IMediator _mediator;

        //Dependency Injection
        public ToDoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> ListAllToDoCached()
        {
            var response = await _mediator.Send(new GetAllToDoCachedQuery.Query());

            if (response != null)
                return Ok(response);

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetToDoById(int id)
        {
            var response = await _mediator.Send(new GetToDoByIdQuery.Query(id));

            if (response != null)
                return Ok(response);

            return NotFound();
        }

        //WebApi: CORS ON/OFF
        //Web Application: Antiforgery Token when passing the form data         
        [HttpPost]       
        public async Task<IActionResult> AddToDo(ToDoViewModel toDoViewModel)
        {
            if (ModelState.IsValid && Request.IsHttps)
                if (toDoViewModel.Validate_NoteIsNew())
                    return Ok(await _mediator.Send(new AddToDoCommand.Command(Name: toDoViewModel.Name)));

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Alarm()
        {
            var elapsedSleepTime = 1000;

            for (int i = 0; i < 30; i++)
            {
                Thread.Sleep(elapsedSleepTime);
                await _mediator.Publish(new AlarmNotification(i) { Message = $"Alarm raised from ToDoController" });
            }

            return Ok();
        }
    }
}
