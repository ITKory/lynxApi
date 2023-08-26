using Application.Requests.Commands;
using Application.Requests.Queries;
using Application.Requests.QueryHandlers;
using Domain.Models;
using lynxApi.Abstractions;
using MediatR;

namespace lynxApi.EndpointDefinition
{
    public class RequestEndpointDefinition : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var request = app.MapGroup("api/request");
            request.MapGet("/get",GetFullInfo);
            request.MapGet("/get/all",GetAll);
            request.MapPut("/update",UpdateStatus);
            request.MapPost("/add",AddRequest);
            request.MapDelete("/remove",DeleteRequest);
        }

        private async Task<IResult> GetFullInfo(IMediator mediator, int requestId)
        {
            var getRequest = new GetFullRequestInfo() { RequestId = requestId };
            return await mediator.Send(getRequest);
         
        }      
        private async Task<IResult> GetAll(IMediator mediator )
        {
            var getRequest = new GetAllRequests()  ;
            return await mediator.Send(getRequest);
         
        }
        private async Task<IResult> AddRequest(IMediator mediator, SearchRequest searchRequest)
        {
            var createRequest = new CreateRequestCommand() { SearchRequest = searchRequest };
            return await mediator.Send(createRequest);
        }
        private async Task<IResult> UpdateStatus(IMediator mediator, ListItemModel listItem)
        {
            var updateRequest = new UpdateRequestStatus() { request = listItem };
            return await mediator.Send(updateRequest);
        }
        private async Task<IResult> DeleteRequest(IMediator mediator, int requestId)
        {
            var removeRequest = new RemoveRequestCommand() { RequestId = requestId };
            return await mediator.Send(removeRequest);
        }
    }
}
