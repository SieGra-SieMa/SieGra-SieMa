﻿using Microsoft.AspNetCore.Http;
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

        public Task<bool> CreateMedia(RequestMediumDTO mediumDTO);

        public Task<bool> UpdateMedia(int id, RequestMediumDTO mediumDTO);

        public Task<bool> DeleteMedia(int id);
        public Task<MediumInAlbum> AddToAlbum(MediumInAlbum mediumInAlbum);
        public Task<bool> DeleteFromAlbum(int mediaId, int albumId);
    }
    public class MediaService : IMediaService
    {
        private readonly SieGraSieMaContext _SieGraSieMaContext;

        public MediaService(SieGraSieMaContext SieGraSieMaContext)
        {
            _SieGraSieMaContext = SieGraSieMaContext;
        }

        public async Task<bool> CreateMedia(RequestMediumDTO mediumDTO)
        {
            Medium medium = new (){ Url = mediumDTO.Url };
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
            var media = await _SieGraSieMaContext.Media.Include(m => m.MediumInAlbums).ThenInclude(m => m.Album).ToListAsync();
            return media;
        }

        public async Task<Medium> GetMedia(int id)
        {
            var media = await _SieGraSieMaContext.Media.Include(m => m.MediumInAlbums).ThenInclude(m=>m.Album).SingleOrDefaultAsync(m => m.Id == id);
            return media;
        }

        public async Task<bool> UpdateMedia(int id, RequestMediumDTO mediumDTO)
        {
            var oldMedia = await _SieGraSieMaContext.Media.FindAsync(id);
            if (oldMedia == null)
                return false;
            oldMedia.Url = mediumDTO.Url;
            _SieGraSieMaContext.Media.Update(oldMedia);
            if (await _SieGraSieMaContext.SaveChangesAsync() > 0)
                return true;

            return false;
        }

        public async Task<MediumInAlbum> AddToAlbum(MediumInAlbum mediumInAlbum)
        {
            var medium = await _SieGraSieMaContext.Media.FindAsync(mediumInAlbum.MediumId);
            var album = await _SieGraSieMaContext.Media.FindAsync(mediumInAlbum.AlbumId);
            if (medium == null || album == null)
            {
                throw new Exception("Medium or album does not exist");
            }
            else if((await _SieGraSieMaContext.MediumInAlbum.FindAsync(mediumInAlbum.MediumId, mediumInAlbum.AlbumId)) != null)
            {
                throw new Exception("Medium already added to this album");
            }
            await _SieGraSieMaContext.MediumInAlbum.AddAsync(mediumInAlbum);
            await _SieGraSieMaContext.SaveChangesAsync();
            return mediumInAlbum;
        }
        public async Task<bool> DeleteFromAlbum(int mediaId, int albumId)
        {
            var mediumInAlbum = await _SieGraSieMaContext.MediumInAlbum.FindAsync(mediaId, albumId);
            if (mediumInAlbum == null)
            {
                throw new Exception("Medium doesnt belong to this album");
            }

            _SieGraSieMaContext.MediumInAlbum.Remove(mediumInAlbum);
            return await _SieGraSieMaContext.SaveChangesAsync()>0;
        }


    }
}