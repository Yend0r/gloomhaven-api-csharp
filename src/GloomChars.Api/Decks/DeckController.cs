using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bearded.Monads;
using GloomChars.Characters.Interfaces;
using GloomChars.Api.Authentication;
using GloomChars.Characters.Models;
using GloomChars.Api.Errors;

namespace GloomChars.Api.Decks
{
    [Route("characters")]
    [ApiController]
    public class DeckController : ControllerBase
    {
        readonly ICharactersService _characterSvc;
        readonly IDeckService _deckSvc;
        readonly IUserManager _userManager;

        public DeckController(ICharactersService characterSvc, IDeckService deckSvc, IUserManager userManager)
        {
            _characterSvc = characterSvc;
            _deckSvc = deckSvc;
            _userManager = userManager;
        }

        [HttpGet("{characterId}/decks")]
        [Authorize]
        public ActionResult<DeckViewModel> GetDeck(int characterId)
        {
            return GetCharacter(characterId)
                    .Map(c => _deckSvc.GetDeck(c))
                    .Map(d => new DeckViewModel(d))
                    .Unify<DeckViewModel, IApiError, ActionResult<DeckViewModel>>(
                        x => x, 
                        e => e.ToActionResult()
                    );
        }

        [HttpPost("{characterId}/decks")]
        [Authorize]
        public ActionResult<DeckViewModel> DeckAction(int characterId, DeckActionRequest deckActionRequest)
        {
            if (deckActionRequest.Action.ToUpper() == "DRAW")
            {
                //Draw a card
                return GetCharacter(characterId)
                        .Map(c => _deckSvc.DrawCard(c))
                        .Map(d => new DeckViewModel(d))
                        .Unify<DeckViewModel, IApiError, ActionResult<DeckViewModel>>(
                            x => x, 
                            e => e.ToActionResult()
                        );
                
            }
            else if (deckActionRequest.Action.ToUpper() == "RESHUFFLE")
            {
                //Reshuffle
                return GetCharacter(characterId)
                        .Map(c => _deckSvc.Reshuffle(c))
                        .Map(d => new DeckViewModel(d))
                        .Unify<DeckViewModel, IApiError, ActionResult<DeckViewModel>>(
                            x => x, 
                            e => e.ToActionResult()
                        );
            }

            return new BadRequestError("Invalid action", "Action must be 'draw' or 'reshuffle'").ToActionResult();
        }

        private Either<Character, IApiError> GetCharacter(int characterId)
        {
            return 
                from user in _userManager.GetCurrentUser(this.User)  
                from ch   in _characterSvc.GetCharacter(characterId, user.Id)
                                          .AsEither<Character, IApiError>(new NotFoundError())
                select ch;
        }
    }
}
