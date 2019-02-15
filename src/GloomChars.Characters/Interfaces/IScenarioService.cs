using System;
using Bearded.Monads;
using GloomChars.Characters.Models;

namespace GloomChars.Characters.Interfaces
{
    public interface IScenarioService
    {
        Either<int, string> NewScenario(Character character, string name);
        Option<ScenarioState> GetScenario(Character character);
        Either<int, string> CompleteScenario(Character character);
        Either<ScenarioState, string> UpdateStats(Character character, StatsUpdate statsUpdate);
        Either<ScenarioState, string> DrawCard(Character character);
        Either<ScenarioState, string> Reshuffle(Character character);
    }
}

