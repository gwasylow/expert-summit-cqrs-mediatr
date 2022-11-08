using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatrCQRS.Interfaces;
using MediatrCQRS.Logic;
using MediatrCQRS.ViewModels;

namespace MediatrCQRS.Queries
{
    public class GetAllToDoCachedQuery
    {
        //Query/Command
        //All data we would like to execute

        public class Query : IRequest<CQRSQueryResponse<Response>>, ICacheable        
        {
            public string CacheKey => $"GetAllToDoQuery";

            public Query()
            {
            }
        }


        //Handler:pp=
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
                var todos = _toDoRepository.GetAll();

                return todos == null ?
                    new CQRSQueryResponse<Response> { StatusCode = HttpStatusCode.NotFound, ErrorMessage = "Not items found!", QueryResult = null } :
                    new CQRSQueryResponse<Response> { StatusCode = HttpStatusCode.OK, QueryResult = new Response(todos.ToList()) };                
            }
        }

        //Reposne:
        //The data we would like to return
        public class Response
        {
            public List<ToDoViewModel> ToDoList { get; } = new List<ToDoViewModel>();

            public Response(List<ToDoViewModel> toDoList)
            {
                ToDoList = toDoList;
            }
        }
    }
}
