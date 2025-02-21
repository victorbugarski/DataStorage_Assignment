using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class UserRepository : BaseRepository<UserEntity>, IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}
