using System;
using GloomChars.Characters.Models;

namespace GloomChars.Characters.Repositories.Models
{
    public class DbCharacterUpdate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }      
        public int Experience { get; set; }
        public int Gold { get; set; }
        public int Achievements { get; set; }
        public DateTime DateUpdated { get; set; }

        public DbCharacterUpdate(CharacterUpdate ch)
        {
            Id = ch.Id;
            UserId = ch.UserId;
            Name = ch.Name;
            Experience = ch.Experience;
            Gold = ch.Gold;
            Achievements = ch.Achievements;
            DateUpdated = DateTime.Now;
        }
    }
}
