using Microsoft.EntityFrameworkCore;
using SportHub.Contracts.RepositoryInterfaces;
using SportHub.Core.Entities;
using SportHub.Core.Enums;
using SportHub.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportHub.Data.Repositories
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        private readonly SportHubDbContext _context;
        private readonly DbSet<Player> _dbSet;

        public PlayerRepository(SportHubDbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Player>();
        }

        public async Task<IEnumerable<Player>> GetPlayersByTeamIdAsync(int teamId)
        {
            return await _dbSet
                .Where(p => p.TeamId == teamId)
                .Include(p => p.Team)
                .ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayersByPositionAsync(Position position)
        {
            return await _dbSet
                .Where(p => p.Position == position)
                .Include(p => p.Team)
                .ToListAsync();
        }

        // Override base methods to include related data
        public override async Task<IEnumerable<Player>> GetAllAsync()
        {
            return await _dbSet
                .Include(p => p.Team)
                .ToListAsync();
        }

        // Override GetByIdAsync to include Team data
        public override async Task<Player> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}