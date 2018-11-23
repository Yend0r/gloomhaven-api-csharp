using System;
using GloomChars.Characters.Models;

namespace GloomChars.Characters.Repositories.Models
{
    public class DbNewCharacter
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public DbNewCharacter(NewCharacter ch)
        {
            UserId = ch.UserId;
            Name = ch.Name;
            ClassName = ch.ClassName.ToString();
            DateCreated = DateTime.Now;
            DateUpdated = DateTime.Now;
        }
    }
}
