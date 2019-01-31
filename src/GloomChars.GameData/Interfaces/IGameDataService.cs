using System;
using System.Collections.Generic;
using Bearded.Monads;
using GloomChars.GameData.Models;

namespace GloomChars.GameData.Interfaces
{
    public interface IGameDataService
    {
        List<int> XPLevels { get; }
        int GetCharacterLevel(int xp);
        int GetCharacterHP(GloomClass gloomClass, int xp);
        int? GetCharacterPetHP(GloomClass gloomClass, int xp);
        GloomClass GetGloomClass(GloomClassName name);        
        Option<GloomClass> GetGloomClass(string name);    
        List<GloomClass> GloomClasses();
    }
}
