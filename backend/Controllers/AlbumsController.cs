using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs.AlbumDTO;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.Models;
using SieGraSieMa.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SieGraSieMa.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {

        private readonly IAlbumService _albumService;
        private readonly IMediaService _mediaService;
        private readonly UserManager<User> _userManager;
        private readonly ILogService _logService;


        public AlbumsController(IAlbumService albumService, IMediaService mediaService, UserManager<User> userManager, ILogService logService)
        {
            _albumService = albumService;
            _mediaService = mediaService;
            _userManager = userManager;
            _logService = logService;
        }

        [AllowAnonymous]
        [HttpGet()]
        public async Task<IActionResult> GetAlbums()
        {
            var albums = await _albumService.GetAlbums();

            return Ok(albums);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(int id)
        {
            var album = await _albumService.GetAlbum(id);

            if (album == null)
                return NotFound(new ResponseErrorDTO { Error = "Nie znaleziono albumu!" });

            return Ok(album);
        }


        //-------------------------------------------------admin functions


        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPost("{id}/media")]
        public async Task<IActionResult> AddMediaToAlbum(int id, IFormFile[] files)
        {
            try
            {
                var list = await _mediaService.CreateMedia(id, null, files, IMediaService.MediaTypeEnum.photos);

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Media added to album number {id}"));
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseErrorDTO { Error = ex.Message });
            }
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAlbum(UpdateAlbumDTO album, int id)
        {
            try
            {
                var newAlbum = new Album { CreateDate = album.CreateDate, Name = album.Name, TournamentId = album.TournamentId };
                var result = await _albumService.UpdateAlbum(id, newAlbum);
                if (!result) return BadRequest(new ResponseErrorDTO { Error = "Nie znaleziono albumu lub konkursu!" });

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Album {id} updated"));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseErrorDTO { Error = ex.Message });
            }
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            try
            {
                var result = await _albumService.DeleteAlbum(id);
                if (!result) return NotFound(new ResponseErrorDTO { Error = "Nie znaleziono albumu!" });

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Album {id} deleted"));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseErrorDTO { Error = ex.Message });
            }
        }
    }
}
