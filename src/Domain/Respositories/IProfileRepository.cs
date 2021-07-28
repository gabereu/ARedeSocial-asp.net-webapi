using System.Collections.Generic;
using dotnetServer.Domain.Models;
using dotnetServer.Domain.DTOs.ProfileDTO;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;

namespace dotnetServer.Domain.Respositories
{
    public interface IProfileRepository
    {
        Task<Profile> Create(CreateProfileDTO dto);
        Task<IEnumerable<Profile>> FindAll();
        // IEnumerable<Profile> FindAll(System.Predicate<Profile> match);
        Task<Profile> Find(Expression<Func<Profile, bool>> predicate);
    }
}