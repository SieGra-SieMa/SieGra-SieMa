using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs.AlbumDTO;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.MediumDTO;
using SieGraSieMa.Models;
using SieGraSieMa.Services.Medias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }


        [AllowAnonymous]
        [HttpGet()]
        public async Task<IActionResult> GetMedia()
        {
            var media = await _mediaService.GetMedia();

            return Ok(media);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedium(int id)
        {
            var medium = await _mediaService.GetMedia(id);

            if (medium == null)
                return NotFound(new ResponseErrorDTO { Error = "Medium not found" });

            return Ok(medium);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateMedium(RequestMediumDTO medium)
        {
            var result = await _mediaService.CreateMedia(medium);

            if (!result)
                return BadRequest(new ResponseErrorDTO { Error = "Bad request" });

            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateMedium(RequestMediumDTO medium, int id)
        {
            //var newMedium = new Medium { Url = medium.Url, AlbumId = medium.AlbumId };

            var result = await _mediaService.UpdateMedia(id, medium);
            if (!result)
                return BadRequest(new ResponseErrorDTO { Error = "Bad request" });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedium(int id)
        {
            var result = await _mediaService.DeleteMedia(id);

            if (!result)
                return NotFound(new ResponseErrorDTO { Error = "Medium not found" });

            return Ok();
        }
    }
}
