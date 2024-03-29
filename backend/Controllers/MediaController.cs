﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SieGraSieMa.DTOs;
using SieGraSieMa.DTOs.AlbumDTO;
using SieGraSieMa.DTOs.ErrorDTO;
using SieGraSieMa.DTOs.MediumDTO;
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
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        private readonly UserManager<User> _userManager;
        private readonly ILogService _logService;
        public MediaController(IMediaService mediaService, UserManager<User> userManager, ILogService logService)
        {
            _mediaService = mediaService;
            _userManager = userManager;
            _logService = logService;
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

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedium(int id)
        {
            try
            {
                var result = await _mediaService.DeleteMedia(id);
                if (!result) return NotFound(new ResponseErrorDTO { Error = "Nie znaleziono mediów!" });

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Medium with id {id} deleted"));
                return Ok(new MessageDTO { Message = "Usunięto media!" });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpPost("{id}/{albumId}")]
        public async Task<IActionResult> AddToAlbum(int id, int albumId)
        {
            try
            {
                var result = await _mediaService.AddToAlbum(new MediumInAlbum { MediumId = id, AlbumId = albumId });

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Medium with id {id} added to album with id {albumId}"));
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }

        [Authorize(Policy = "OnlyAdminAuthenticated")]
        [HttpDelete("{id}/{albumId}")]
        public async Task<IActionResult> DeleteFromAlbum(int id, int albumId)
        {
            try
            {
                var result = await _mediaService.DeleteFromAlbum(id, albumId);

                var email = HttpContext.User.FindFirst(e => e.Type == ClaimTypes.Name)?.Value;
                var user = await _userManager.FindByEmailAsync(email);
                await _logService.AddLog(new Log(user, $"Mediumwith id {id} deleted from album with id {albumId}"));
                return Ok(new MessageDTO { Message = "Zdjęcie usunięte z albumu!" });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseErrorDTO { Error = e.Message });
            }
        }
    }
}
