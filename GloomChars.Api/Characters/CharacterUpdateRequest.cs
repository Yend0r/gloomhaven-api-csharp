using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GloomChars.Api.Characters
{
    public class CharacterUpdateRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Experience { get; set; }
        [Required]
        public int Gold { get; set; }
        [Required]
        public int Achievements { get; set; }
        [Required]
        public List<PerkRequest> Perks { get; set; }
    }
}
