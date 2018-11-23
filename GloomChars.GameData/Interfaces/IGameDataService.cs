using System;
using System.Collections.Generic;
using Bearded.Monads;
using GloomChars.GameData.Models;

namespace GloomChars.GameData.Interfaces
{
    public interface IGameDataService
    {
        GloomClass GetGloomClass(GloomClassName name);        
        Option<GloomClass> GetGloomClass(string name);    
        List<GloomClass> GloomClasses();
    }
}
