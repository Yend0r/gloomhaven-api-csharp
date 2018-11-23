using System;
using System.Collections.Generic;
using Bearded.Monads;
using GloomChars.Characters.Models;

namespace GloomChars.Characters.Interfaces
{
    public interface ICharactersService
    {
        Option<Character> GetCharacter(int characterId, int userId);
        List<CharacterListItem> GetCharacters(int userId);

        Either<int, string> InsertNewCharacter(NewCharacter newCharacter);
        Either<int, string> UpdateCharacter(CharacterUpdate characterUpdate);
        Either<int, string> DeleteCharacter(int characterId, int userId);
    }
}
