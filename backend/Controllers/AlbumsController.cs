using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs.AlbumDTO;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.Models;
using SieGraSieMa.Services.Albums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {

        private readonly IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
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
                return NotFound(new ResponseErrorDTO { Error = "Album not found" });

            return Ok(album);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAlbum(RequestAlbumDTO album)
        {
            var result = await _albumService.CreateAlbum(new Album { CreateDate = album.CreateDate, Name = album.Name, TournamentId = album.TournamentId });

            if (!result)
                return BadRequest(new ResponseErrorDTO { Error = "Bad request" });

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAlbum(RequestAlbumDTO album, int id)
        {
            var newAlbum = new Models.Album { CreateDate = album.CreateDate, Name = album.Name, TournamentId = album.TournamentId };

            var result = await _albumService.UpdateAlbum(id, newAlbum);
            if (!result)
                return BadRequest(new ResponseErrorDTO { Error = "Bad request" });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var result = await _albumService.DeleteAlbum(id);

            if (!result)
                return NotFound(new ResponseErrorDTO { Error = "Album not found" });

            return Ok(result);
        }
    }
}
