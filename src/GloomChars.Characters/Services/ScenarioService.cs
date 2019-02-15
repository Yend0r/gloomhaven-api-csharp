using System;
using Bearded.Monads;
using GloomChars.Characters.Interfaces;
using GloomChars.Characters.Models;
using GloomChars.GameData.Interfaces;
using GloomChars.GameData.Models;

namespace GloomChars.Characters.Services
{
    public class ScenarioService : IScenarioService 
    {
        readonly IScenarioRepository _scenarioRepo;
        readonly IDeckService _deckSvc;
        readonly IGameDataService _gameData;

        public ScenarioService(IScenarioRepository scenarioRepo, IDeckService deckSvc, IGameDataService gameData)
        {
            _scenarioRepo = scenarioRepo;
            _deckSvc = deckSvc;
            _gameData = gameData;
        }

        public Option<ScenarioState> GetScenario(Character character)
        {
            return _scenarioRepo.GetScenario(character.Id)
                                .Map(s => new ScenarioState(s.Info, s.Stats, _deckSvc.GetDeck(character)));
        }

        public Either<int, string> NewScenario(Character character, string name)
        {
            GloomClass gloomClass = _gameData.GetGloomClass(character.ClassName);
            int characterHp = _gameData.GetCharacterHP(gloomClass, character.Experience);

            _deckSvc.Reshuffle(character);

            return _scenarioRepo.InsertNewScenario(character.Id, name, characterHp);
        }

        public Either<int, string> CompleteScenario(Character character)
        {
            var scenario = _scenarioRepo.GetScenario(character.Id);
            if (scenario.IsSome)
            {
                _deckSvc.Reshuffle(character);
                return _scenarioRepo.CompleteActiveScenarios(character.Id);
            }
            return "Scenario not found.";
        }

        public Either<ScenarioState, string> UpdateStats(Character character, StatsUpdate statsUpdate)
        {
            return GetScenario(character).AsEither("Scenario not found.")
                                         .Map(s => UpdateStats(statsUpdate, s));
        }

        public Either<ScenarioState, string> DrawCard(Character character)
        {
            return GetScenario(character).AsEither("Scenario not found.")
                                         .Map(s => DrawCard(character, s));
        }

        public Either<ScenarioState, string> Reshuffle(Character character)
        {
            return GetScenario(character).AsEither("Scenario not found.")
                                         .Map(s => Reshuffle(character, s));
        }

        private ScenarioState UpdateStats(StatsUpdate statsUpdate, ScenarioState scenario)
        {
            scenario.CharacterStats = MergeStats(statsUpdate, scenario);
            _scenarioRepo.UpdateCharacterStats(scenario.Info.Id, scenario.CharacterStats);
            return scenario;
        }

        private ScenarioCharacterStats MergeStats(StatsUpdate statsUpdate, ScenarioState scenario)
        {
            int max = scenario.Info.MaxHealth;
            ScenarioCharacterStats stats = scenario.CharacterStats;

            int newHealth = statsUpdate.Health ?? stats.Health;
            if (newHealth < 0)
            {
                newHealth = 0;
            }
            else if (newHealth > max)
            {
                newHealth = max;
            }

            int newXp = statsUpdate.Experience ?? stats.Experience;
            if (newXp < 0)
            {
                newXp = 0;
            }

            return new ScenarioCharacterStats { Health = newHealth, Experience = newXp };
        }

        private ScenarioState DrawCard(Character character, ScenarioState scenario)
        {
            scenario.ModifierDeck = _deckSvc.DrawCard(character);
            return scenario;
        }

        private ScenarioState Reshuffle(Character character, ScenarioState scenario)
        {
            scenario.ModifierDeck = _deckSvc.Reshuffle(character);
            return scenario;
        }
    }
}
