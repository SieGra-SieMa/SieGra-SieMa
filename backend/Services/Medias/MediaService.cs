using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SieGraSieMa.DTOs.MediumDTO;
using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static SieGraSieMa.Services.Medias.IMediaService;

namespace SieGraSieMa.Services.Medias
{
    public interface IMediaService
    {
        public Task<IEnumerable<Medium>> GetMedia();
        public Task<Medium> GetMedia(int id);
        public Task<List<RequestMediumDTO>> CreateMedia(int? albumId, int? id, IFormFile[] files, MediaTypeEnum mediaType);
        public enum MediaTypeEnum { photos, teams, tournaments }
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

        public async Task<List<RequestMediumDTO>> CreateMedia(int? albumId, int? id, IFormFile[] files, MediaTypeEnum mediaType)
        {
            var year = DateTime.UtcNow.Year.ToString();
            var month = DateTime.UtcNow.Month.ToString();
            var list = new List<RequestMediumDTO>();

            var separator = OperatingSystem.IsWindows() ? '\\' : '/';

            foreach (var file in files)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    var result = mediaType switch
                    {
                        MediaTypeEnum.photos => $@"{year}{separator}{month}",
                        MediaTypeEnum.teams => $@"{id}",
                        MediaTypeEnum.tournaments => $@"{id}"
                    };

                    if (!Directory.Exists($@"wwwroot{separator}{mediaType}{separator}{result}"))
                        Directory.CreateDirectory($@"wwwroot{separator}{mediaType}{separator}{result}");
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), $@"wwwroot{separator}{mediaType}{separator}{result}", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    var absPath = new Uri($@"http://localhost:5000/{mediaType}/{result}/{fileName}").AbsolutePath;
                    list.Add(new RequestMediumDTO { Url = absPath });
                    var addedMedium = new Medium { Url = absPath };
                    _SieGraSieMaContext.Media.Add(addedMedium);
                    if(mediaType == MediaTypeEnum.photos)
                        _SieGraSieMaContext.MediumInAlbum.Add(new MediumInAlbum { AlbumId = (int)albumId, Medium = addedMedium });
                    if(mediaType == MediaTypeEnum.teams)
                    {
                        var team = await _SieGraSieMaContext.Teams.FindAsync(id);
                        team.Medium = addedMedium;
                        _SieGraSieMaContext.Update(team);
                    }
                    if (mediaType == MediaTypeEnum.tournaments)
                    {
                        var tournament = await _SieGraSieMaContext.Tournaments.FindAsync(id);
                        tournament.Medium = addedMedium;
                        _SieGraSieMaContext.Update(tournament);
                    }
                }
            }
            await _SieGraSieMaContext.SaveChangesAsync();
            return list;
        }

        
        public async Task<bool> DeleteMedia(int id)
        {
            var medium = await _SieGraSieMaContext.Media.FindAsync(id);
            if (medium == null)
                throw new Exception("Medium not found!");
 
            _SieGraSieMaContext.Media.Remove(medium);
            if (await _SieGraSieMaContext.SaveChangesAsync() > 0)
            {
                var absPath = $@"wwwroot\{medium.Url}";
                {
                    System.IO.File.Delete(absPath);
                }
                return true;
            }
                

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
            var mediumInAlbum = await _SieGraSieMaContext.MediumInAlbum.FindAsync(albumId, mediaId);
            if (mediumInAlbum == null)
                throw new Exception("Medium doesnt belong to this album");

            _SieGraSieMaContext.MediumInAlbum.Remove(mediumInAlbum);
            var medium = await _SieGraSieMaContext.Media.FindAsync(mediaId);
            if(medium != null && await _SieGraSieMaContext.MediumInAlbum.AnyAsync(m => m.MediumId == mediaId))
            {
                if (!_SieGraSieMaContext.Teams.Include(m => m.TeamInTournaments).ThenInclude(t => t.Tournament).GroupBy(m => m.MediumId).Any(m => m.Key == medium.Id))
                    await DeleteMedia(mediaId);
            }
            

            await _SieGraSieMaContext.SaveChangesAsync();

            return true;
        }


    }
}
