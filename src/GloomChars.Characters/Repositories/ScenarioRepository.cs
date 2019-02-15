using System;
using Bearded.Monads;
using GloomChars.Characters.Interfaces;
using GloomChars.Characters.Models;
using GloomChars.Common;
using GloomChars.Common.Config;
using Microsoft.Extensions.Options;
using GloomChars.Characters.Repositories.Models;

namespace GloomChars.Characters.Repositories
{
    public class ScenarioRepository : IScenarioRepository
    {
        readonly string _connStr;

        public ScenarioRepository(IOptions<DatabaseConfig> dbConfig)
        {
            _connStr = dbConfig.Value.ConnectionString;
        }

        public int CompleteActiveScenarios(int characterId)
        {
            string sql = @"
                UPDATE scenarios 
                SET date_completed = @dateCompleted,
                    is_active = false
                WHERE character_id = @character_id 
                    AND is_active = true;
                ";

            var param = new 
            {           
                CharacterId    = characterId,
                DateCompleted = DateTime.Now   
            };
            return DapperUtils.Execute(_connStr, sql, param);
        }

        public Option<(ScenarioInfo Info, ScenarioCharacterStats Stats)> GetScenario(int characterId)
        {
            string sql = @"
                SELECT  id              AS Id, 
                        character_id    AS CharacterId, 
                        name            AS Name,
                        health          AS Health, 
                        max_health      AS MaxHealth, 
                        experience      AS Experience, 
                        date_started    AS DateStarted, 
                        date_last_event AS DateLastEvent 
                FROM scenarios 
                WHERE character_id = @characterId 
                    AND is_active = true
                ";

            return DapperUtils.SingleQuery<DbScenario>(_connStr, sql, new { CharacterId = characterId })
                              .Map(MapToOutput);            
        }

        public int InsertNewScenario(int characterId, string name, int characterHp)
        {
            string sql = @"
                INSERT INTO scenarios
                    (is_active, 
                    character_id, 
                    name, 
                    health, 
                    max_health, 
                    experience, 
                    date_started, 
                    date_last_event)
                VALUES 
                    (true,
                    @characterId, 
                    @name, 
                    @health, 
                    @maxHealth, 
                    0, 
                    @dateStarted, 
                    @dateLastEvent)
                ";
            
            var param = new 
            {           
                CharacterId   = characterId,
                Name          = name,
                Health        = characterHp,
                MaxHealth     = characterHp,
                DateStarted   = DateTime.Now,   
                DateLastEvent = DateTime.Now   
            };
            return DapperUtils.Execute(_connStr, sql, param);
        }

        public int UpdateCharacterStats(int scenarioId, ScenarioCharacterStats stats)
        {
            string sql = @"
                UPDATE scenarios 
                SET health          = @health,
                    experience      = @experience,
                    date_last_event = @dateLastEvent
                WHERE id = @scenarioId;
                ";

            var param = new 
            {           
                ScenarioId    = scenarioId,
                Health        = stats.Health,
                Experience    = stats.Experience, 
                DateLastEvent = DateTime.Now   
            };
            return DapperUtils.Execute(_connStr, sql, param);
        }
        
        private (ScenarioInfo Info, ScenarioCharacterStats Stats) MapToOutput(DbScenario dbScenario)
        {
            var info = new ScenarioInfo
            {
                Id            = dbScenario.Id,            
                CharacterId   = dbScenario.CharacterId,   
                Name          = dbScenario.Name,   
                MaxHealth     = dbScenario.MaxHealth,   
                DateStarted   = dbScenario.DateStarted,   
                DateLastEvent = dbScenario.DateLastEvent   
            };

            var stats = new ScenarioCharacterStats
            {
                Health     = dbScenario.Health,   
                Experience = dbScenario.Experience   
            };

            return (info, stats);
        }        
    }
}
