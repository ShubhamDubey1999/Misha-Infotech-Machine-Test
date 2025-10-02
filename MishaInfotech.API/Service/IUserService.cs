using Microsoft.AspNetCore.Mvc;
using MishaInfotech.API.Models;

namespace MishaInfotech.API.Service
{
	public interface IUserService
	{
		Task<List<User>> GetUsers();
		Task<bool> CreateUsers(User user);
		Task<List<States>> GetStates();
		Task<List<Cities>> GetCities(int stateId);
	}
}
