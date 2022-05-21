using Microsoft.EntityFrameworkCore;
using SieGraSieMa.DTOs.AlbumDTO;
using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services.Albums
{
    public interface IAlbumService
    {
        public Task<IEnumerable<ResponseAlbumDTO>> GetAlbums();

        public Task<ResponseAlbumDTO> GetAlbum(int id);

        public Task<bool> CreateAlbum(Album album);

        public Task<bool> UpdateAlbum(int id, Album album);

        public Task<bool> DeleteAlbum(int id);
    }

    public class AlbumService : IAlbumService
    {
        private readonly SieGraSieMaContext _SieGraSieMaContext;

        public AlbumService(SieGraSieMaContext SieGraSieMaContext)
        {
            _SieGraSieMaContext = SieGraSieMaContext;
        }
        public async Task<bool> CreateAlbum(Album album)
        {
            var tournament = await _SieGraSieMaContext.Tournaments.FindAsync(album.TournamentId);
            if (tournament == null)
                return false;

            await _SieGraSieMaContext.Albums.AddAsync(album);
            if (await _SieGraSieMaContext.SaveChangesAsync() > 0)
                return true;

            return false;
        }

        public async Task<bool> DeleteAlbum(int id)
        {
            var tournament = await _SieGraSieMaContext.Albums.FindAsync(id);
            _SieGraSieMaContext.Albums.Remove(tournament);
            if (await _SieGraSieMaContext.SaveChangesAsync() > 0)
                return true;

            return false;
        }

        public async Task<ResponseAlbumDTO> GetAlbum(int id)
        {
            var album = await _SieGraSieMaContext.Albums.FindAsync(id);
            if (album == null)
                return null;
                
            return new ResponseAlbumDTO { Id = album.Id, Name = album.Name, CreateDate = album.CreateDate, TournamentId = album.TournamentId};
        }

        public async Task<IEnumerable<ResponseAlbumDTO>> GetAlbums()
        {
            var albums = await _SieGraSieMaContext.Albums.Select(a => new ResponseAlbumDTO { Id = a.Id, Name = a.Name, CreateDate = a.CreateDate, TournamentId = a.TournamentId }).ToListAsync();

            return albums;
        }

        public async Task<bool> UpdateAlbum(int id, Album album)
        {
            var tournament = await _SieGraSieMaContext.Tournaments.FindAsync(album.TournamentId);
            if (tournament == null)
                return false;
            var oldAlbum = await _SieGraSieMaContext.Albums.FindAsync(id);
            if (oldAlbum == null)
                return false;
            oldAlbum.Name = album.Name;
            oldAlbum.CreateDate = album.CreateDate;
            oldAlbum.TournamentId = album.TournamentId;
            _SieGraSieMaContext.Albums.Update(oldAlbum);
            if (await _SieGraSieMaContext.SaveChangesAsync() > 0)
                return true;

            return false;
        }
    }
}
