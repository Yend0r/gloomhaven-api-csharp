﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bearded.Monads;
using GloomChars.Characters.Interfaces;
using GloomChars.Api.Authentication;
using GloomChars.Characters.Models;
using GloomChars.Api.Errors;
using GloomChars.GameData.Interfaces;
using GloomChars.GameData.Models;

namespace GloomChars.Api.Characters
{
    [Route("characters")]
    [ApiController]
    public class CharactersEditController : ControllerBase
    {
        readonly ICharactersService _characterSvc;
        readonly IGameDataService _gameSvc;
        readonly IUserManager _userManager;

        public CharactersEditController(ICharactersService characterSvc, IGameDataService gameSvc, IUserManager userManager)
        {
            _characterSvc = characterSvc;
            _gameSvc = gameSvc;
            _userManager = userManager;
        }

        [HttpPost("")]
        [Authorize]
        public IActionResult AddCharacter(NewCharacterRequest newCharacter)
        {
            var result =
                from user       in _userManager.GetCurrentUser(this.User)  
                from gloomClass in GetGloomClass(newCharacter.ClassName)
                from newChar    in ToNewCharacter(user.Id, newCharacter.Name, gloomClass.ClassName)
                from addResult  in AddCharacter(newChar)
                from character  in GetCharacter(addResult, user.Id)
                select character;

            return result
                    .Unify<CharacterViewModel, IApiError, ActionResult>(
                        x => Created(ToCharacterUri(x.Id), x),  
                        e => e.ToActionResult()
                    );
        }

        [HttpPost("")]
        [Authorize]
        public IActionResult UpdateCharacter([FromQuery] int characterId, [FromBody] CharacterUpdateRequest characterUpdate)
        {
            var result =
                from user         in _userManager.GetCurrentUser(this.User)  
                from udpate       in ToCharacterUpdate(user.Id, characterId, characterUpdate)
                from updateResult in UpdateCharacter(udpate)
                select updateResult;

            return result
                    .Unify<int, IApiError, ActionResult>(
                        x => NoContent(),  
                        e => e.ToActionResult()
                    );
        }
        
        private Either<GloomClass, IApiError> GetGloomClass(string className)
        {
            return _gameSvc.GetGloomClass(className)
                           .AsEither<GloomClass, IApiError>(new BadRequestError("Invalid class name", ""));
        }

        private Either<NewCharacter, IApiError> ToNewCharacter(int userId, string name, GloomClassName className)
        {
            return new NewCharacter
            {
                UserId = userId,
                Name = name,
                ClassName = className
            };    
        }

        private Either<int, IApiError> AddCharacter(NewCharacter newChar)
        {
            return _characterSvc.InsertNewCharacter(newChar)
                                .MapError(e => (IApiError)new BadRequestError(e));
        }

        private Either<CharacterViewModel, IApiError> GetCharacter(int characterId, int userId)
        {
            return _characterSvc.GetCharacter(characterId, userId)
                                .AsEither<Character, IApiError>(new NotFoundError())
                                .Map(c => new CharacterViewModel(c));
        }

        private string ToCharacterUri(int characterId)
        {
            return $"{HttpContext.Request.Host.ToString()}/characters/{characterId}";
        }

        private Either<CharacterUpdate, IApiError> ToCharacterUpdate(int userId, int characterId, CharacterUpdateRequest updateRequest)
        {
            return new CharacterUpdate
            {
                Id = characterId,
                UserId = userId,
                Name = updateRequest.Name,
                Experience = updateRequest.Experience,
                Gold = updateRequest.Gold,
                Achievements = updateRequest.Achievements,
                Perks = updateRequest.Perks.Select(p => new PerkUpdate { Id = p.Id, Quantity = p.Quantity }).ToList()
            };
        }

        private Either<int, IApiError> UpdateCharacter(CharacterUpdate update)
        {
            return _characterSvc.UpdateCharacter(update)
                                .MapError(e => (IApiError)new BadRequestError(e));
        }
    }
}
