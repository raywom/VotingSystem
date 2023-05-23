using Application.VotingSystem;
using Application.Core;
using Application.Polls;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PollsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetPolls([FromQuery] PollParams param)
        {
            return HandlePagedResult(await Mediator.Send(new List.Query { Params = param }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoll(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoll(Poll poll)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Poll = poll }));
        }

        [Authorize(Policy = "IsPollHost")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, Poll poll)
        {
            poll.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Poll = poll }));
        }

        [Authorize(Policy = "IsPollHost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [HttpPost("{id}/attend")]
        public async Task<IActionResult> Attend(Guid id)
        {
            return HandleResult(await Mediator.Send(new UpdateVote.Command { Id = id }));
        }
    }
}