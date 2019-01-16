using System;
namespace GloomChars.Characters.Repositories.Models
{
    public class DbModifierCard
    {
        public int Id { get; set; }
        public int Damage { get; set; }
        public bool DrawAnother { get; set; }
        public bool Reshuffle { get; set; }
        public string Action { get; set; }
        public int? ActionAmount { get; set; }
    }
}
