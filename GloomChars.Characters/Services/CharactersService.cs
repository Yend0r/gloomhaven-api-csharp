using System;
using System.Linq;
using System.Collections.Generic;
using Bearded.Monads;
using GloomChars.Characters.Interfaces;
using GloomChars.Characters.Models;

namespace GloomChars.Characters.Services
{
    public class CharactersService : ICharactersService
    {
        readonly ICharactersReadRepository _readRepo;
        readonly ICharactersEditRepository _editRepo;

        public CharactersService(ICharactersReadRepository readRepo, ICharactersEditRepository editRepo)
        {
            _readRepo = readRepo;
            _editRepo = editRepo;
        }

        public Option<Character> GetCharacter(int characterId, int userId)
        {
            return _readRepo.GetCharacter(characterId, userId);
        }

        public List<CharacterListItem> GetCharacters(int userId)
        {
            return _readRepo.GetCharacters(userId);
        }

        public Either<int, string> InsertNewCharacter(NewCharacter newCharacter)
        {
            return _editRepo.InsertNewCharacter(newCharacter);
        }

        public Either<int, string> UpdateCharacter(CharacterUpdate characterUpdate)
        {
            //First must get the character to make sure that this user owns the character
            var character = _readRepo.GetCharacter(characterUpdate.Id, characterUpdate.UserId);
            if (character.IsSome)
            {
                return _editRepo.UpdateCharacter(characterUpdate);
            }
            return "Character not found.";
        }
        
        public Either<int, string> DeleteCharacter(int characterId, int userId)
        {
            //First must get the character to make sure that this user owns the character
            var character = _readRepo.GetCharacter(characterId, userId);
            if (character.IsSome)
            {
                return _editRepo.DeleteCharacter(characterId, userId);
            }
            return "Character not found.";
        }
    }
}
