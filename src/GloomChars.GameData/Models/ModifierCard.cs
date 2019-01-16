using System;
namespace GloomChars.GameData.Models
{
    public class ModifierCard
    {
        public ModifierCard() { }

        public ModifierCard(int dmg, CardAction action, int? actionAmnt, bool draw, bool reshuffle)
        {
            this.DrawAnother = draw;
            this.Reshuffle = reshuffle;
            this.Action = action;
            this.ActionAmount = actionAmnt;
            this.Damage = dmg;
        }

        public bool DrawAnother { get; set; }
        public bool Reshuffle { get; set; }
        public CardAction Action { get; set; }
        public int? ActionAmount { get; set; }
        public int Damage { get; set; }

        public static ModifierCard DamageCard(int dmg)
            => new ModifierCard(dmg, CardAction.Damage, null, false, false);

        public override bool Equals(object obj)
            => Equals(obj as ModifierCard);

        public bool Equals(ModifierCard obj)
        {
            // Check for null if nullable (e.g., a reference type)
            if (obj == null)
            {
                return false;
            }

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // Check for equivalent hash codes
            if (this.GetHashCode() != obj.GetHashCode())
            {
                return false;
            }

            // Compare identifying fields for equality.
            return this.DrawAnother.Equals(obj.DrawAnother)
                && this.Reshuffle.Equals(obj.Reshuffle)
                && this.Action.Equals(obj.Action)
                && this.ActionAmount.Equals(obj.ActionAmount)
                && this.Damage.Equals(obj.Damage);
        }

        public override int GetHashCode()
        {
            // 269 and 47 are primes
            int hash = 269;
            hash = (hash * 47) + DrawAnother.GetHashCode();
            hash = (hash * 47) + Reshuffle.GetHashCode();
            hash = (hash * 47) + Action.GetHashCode();
            hash = (hash * 47) + ActionAmount.GetHashCode();
            hash = (hash * 47) + Damage.GetHashCode();
            return hash;
        }

        public static bool operator ==(ModifierCard a, ModifierCard b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }
        
            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
        
            // Return true if the fields match:
            return a.Equals(b);
        }
        
        public static bool operator !=(ModifierCard a, ModifierCard b)
        {
            return !(a == b);
        }
    }
}