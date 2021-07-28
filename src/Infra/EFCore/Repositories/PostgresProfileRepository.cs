using dotnetServer.Domain.Models;
using dotnetServer.Domain.DTOs.ProfileDTO;
using dotnetServer.Domain.Respositories;
using System.Collections.Generic;
using BCryptNet = BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;

namespace dotnetServer.Infra.EFCore
{
    public class PostgresProfileRepository: IProfileRepository
    {
        private DataContext _dataContext;
        public PostgresProfileRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public async Task<Profile> Create(CreateProfileDTO profileDTO){
            var profile = (await _dataContext.Profiles.AddAsync(profileDTO.ToProfile())).Entity;
            _dataContext.SaveChanges();

            return profile;
        }

        public async Task<IEnumerable<Profile>> FindAll(){
            var profiles = await _dataContext.Profiles.ToListAsync();

            return profiles;
        }

        public async Task<Profile> Find(Expression<Func<Profile, bool>> predicate){
            var profile = await _dataContext.Profiles.SingleOrDefaultAsync(predicate);

            return profile;
        }

    }
}