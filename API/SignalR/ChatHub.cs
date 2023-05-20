using Application.Comments;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendComment(Create.Command command)
        {
            var comment = await _mediator.Send(command);
            
            await Clients.Group(command.PollId.ToString())
                .SendAsync("ReceiveComment", comment.Value);
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext != null)
            {
                var pollId = httpContext.Request.Query["pollId"];
                await Groups.AddToGroupAsync(Context.ConnectionId, pollId);
                var result = await _mediator.Send(new List.Query{PollId = Guid.Parse(pollId)});
                await Clients.Caller.SendAsync("LoadComments", result.Value);
            }
        }
    }
}

