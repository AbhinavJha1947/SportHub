using SportHub.Core.Entities;
using SportHub.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportHub.Contracts.RepositoryInterfaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<IEnumerable<Player>> GetPlayersByTeamIdAsync(int teamId);
        Task<IEnumerable<Player>> GetPlayersByPositionAsync(Position position);
    }
}
