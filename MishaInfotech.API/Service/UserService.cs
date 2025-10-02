using Microsoft.EntityFrameworkCore;
using MishaInfotech.API.Data;
using MishaInfotech.API.Models;
using System.Collections.Generic;

namespace MishaInfotech.API.Service
{
    public class UserService : IUserService
    {
        private readonly DataContext _db;
        public UserService(DataContext dataContext)
        {
            _db = dataContext;
        }

        public async Task<bool> CreateUsers(User user)
        {
            try
            {
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<List<User>> GetUsers()
        {

            var users = (from u in _db.Users
                         join s in _db.States on u.StateId equals s.StateId
                         select new User
                         {
                             Id = u.Id,
                             Name = u.Name,
                             Email = u.Email,
                             Image = u.Image,
                             Gender = u.Gender,
                             StateName = s.Name
                         }).ToList();
            return users;
        }
        public async Task<List<States>> GetStates()
        {
            return await _db.States.ToListAsync();
        }
        public async Task<List<Cities>> GetCities(int stateId)
        {
            return await _db.Cities.Where(c => c.StateId == stateId).ToListAsync();
        }
    }
}

