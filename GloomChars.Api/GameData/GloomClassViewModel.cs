using System;
using System.Linq;
using System.Collections.Generic;
using GloomChars.GameData.Models;

namespace GloomChars.Api.GameData
{
    public class GloomClassViewModel
    {
        public string ClassName { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public bool IsStarting { get; set; }
        public IEnumerable<PerkViewModel> Perks { get; set; }

        public GloomClassViewModel(GloomClass gClass)
        {
            ClassName  = gClass.ClassName.ToString();
            Name       = gClass.Name;
            Symbol     = gClass.Symbol;
            IsStarting = gClass.IsStarting;
            Perks      = gClass.Perks.Select(p => new PerkViewModel(p));
        }
    }
}
