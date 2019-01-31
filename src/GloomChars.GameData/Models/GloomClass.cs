using System;
using System.Collections.Generic;

namespace GloomChars.GameData.Models
{
    public class GloomClass
    {
        public GloomClass() 
        { 
            this.Perks = new List<Perk>();
            this.HPLevels = new List<int>();
            this.PetHPLevels = new List<int>();
        }

        public GloomClass(GloomClassName className, 
                        string name, 
                        string symbol, 
                        bool isStarting, 
                        List<Perk> perks, 
                        List<int> hpLevels, 
                        List<int> petHPLevels)
        {
            this.ClassName = className;
            this.Name = name;
            this.Symbol = symbol;
            this.IsStarting = isStarting;
            this.Perks = perks;
            this.HPLevels = hpLevels;
            this.PetHPLevels = petHPLevels;
        }

        public GloomClassName ClassName { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public bool IsStarting { get; set; }
        public List<Perk> Perks { get; set; }
        public List<int> HPLevels { get; set; }
        public List<int> PetHPLevels { get; set; } // Awkward... this is empty for most classes

        public static GloomClass Create(GloomClassName className, string name, string symbol, bool isStarting) =>
            new GloomClass(className, name, symbol, isStarting, new List<Perk>(), new List<int>(), new List<int>());

        public GloomClass WithPerk(Perk perk)
        {
            this.Perks.Add(perk);
            return this;
        }

        public GloomClass WithHP(List<int> hpLevels)
        {
            this.HPLevels = hpLevels;
            return this;
        }

        public GloomClass WithPetHP(List<int> petHPLevels)
        {
            this.PetHPLevels = petHPLevels;
            return this;
        }
    }
}
