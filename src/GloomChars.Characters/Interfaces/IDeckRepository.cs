using System;
using System.Collections.Generic;
using GloomChars.GameData.Models;

namespace GloomChars.Characters.Interfaces
{
    public interface IDeckRepository
    {
        List<ModifierCard> GetDiscards(int characterId);
        int InsertDiscard(int characterId, ModifierCard card);
        int DeleteDiscards(int characterId);
    }
}
