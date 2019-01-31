using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bearded.Monads;
using GloomChars.Characters.Interfaces;
using GloomChars.Api.Authentication;
using GloomChars.Characters.Models;
using GloomChars.Api.Errors;
using GloomChars.GameData.Interfaces;

namespace GloomChars.Api.Characters
{
    [Route("characters")]
    [ApiController]
    public class CharactersReadController : ControllerBase
    {
        readonly ICharactersService _characterSvc;
        readonly IGameDataService _gameSvc;
        readonly IUserManager _userManager;

        public CharactersReadController(ICharactersService characterSvc, IGameDataService gameSvc, IUserManager userManager)
        {
            _characterSvc = characterSvc;
            _gameSvc = gameSvc;
            _userManager = userManager;
        }

        [HttpGet("")]
        [Authorize]
        public ActionResult<List<CharacterListModel>> ListCharacters()
        {
            return _userManager.GetCurrentUser(this.User)
                    .Map(u => _characterSvc.GetCharacters(u.Id))
                    .Map(cList => cList.Select(ToCharacterListModel).ToList())
                    .Unify<List<CharacterListModel>, IApiError, ActionResult<List<CharacterListModel>>>(
                        x => x, 
                        e => e.ToActionResult()
                    );
        }

        [HttpGet("{characterId}")]
        [Authorize]
        public ActionResult<CharacterViewModel> GetCharacter(int characterId)
        {
            var character = 
                from user      in _userManager.GetCurrentUser(this.User)  
                from viewModel in GetCharacter(characterId, user.Id)
                select viewModel;

            return character
                    .Unify<CharacterViewModel, IApiError, ActionResult<CharacterViewModel>>(
                        x => x,
                        e => e.ToActionResult() 
                    );
        }

        private Either<CharacterViewModel, IApiError> GetCharacter(int characterId, int userId)
        {
            return _characterSvc.GetCharacter(characterId, userId)
                                .AsEither<Character, IApiError>(new NotFoundError())
                                .Map(ToCharacterViewModel);
        }

        private CharacterListModel ToCharacterListModel(CharacterListItem c)
        {
            var gloomClass = _gameSvc.GetGloomClass(c.ClassName);
            var level = _gameSvc.GetCharacterLevel(c.Experience);

            return new CharacterListModel(level, c);
        }

        private CharacterViewModel ToCharacterViewModel(Character c)
        {
            var gloomClass = _gameSvc.GetGloomClass(c.ClassName);
            var level = _gameSvc.GetCharacterLevel(c.Experience);
            var hp = _gameSvc.GetCharacterHP(gloomClass, c.Experience);
            var petHP = _gameSvc.GetCharacterPetHP(gloomClass, c.Experience);

            return new CharacterViewModel(level, hp, petHP, c);
        }
    }
}