using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SieGraSieMa.DTOs.MediumDTO;
using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services.Medias
{
    public interface IMediaService
    {
        public Task<IEnumerable<Medium>> GetMedia();

        public Task<Medium> GetMedia(int id);

        public Task<bool> CreateMedia(Medium medium);

        public Task<bool> UpdateMedia(int id, Medium medium);

        public Task<bool> DeleteMedia(int id);
    }
    public class MediaService : IMediaService
    {
        private readonly SieGraSieMaContext _SieGraSieMaContext;

        public MediaService(SieGraSieMaContext SieGraSieMaContext)
        {
            _SieGraSieMaContext = SieGraSieMaContext;
        }
        public async Task<bool> CreateMedia(Medium medium)
        {
            await _SieGraSieMaContext.Media.AddAsync(medium);
            if (await _SieGraSieMaContext.SaveChangesAsync() > 0)
                return true;

            return false;
        }

        public async Task<bool> DeleteMedia(int id)
        {
            var medium = await _SieGraSieMaContext.Media.FindAsync(id);
            _SieGraSieMaContext.Media.Remove(medium);
            if (await _SieGraSieMaContext.SaveChangesAsync() > 0)
                return true;

            return false;
        }

        public async Task<IEnumerable<Medium>> GetMedia()
        {
            var media = await _SieGraSieMaContext.Media.ToListAsync();
            return media;
        }

        public async Task<Medium> GetMedia(int id)
        {
            var media = await _SieGraSieMaContext.Media.FindAsync(id);
            return media;
        }

        public async Task<bool> UpdateMedia(int id, Medium medium)
        {
            var oldMedia = await _SieGraSieMaContext.Media.FindAsync(id);
            if (oldMedia == null)
                return false;
            oldMedia.Url = medium.Url;
            oldMedia.AlbumId = medium.AlbumId;
            _SieGraSieMaContext.Media.Update(oldMedia);
            if (await _SieGraSieMaContext.SaveChangesAsync() > 0)
                return true;

            return false;
        }
    }
}
