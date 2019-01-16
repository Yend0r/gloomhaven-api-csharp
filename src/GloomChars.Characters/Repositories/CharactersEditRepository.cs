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

namespace GloomChars.Characters.Repositories
{
    public class CharactersEditRepository : ICharactersEditRepository
    {
        readonly string _connStr;

        public CharactersEditRepository(IOptions<DatabaseConfig> dbConfig)
        {
            _connStr = dbConfig.Value.ConnectionString;
        }

        public Either<int, string> InsertNewCharacter(NewCharacter newCharacter)
        {
            string sql = @"
                INSERT INTO characters
                    (user_id, 
                    name, 
                    class_name, 
                    experience, 
                    gold, 
                    achievements,
                    date_created,
                    date_updated)
                VALUES 
                    (@userId, 
                    @name, 
                    @className, 
                    0, 
                    0, 
                    0,
                    @dateCreated,
                    @dateUpdated)
                RETURNING id
                ";
            
            var param = new DbNewCharacter(newCharacter);
            return DapperUtils.TryExecuteScalar(_connStr, sql, param, "You already have a character with that name and class.");
        }

        public Either<int, string> UpdateCharacter(CharacterUpdate characterUpdate)
        {
            string sql = @"
                UPDATE characters 
                SET name         = @name, 
                    experience   = @experience, 
                    gold         = @gold, 
                    achievements = @achievements, 
                    date_updated = @dateUpdated
                WHERE id = @id 
                    AND user_id = @userId
                ";
            
            var param = new DbCharacterUpdate(characterUpdate);
            var constraintError = "You already have a character with that name and class.";
            var result = DapperUtils.TryExecute(_connStr, sql, param, constraintError);   
            if (result.IsSuccess)
            {
                UpdatePerks(characterUpdate.Id, characterUpdate.Perks);
            }

            return result;
        }

        public int DeleteCharacter(int characterId, int userId)
        {
            string sql = @"
                DELETE FROM characters
                WHERE user_id = @userId
                    AND id = @characterId;
    
                DELETE FROM character_perks
                WHERE character_id = @characterId;
                ";
            
            var param = new { CharacterId = characterId, UserId = userId };
            return DapperUtils.Execute(_connStr, sql, param);
        }
        
        private void UpdatePerks(int characterId, List<PerkUpdate> perks)
        {
            DeletePerks(characterId);
            InsertPerks(characterId, perks);
        }

        private void InsertPerks(int characterId, List<PerkUpdate> perks)
        {
            string sql = @"
                INSERT INTO character_perks
                    (character_id, perk_id, quantity)
                VALUES 
                    (@characterId, @perkId, @quantity)
                ";
            
            var param = perks
                        .Select(p => new 
                        { 
                            CharacterId = characterId, 
                            PerkId = p.Id, 
                            Quantity = p.Quantity 
                        })
                        .ToArray();
            DapperUtils.Execute(_connStr, sql, param);
        }

        private void DeletePerks(int characterId)
        {
            string sql = @"
                DELETE FROM character_perks
                WHERE character_id = @id;
                ";

            DapperUtils.Execute(_connStr, sql, new { Id = characterId });
        }
    }
}
