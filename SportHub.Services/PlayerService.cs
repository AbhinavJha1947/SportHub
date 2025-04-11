using AutoMapper;
using SportHub.Contracts.DTOs;
using SportHub.Contracts.RepositoryInterfaces;
using SportHub.Contracts.ServiceInterfaces;
using SportHub.Core.Entities;
using SportHub.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportHub.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public PlayerService(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerDto>> GetAllPlayersAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PlayerDto>>(players);
        }

        public async Task<PlayerDto> GetPlayerByIdAsync(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            return _mapper.Map<PlayerDto>(player);
        }

        public async Task<PlayerDto> CreatePlayerAsync(CreatePlayerDto createPlayerDto)
        {
            var player = _mapper.Map<Player>(createPlayerDto);
            var addedPlayer = await _playerRepository.AddAsync(player);
            return _mapper.Map<PlayerDto>(addedPlayer);
        }

        public async Task<PlayerDto> UpdatePlayerAsync(int id, UpdatePlayerDto updatePlayerDto)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if (player == null)
                return null;

            _mapper.Map(updatePlayerDto, player);
            var updatedPlayer = await _playerRepository.UpdateAsync(player);
            return _mapper.Map<PlayerDto>(updatedPlayer);
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            return await _playerRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PlayerDto>> GetPlayersByTeamIdAsync(int teamId)
        {
            var players = await _playerRepository.GetPlayersByTeamIdAsync(teamId);
            return _mapper.Map<IEnumerable<PlayerDto>>(players);
        }

        public async Task<IEnumerable<PlayerDto>> GetPlayersByPositionAsync(Position position)
        {
            var players = await _playerRepository.GetPlayersByPositionAsync(position);
            return _mapper.Map<IEnumerable<PlayerDto>>(players);
        }
    }
}
