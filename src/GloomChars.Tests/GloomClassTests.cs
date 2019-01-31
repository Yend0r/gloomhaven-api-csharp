using System;
using System.Collections.Generic;
using System.Linq;
using GloomChars.GameData.Interfaces;
using GloomChars.GameData.Services;
using GloomChars.GameData.Models;
using Xunit;

namespace GloomChars.Tests
{
    public class GloomClassTests
    {
        IGameDataService _gameSvc = new GameDataService();

        [Fact]
        public void XP_should_have_correct_level()
        {
            var xpList =
                new List<int> { 20, 45, 90, 95, 120, 150, 200, 210, 250, 275, 300, 345, 400, 420, 450, 500, 600 };

            var levels = xpList.Select(i => _gameSvc.GetCharacterLevel(i)).ToList();

            var expected =
                new List<int> { 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 9 };

            for (int i = 0; i < levels.Count; i++)
            {
                Assert.Equal(levels[i], expected[i]);
            }            
        }

        [Fact]
        public void All_classes_should_have_9_HP_levelsl()
        {
            var ghClasses = _gameSvc.GloomClasses();

            foreach (var ghClass in ghClasses)
            {
                Assert.Equal(9, ghClass.HPLevels.Count);
            }            
        }

        [Fact]
        public void Brute_HP_should_have_correct_value()
        {
            var xpList =
                new List<int> { 20, 90, 120, 200, 250, 300, 400, 450, 600 };

            var brute = _gameSvc.GetGloomClass(GloomClassName.Brute);

            var hpList = xpList.Select(i => _gameSvc.GetCharacterHP(brute, i)).ToList();

            for (int i = 0; i < hpList.Count; i++)
            {
                Assert.Equal(hpList[i], brute.HPLevels[i]);
            }            
        }

        [Fact]
        public void BeastTyrant_Pet_HP_should_have_correct_value()
        {
            var xpList =
                new List<int> { 20, 90, 120, 200, 250, 300, 400, 450, 600 };

            var beast = _gameSvc.GetGloomClass(GloomClassName.BeastTyrant);

            var hpList = xpList.Select(i => _gameSvc.GetCharacterHP(beast, i)).ToList();

            for (int i = 0; i < hpList.Count; i++)
            {
                Assert.Equal(hpList[i], beast.HPLevels[i]);
            }            
        }

        [Fact]
        public void There_should_be_17_classes()
        {
            var ghClasses = _gameSvc.GloomClasses();
            Assert.Equal(17, ghClasses.Count);
        }

        [Fact]
        public void There_should_be_6_starting_classes()
        {
            var ghClasses = _gameSvc.GloomClasses().Where(c => c.IsStarting).ToList();
            Assert.Equal(6, ghClasses.Count);
        }

        [Fact]
        public void Brute_perks()
        {
            var perks = new List<string> {
                "Remove two -1 cards",
                "Remove one -1 card and add one +1 card",
                "Add two +1 cards",
                "Add one +3 card",
                "Add three PUSH 1 DRAW ANOTHER cards",    
                "Add two PIERCE 3 DRAW ANOTHER cards",
                "Add one STUN DRAW ANOTHER card",
                "Add one DISARM DRAW ANOTHER card and add one MUDDLE DRAW ANOTHER card",
                "Add one ADD TARGET DRAW ANOTHER card",
                "Add one +1 SHIELD 1 card",
                "Ignore negative item effects and add one +1 card",
            };

            var ghClass = _gameSvc.GetGloomClass(GloomClassName.Brute);
            
            for (int i = 0; i < perks.Count; i++)
            {
                Assert.Equal(perks[i], ghClass.Perks[i].ActionsToString());
            } 
        }
    }
}
