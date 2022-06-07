using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs;
using SieGraSieMa.DTOs.AlbumDTO;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.MediumDTO;
using SieGraSieMa.Models;
using SieGraSieMa.Services.Email;
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
        private readonly IEmailService _emailService;
        public MediaController(IMediaService mediaService, IEmailService emailService)
        {
            _mediaService = mediaService;
            _emailService = emailService;
        }


        [AllowAnonymous]
        [HttpGet()]
        public async Task<IActionResult> GetMedia()
        {
            var media = await _mediaService.GetMedia();
            //await _emailService.SendAsync("jan.biniek@gmail.com", "Logowanie dwuetapowe", "acb");

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

        /*[HttpPost()]
        public async Task<IActionResult> CreateMedium(IFormFile[] files)
        {
            try
            {
                var list = await _mediaService.CreateMedia(files);

                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(new ResponseErrorDTO { Error = ex.Message });
            }
        }*/

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
            try
            {
                var result = await _mediaService.DeleteMedia(id);

                if (!result)
                    return NotFound(new ResponseErrorDTO { Error = "Medium not found" });

                return Ok(new MessageDTO { Message = "Medium deleted!"});
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [HttpPost("{id}/{albumId}")]
        public async Task<IActionResult> AddToAlbum(int id, int albumId)
        {
            try
            {
                var result = await _mediaService.AddToAlbum(new MediumInAlbum { MediumId = id, AlbumId = albumId });
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [HttpDelete("{id}/{albumId}")]
        public async Task<IActionResult> DeleteFromAlbum(int id, int albumId)
        {
            try
            {
                var result = await _mediaService.DeleteFromAlbum(id, albumId);
                return Ok(new MessageDTO { Message = "Photo deleted from album" });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }
    }
}
