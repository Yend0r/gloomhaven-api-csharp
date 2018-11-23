using System;
using System.ComponentModel.DataAnnotations;

namespace GloomChars.Api.Characters
{
    public class NewCharacterRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ClassName { get; set; }
    }
}
