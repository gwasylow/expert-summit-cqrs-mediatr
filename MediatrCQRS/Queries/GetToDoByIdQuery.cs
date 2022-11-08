using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatrCQRS.Interfaces;
using MediatrCQRS.Logic;

namespace MediatrCQRS.Queries
{
    public static class GetToDoByIdQuery
    {
        //Query/Command
        //All data we would like to execute
        //***We do not use the caching mechanism in here***
        public class Query : IRequest<CQRSQueryResponse<Response>>
        {
            public int Id { get; }

            public Query(int id)
            {
                Id = id;
            }
        }


        //Handler:
        //Business logic to execute -> returns a RESPONSE
        public class Handler : IRequestHandler<Query, CQRSQueryResponse<Response>>
        {
            private readonly IToDoRepository _toDoRepository;

            public Handler(IToDoRepository toDoRepository)
            {
                _toDoRepository = toDoRepository;
            }

            public async Task<CQRSQueryResponse<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                var todo = _toDoRepository.GetAll().FirstOrDefault(x => x.Id == request.Id);

                return todo == null ?
                    new CQRSQueryResponse<Response> {
                        StatusCode = HttpStatusCode.NotFound, ErrorMessage = $"To do with id {request.Id} not found", QueryResult = null
                    } :
                    new CQRSQueryResponse<Response> {
                        StatusCode = HttpStatusCode.OK, QueryResult = new Response { Id = todo.Id, Name = todo.Name, Completed = todo.Completed }
                    };
            }
        }

        //Reposne
        public class Response
        {
            public int Id { get; init; }
            public string Name { get; init; }
            public bool Completed { get; init; }
        }
    }
}
