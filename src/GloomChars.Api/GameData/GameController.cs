using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bearded.Monads;
using GloomChars.GameData.Interfaces;

namespace GloomChars.Api.GameData
{
    [Route("game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        readonly IGameDataService _gameSvc;

        public GameController(IGameDataService gameSvc)
        {
            _gameSvc = gameSvc;
        }

        [HttpGet("classes")]
        [Authorize]
        public ActionResult<IEnumerable<GloomClassViewModel>> ListClasses()
        {
            return Ok(_gameSvc.GloomClasses().Select(c => new GloomClassViewModel(_gameSvc.XPLevels, c)));
        }

        [HttpGet("classes/{name}")]
        [ProducesResponseType(200, Type = typeof(GloomClassViewModel))]
        [ProducesResponseType(404)]
        [Authorize]
        public IActionResult GetClass(string name)
        {
            return _gameSvc.GetGloomClass(name).AsEither("Class not found.")
                   .Map(c => new GloomClassViewModel(_gameSvc.XPLevels, c))
                   .Unify<GloomClassViewModel, string, IActionResult>(Ok, NotFound);
        }
    }
}
