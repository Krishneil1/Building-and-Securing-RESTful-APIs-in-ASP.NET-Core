﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LandonApi.Controllers
{
    [Route("/")]
    [ApiController]//Adds extra features like automatic validation
    public class RootController : ControllerBase
    {
        [HttpGet(Name =nameof(GetRoot))]
        [ProducesResponseType(200)]
        public IActionResult GetRoot()// Iaction helps in returning JSON result
        {
            var response = new
            {
                href = Url.Link(nameof(GetRoot),null),
                rooms = new
                {
                    href = Url.Link(nameof(RoomsController.GetRooms),null)
                },
                info = new
                {
                    href = Url.Link(nameof(InfoController.GetInfo), null)
                }


            };
            return Ok(response);
        }
    }
}