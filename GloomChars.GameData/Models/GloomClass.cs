using System;
using System.Collections.Generic;

namespace GloomChars.GameData.Models
{
    public class GloomClass
    {
        public GloomClass() { }

        public GloomClass(GloomClassName className, string name, string symbol, bool isStarting, List<Perk> perks)
        {
            this.ClassName = className;
            this.Name = name;
            this.Symbol = symbol;
            this.IsStarting = isStarting;
            this.Perks = perks;
        }

        public GloomClassName ClassName { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public bool IsStarting { get; set; }
        public List<Perk> Perks { get; set; }

        public static GloomClass Create(GloomClassName className, string name, string symbol, bool isStarting) =>
            new GloomClass(className, name, symbol, isStarting, new List<Perk>());

        public GloomClass WithPerk(Perk perk)
        {
            this.Perks.Add(perk);
            return this;
        }
    }
}
