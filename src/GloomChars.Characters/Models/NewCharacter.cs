using System;
using System.ComponentModel.DataAnnotations;
using GloomChars.GameData.Models;

namespace GloomChars.Characters.Models
{
    public class NewCharacter
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public GloomClassName ClassName { get; set; }
    }
}
