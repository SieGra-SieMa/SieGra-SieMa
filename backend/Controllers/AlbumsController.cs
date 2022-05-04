using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs.ErrorDTO;
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
                return NotFound(new ResponseErrorDTO { Error = "Tournament not found" });

            return Ok(album);
        }

        /*[HttpPost()]
        public async Task<IActionResult> CreateAlbum(RequestAlbumDTO album)
        {
            //var newTournament = _mapper.Map<Tournament>(tournament);
            var newTournament = new Tournament { Name = tournament.Name, StartDate = tournament.StartDate, EndDate = tournament.EndDate, Description = tournament.Description, Address = tournament.Address };
            var result = await _tournamentsService.CreateTournament(newTournament);

            if (!result)
                return BadRequest(new ResponseErrorDTO { Error = "Bad request" });

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAlbum(RequestTournamentDTO tournament, int id)
        {
            var newTournament = new Tournament { Name = tournament.Name, StartDate = tournament.StartDate, EndDate = tournament.EndDate, Description = tournament.Description, Address = tournament.Address };

            var result = await _tournamentsService.UpdateTournament(id, newTournament);

            if (!result)
                return BadRequest(new ResponseErrorDTO { Error = "Bad request" });

            return Ok(result);
        }*/

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
