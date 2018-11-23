using System;
using Bearded.Monads;
using GloomChars.Characters.Models;

namespace GloomChars.Characters.Interfaces
{
    public interface ICharactersEditRepository
    {
        Either<int, string> InsertNewCharacter(NewCharacter newCharacter);
        Either<int, string> UpdateCharacter(CharacterUpdate characterUpdate);
        int DeleteCharacter(int characterId, int userId);
    }
}
