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
            var Users = await _db.Users.ToListAsync();
            return Users;
        }
        public async Task<List<States>> GetStates()
        {
            List<States> States = new List<States>();
            try
            {
                States = await _db.States.ToListAsync();
            }
            catch
            {

            }
            return States;
        }
        public async Task<List<Cities>> GetCities(int stateId)
        {
            List<Cities> Cities = new List<Cities>();

            try
            {
                Cities = await _db.Cities.Where(c => c.StateId == stateId).ToListAsync();
            }
            catch
            {

            }
            return Cities;
        }
    }
}
