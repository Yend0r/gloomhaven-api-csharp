using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bearded.Monads;
using GloomChars.Characters.Interfaces;
using GloomChars.Api.Authentication;
using GloomChars.Characters.Models;
using GloomChars.Api.Errors;

namespace GloomChars.Api.Scenarios
{
    [Route("characters")]
    [ApiController]
    public class ScenarioController : ControllerBase
    {
        readonly ICharactersService _characterSvc;
        readonly IScenarioService _scenarioSvc;
        readonly IUserManager _userManager;

        public ScenarioController(ICharactersService characterSvc, IScenarioService scenarioSvc, IUserManager userManager)
        {
            _characterSvc = characterSvc;
            _scenarioSvc = scenarioSvc;
            _userManager = userManager;
        }

        [HttpGet("{characterId}/scenarios")]
        [Authorize]
        public ActionResult<ScenarioViewModel> GetScenario(int characterId)
        {
            var result =
                from character in GetCharacter(characterId)
                from scenario  in GetScenarioViewModel(character)
                select scenario;
                
            return result
                    .Unify<ScenarioViewModel, IApiError, ActionResult<ScenarioViewModel>>(
                        x => x, 
                        e => e.ToActionResult()
                    );
        }

        [HttpDelete("{characterId}/scenarios")]
        [Authorize]
        public IActionResult CompleteScenario(int characterId)
        {
            var result =
                from character in GetCharacter(characterId)
                from complete  in CompleteScenario(character)
                select complete;
                
            return result
                    .Unify<int, IApiError, ActionResult>(
                        _ => NoContent(),  
                        e => e.ToActionResult()
                    );
        }

        [HttpPost("{characterId}/scenarios")]
        [Authorize]
        public ActionResult<ScenarioViewModel> NewScenario(int characterId, [FromBody] NewScenarioRequest newScenarioRequest)
        {
            var result =
                from character     in GetCharacter(characterId)
                from validScenario in ValidateNewScenario(newScenarioRequest)
                from newResult     in AddScenario(character, validScenario.Name)
                from scenario      in GetScenarioViewModel(character)
                select scenario;
                
            return result
                    .Unify<ScenarioViewModel, IApiError, ActionResult<ScenarioViewModel>>(
                        x => Created(ToScenarioUri(characterId), x),  
                        e => e.ToActionResult()
                    );
        }

        [HttpPatch("{characterId}/scenarios/stats")]
        [Authorize]
        public ActionResult<ScenarioViewModel> PatchScenarioStats(int characterId, [FromBody] StatsPatchRequest statsPatch)
        {
            var result =
                from character in GetCharacter(characterId)
                from udpate       in MapPatchToUpdate(statsPatch)
                from updateResult in UpdateStats(character, udpate)
                select updateResult;

            return result
                    .Map(s => new ScenarioViewModel(s))
                    .Unify<ScenarioViewModel, IApiError, ActionResult<ScenarioViewModel>>(
                        x => x,  
                        e => e.ToActionResult()
                    );
        }
        
        [HttpPost("{characterId}/scenarios/deck")]
        [Authorize]
        public ActionResult<ScenarioViewModel> DeckAction(int characterId, [FromBody] DeckActionRequest deckActionRequest)
        {
            Func<Character, Either<ScenarioState, IApiError>> handler = 
                (_) => Either<ScenarioState, IApiError>.CreateError(new BadRequestError("Invalid action", "Action must be 'draw' or 'reshuffle'."));

            if (deckActionRequest.Action.ToUpper() == "DRAW")
            {
                handler = (c) => DrawCard(c);
            }
            
            if (deckActionRequest.Action.ToUpper() == "RESHUFFLE")
            {
                handler = (c) => Reshuffle(c);
            }

            var result =
                from character in GetCharacter(characterId)
                from scenarioState in handler(character)
                select scenarioState;

            return result
                    .Map(s => new ScenarioViewModel(s))
                    .Unify<ScenarioViewModel, IApiError, ActionResult<ScenarioViewModel>>(
                        x => x, 
                        e => e.ToActionResult()
                    );
        }

        private Either<Character, IApiError> GetCharacter(int characterId)
        {
            return 
                from user in _userManager.GetCurrentUser(this.User)  
                from ch   in _characterSvc.GetCharacter(characterId, user.Id)
                                          .AsEither<Character, IApiError>(new NotFoundError())
                select ch;
        }

        private Either<ScenarioState, IApiError> GetScenario(Character character)
        {
            return _scenarioSvc.GetScenario(character)
                               .AsEither<ScenarioState, IApiError>(new NotFoundError());
        }

        private Either<ScenarioViewModel, IApiError> GetScenarioViewModel(Character character)
        {
            return GetScenario(character).Map(s => new ScenarioViewModel(s));
        }

        private Either<ScenarioState, IApiError> DrawCard(Character character)
        {
            return _scenarioSvc.DrawCard(character)                            
                               .MapError(e => (IApiError)new BadRequestError(e));
        }

        private Either<ScenarioState, IApiError> Reshuffle(Character character)
        {
            return _scenarioSvc.Reshuffle(character)
                               .MapError(e => (IApiError)new BadRequestError(e));
        }

        private Either<NewScenarioRequest, IApiError> ValidateNewScenario(NewScenarioRequest newScenario)
        {
            if (string.IsNullOrEmpty(newScenario.Name))
            {
                return Either<NewScenarioRequest, IApiError>.CreateError(new BadRequestError("Invalid name", "Scenario name is required."));
            }
            return newScenario;
        }

        private Either<int, IApiError> AddScenario(Character character, string name)
        {
            return _scenarioSvc.NewScenario(character, name)
                               .MapError(e => (IApiError)new BadRequestError(e)); 
        }

        private string ToScenarioUri(int characterId)
        {
            return $"{HttpContext.Request.Host.ToString()}/characters/{characterId}/scenarios";
        }
        
        private Either<StatsUpdate, IApiError> MapPatchToUpdate(StatsPatchRequest patch)
        {
            return new StatsUpdate { Health = patch.Health, Experience = patch.Experience };
        }

        private Either<ScenarioState, IApiError> UpdateStats(Character character, StatsUpdate update)
        {
            return _scenarioSvc.UpdateStats(character, update)
                               .MapError(e => (IApiError)new BadRequestError(e)); 
        }

        private Either<int, IApiError> CompleteScenario(Character character)
        {
            return _scenarioSvc.CompleteScenario(character)
                               .MapError(e => (IApiError)new BadRequestError(e)); 
        }
    }
}
