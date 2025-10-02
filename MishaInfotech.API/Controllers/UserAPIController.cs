using Microsoft.AspNetCore.Mvc;
using MishaInfotech.API.Models;
using MishaInfotech.API.Service;

namespace MishaInfotech.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserAPIController : ControllerBase
	{
		private readonly IUserService _userService;
		public UserAPIController(IUserService userService)
		{
			_userService = userService;
		}
		[HttpGet("GetUsers")]
		public async Task<ActionResult<List<User>>> GetUsers()
		{
			var Users = await _userService.GetUsers();
			return Ok(Users);
		}
		[HttpPost("CreateUsers")]
		public async Task<ActionResult<bool>> CreateUsers(User user)
		{
			var Users = await _userService.CreateUsers(user);
			return Ok(Users);
		}
		[HttpGet("GetStates")]
		public async Task<ActionResult<List<States>>> GetStates()
		{
			var States = await _userService.GetStates();
			return Ok(States);
		}
		[HttpGet("GetCities")]
		public async Task<ActionResult<List<Cities>>> GetCities(int StateId)
		{
			var Cities = await _userService.GetCities(StateId);
			return Ok(Cities);
		}
	}
}
