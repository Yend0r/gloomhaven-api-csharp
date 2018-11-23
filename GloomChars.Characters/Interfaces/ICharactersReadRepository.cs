using System;
using System.Collections.Generic;
using Bearded.Monads;
using GloomChars.Characters.Models;

namespace GloomChars.Characters.Interfaces
{
    public interface ICharactersReadRepository
    {
        Option<Character> GetCharacter(int characterId, int userId);
        List<CharacterListItem> GetCharacters(int userId);
    }
}
