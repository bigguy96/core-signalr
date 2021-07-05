using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR.Data;
using SignalR.Hubs;

namespace SignalR.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IHubContext<UserHub> _hub;

        public UsersController(DatabaseContext context, IHubContext<UserHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<User>> GetOne(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null)
        //    {
        //        await _hub.Clients.All.SendAsync("ReceiveMessage", $"User: {id}", "not found");
        //        return NotFound();
        //    }

        //    await _hub.Clients.All.SendAsync("ReceiveMessage", $"User: {id}", "found");
        //    return user;
        //}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await _context.Users.ToListAsync();

            await _hub.Clients.All.SendAsync("ReceiveMessage", "GetAll", "was called");

            return Ok(users);
        }

        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[HttpPost]
        //public async Task<ActionResult> Add(User user)
        //{
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    await _hub.Clients.All.SendAsync("ReceiveMessage", $"User: {user.Id}", "was created");

        //    return CreatedAtAction("GetOne", new { id = user.Id }, user);
        //}
    }
}