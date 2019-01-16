using System;
using System.Linq;
using System.Collections.Generic;
using Bearded.Monads;
using GloomChars.Characters.Interfaces;
using GloomChars.Characters.Models;
using GloomChars.Characters.Repositories.Models;
using GloomChars.Common;
using GloomChars.Common.Config;
using Microsoft.Extensions.Options;
using GloomChars.GameData.Models;
using GloomChars.GameData.Interfaces;

namespace GloomChars.Characters.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        readonly string _connStr;
        readonly IGameDataService _gameData;

        public DeckRepository(IOptions<DatabaseConfig> dbConfig, IGameDataService gameData)
        {
            _connStr = dbConfig.Value.ConnectionString;
            _gameData = gameData;
        }

        public List<ModifierCard> GetDiscards(int characterId)
        {
            string sql = @"
                SELECT id              AS Id, 
                    damage             AS Damage, 
                    draw_another       AS DrawAnother, 
                    reshuffle          AS Reshuffle, 
                    card_action        AS Action, 
                    card_action_amount AS ActionAmount
                FROM modifier_discards 
                WHERE character_id = @characterId
                ORDER BY date_discarded DESC
                ";

            return
                DapperUtils.Query<DbModifierCard>(_connStr, sql, new { CharacterId = characterId })
                           .Select(c => ToModifierCard(c))
                           .ToList();
        }

        public int InsertDiscard(int characterId, ModifierCard card)
        {
            string sql = @"
                INSERT INTO modifier_discards
                    (damage, 
                    draw_another, 
                    reshuffle, 
                    card_action, 
                    card_action_amount, 
                    character_id, 
                    date_discarded)
                VALUES 
                    (@damage, 
                    @drawAnother, 
                    @reshuffle, 
                    @action, 
                    @actionAmount, 
                    @characterId, 
                    @dateDiscarded)
            ";
            
            var param = new DbNewModifierCard(characterId, card);
            return DapperUtils.Execute(_connStr, sql, param);
        }

        public int DeleteDiscards(int characterId)
        {
            string sql = @"
                DELETE FROM modifier_discards 
                WHERE character_id = @characterId;
                ";
            
            var param = new { CharacterId = characterId };
            return DapperUtils.Execute(_connStr, sql, param);
        }

        private ModifierCard ToModifierCard(DbModifierCard c)
            => new ModifierCard
            {
                Damage = c.Damage,
                DrawAnother = c.DrawAnother,
                Reshuffle = c.Reshuffle,
                ActionAmount = c.ActionAmount,
                Action = Enum.Parse<CardAction>(c.Action, true)
            };
    }
}
