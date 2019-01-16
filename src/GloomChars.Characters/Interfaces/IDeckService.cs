using System;
using GloomChars.Characters.Models;

namespace GloomChars.Characters.Interfaces
{
    public interface IDeckService
    {
        ModifierDeck GetDeck(Character character);
        ModifierDeck DrawCard(Character character);
        ModifierDeck Reshuffle(Character character);
    }
}
