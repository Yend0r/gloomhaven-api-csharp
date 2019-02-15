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
    public class CharactersReadRepository : ICharactersReadRepository
    {
        readonly string _connStr;
        readonly IGameDataService _gameData;

        public CharactersReadRepository(IOptions<DatabaseConfig> dbConfig, IGameDataService gameData)
        {
            _connStr = dbConfig.Value.ConnectionString;
            _gameData = gameData;
        }

        public Option<Character> GetCharacter(int characterId, int userId)
        {
            string sql = @"
                SELECT id        AS Id,
                    user_id      AS UserId,
                    name         AS Name,
                    class_name   AS ClassName,
                    experience   AS Experience,
                    gold         AS Gold,
                    achievements AS Achievements
                FROM characters
                WHERE id = @id
                    AND user_id = @userId;
    
                SELECT perk_id AS PerkId,
                    quantity   AS Quantity
                FROM character_perks
                WHERE character_id = @id
                ORDER BY perk_id         
                ";
            
            (IEnumerable<Character> ch, IEnumerable<DbCharacterPerk> dbPerks) =
                DapperUtils.QueryMulti2<Character, DbCharacterPerk>(
                    _connStr, 
                    sql, 
                    new { Id = characterId, UserId = userId }
                );

            if (ch.Any())
            {
                var character = ch.First();

                //Map the perks
                var allPerks = _gameData.GetGloomClass(character.ClassName).Perks;
                character.ClaimedPerks = dbPerks.Select(p => ToPerk(allPerks, p))
                                                .Where(p => p.IsSome)
                                                .Select(p => p.ForceValue())
                                                .ToList();

                return character;
            }

            return Option<Character>.None;
        }

        public List<CharacterListItem> GetCharacters(int userId)
        {
            string sql = @"
                SELECT id        As Id,
                    name         AS Name,
                    class_name   AS ClassName,
                    experience   AS Experience,
                    gold         AS Gold
                FROM characters
                WHERE user_id = @userId
                ORDER BY name 
                ";
            
            return DapperUtils.Query<CharacterListItem>(_connStr, sql, new { UserId = userId });
        }

        private Option<Perk> ToPerk(List<Perk> allPerks, DbCharacterPerk dbPerk)
        {
            var filteredPerks = allPerks.Where(p => String.Equals(p.Id, dbPerk.PerkId, StringComparison.OrdinalIgnoreCase));
            if (filteredPerks.Any())
            {
                return allPerks.First();
            }
            return Option<Perk>.None;
        }
    }
}
