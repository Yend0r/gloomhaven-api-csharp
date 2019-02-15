using System;
using Bearded.Monads;
using GloomChars.Characters.Models;

namespace GloomChars.Characters.Interfaces
{
    public interface IScenarioRepository
    {
        int CompleteActiveScenarios(int characterId);
        int InsertNewScenario(int characterId, string name, int characterHp);
        Option<(ScenarioInfo Info, ScenarioCharacterStats Stats)> GetScenario(int characterId);
        int UpdateCharacterStats(int scenarioId, ScenarioCharacterStats stats);
    }
}
