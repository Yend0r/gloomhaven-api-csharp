using System;
using System.Collections.Generic;
using Bearded.Monads;
using GloomChars.GameData.Interfaces;
using GloomChars.GameData.Models;

namespace GloomChars.GameData.Services
{
    public class GameDataService : IGameDataService
    {
        public GloomClass GetGloomClass(GloomClassName name)
        {
            return GloomClasses().Find(c => c.ClassName == name);
        }
        
        public Option<GloomClass> GetGloomClass(string name)
        {
            if (Enum.TryParse<GloomClassName>(name, true, out GloomClassName className))
            {
                return GetGloomClass(className);
            }
            return Option<GloomClass>.None;
        }
    
        public List<GloomClass> GloomClasses()
        {
            return new List<GloomClass>
            {
                Brute(),
                Tinkerer(),
                Spellweaver(),
                Scoundrel(),
                Cragheart(),
                Mindthief(),
                Sunkeeper(),
                Quartermaster(),
                Summoner(),
                Nightshroud(),
                Plagueherald(),
                Berserker(),
                Soothsinger(),
                Doomstalker(),
                Sawbones(),
                Elementalist(),
                BeastTyrant()
            };
        }
        
        private GloomClass Brute()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Brute, "Inox Brute", "Horns", true) 
                        
                        .WithPerk(PerkBuilder.Create("brt01", 1).RemoveCard(2, CardAction.Damage, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("brt02", 1).RemoveCard(1, CardAction.Damage, -1, false).AddCard(1, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("brt03", 2).AddCard(2, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("brt04", 1).AddCard(1, CardAction.Damage, 3, false).Build())
                        .WithPerk(PerkBuilder.Create("brt05", 2).AddCard(3, CardAction.Push, 1, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("brt06", 1).AddCard(2, CardAction.Pierce, 3, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("brt07", 2).AddCard(1, CardAction.Stun, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("brt08", 1).AddCard(1, CardAction.Disarm, 0, true).AddCard(1, CardAction.Muddle, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("brt09", 2).AddCard(1, CardAction.AddTarget, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("brt10", 1).AddCard(1, CardAction.Shield, 1, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("brt11", 1).IgnoreItems().AddCard(1, CardAction.Damage, 1, false).Build());
            return gClass;
        }
        

        private GloomClass Tinkerer()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Tinkerer, "Quatryl Tinkerer", "Cog", true) 
                        
                        .WithPerk(PerkBuilder.Create("tnk01", 2).RemoveCard(2, CardAction.Damage, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("tnk02", 1).RemoveCard(1, CardAction.Damage, -2, false).AddCard(1, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("tnk03", 1).AddCard(2, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("tnk04", 1).AddCard(1, CardAction.Damage, 3, false).Build())
                        .WithPerk(PerkBuilder.Create("tnk05", 1).AddCard(2, CardAction.Fire, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("tnk06", 1).AddCard(3, CardAction.Muddle, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("tnk07", 2).AddCard(1, CardAction.Wound, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("tnk08", 2).AddCard(1, CardAction.Immobilise, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("tnk09", 2).AddCard(1, CardAction.Heal, 2, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("tnk10", 1).AddCard(1, CardAction.AddTarget, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("tnk11", 1).IgnoreScenario().Build());
            return gClass;
        }
        

        private GloomClass Spellweaver()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Spellweaver, "Orchid Spellweaver", "Spell", true) 
                        
                        .WithPerk(PerkBuilder.Create("spl01", 1).RemoveCard(4, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("spl02", 2).RemoveCard(1, CardAction.Damage, -1, false).AddCard(1, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("spl03", 2).AddCard(2, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("spl04", 1).AddCard(1, CardAction.Stun, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("spl05", 1).AddCard(1, CardAction.Wound, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("spl06", 1).AddCard(1, CardAction.Immobilise, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("spl07", 1).AddCard(1, CardAction.Curse, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("spl08", 2).AddCard(1, CardAction.Fire, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("spl09", 2).AddCard(1, CardAction.Ice, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("spl10", 1).AddCard(1, CardAction.Earth, 0, true).AddCard(1, CardAction.Air, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("spl11", 1).AddCard(1, CardAction.Light, 0, true).AddCard(1, CardAction.Dark, 0, true).Build());
            return gClass;
        }
        

        private GloomClass Scoundrel()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Scoundrel, "Human Scoundrel", "ThrowingKnives", true) 
                        
                        .WithPerk(PerkBuilder.Create("scn01", 2).RemoveCard(2, CardAction.Damage, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("scn02", 1).RemoveCard(4, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("scn03", 1).RemoveCard(1, CardAction.Damage, -2, false).AddCard(1, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("scn04", 1).RemoveCard(1, CardAction.Damage, -1, false).AddCard(1, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("scn05", 2).RemoveCard(1, CardAction.Damage, 0, false).AddCard(1, CardAction.Damage, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("scn06", 2).AddCard(2, CardAction.Damage, 1, true).Build())
                        .WithPerk(PerkBuilder.Create("scn07", 1).AddCard(2, CardAction.Pierce, 3, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("scn08", 2).AddCard(2, CardAction.Poison, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("scn09", 1).AddCard(2, CardAction.Muddle, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("scn10", 1).AddCard(1, CardAction.Invisible, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("scn11", 1).IgnoreScenario().Build());
            return gClass;
        }
        

        private GloomClass Cragheart()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Cragheart, "Savvas Cragheart", "Rocks", true)                   
                        .WithPerk(PerkBuilder.Create("crg01", 1).RemoveCard(4, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("crg02", 3)
                                    .RemoveCard(1, CardAction.Damage, -1, false)
                                    .AddCard(1, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("crg03", 1).AddCard(1, CardAction.Damage, -2, false).AddCard(2, CardAction.Damage, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("crg04", 2).AddCard(1, CardAction.Immobilise, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("crg05", 2).AddCard(1, CardAction.Muddle, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("crg06", 1).AddCard(2, CardAction.Push, 2, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("crg07", 2).AddCard(2, CardAction.Air, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("crg08", 1).AddCard(2, CardAction.Earth, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("crg09", 1).IgnoreItems().Build())
                        .WithPerk(PerkBuilder.Create("crg10", 1).IgnoreScenario().Build());
            return gClass;
        }
        

        private GloomClass Mindthief()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Mindthief, "Vermling Mindthief", "Brain", true) 
                        
                        .WithPerk(PerkBuilder.Create("mnd01", 2).RemoveCard(2, CardAction.Damage, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("mnd02", 1).RemoveCard(4, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("mnd03", 1).RemoveCard(2, CardAction.Damage, 1, false).AddCard(2, CardAction.Damage, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("mnd04", 1).RemoveCard(1, CardAction.Damage, -2, false).AddCard(1, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("mnd05", 2).AddCard(1, CardAction.Ice, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("mnd06", 2).AddCard(2, CardAction.Damage, 1, true).Build())
                        .WithPerk(PerkBuilder.Create("mnd07", 2).AddCard(3, CardAction.Pull, 1, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("mnd08", 1).AddCard(3, CardAction.Muddle, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("mnd09", 1).AddCard(2, CardAction.Immobilise, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("mnd10", 1).AddCard(1, CardAction.Stun, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("mnd11", 1).AddCard(1, CardAction.Disarm, 0, true).AddCard(1, CardAction.Muddle, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("mnd12", 1).IgnoreScenario().Build());
            return gClass;
        }
        

        private GloomClass Sunkeeper()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Sunkeeper, "Valrath Sunkeeper", "Sun", false) 
                        
                        .WithPerk(PerkBuilder.Create("sun01", 2).RemoveCard(2, CardAction.Damage, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("sun02", 1).RemoveCard(4, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("sun03", 1).RemoveCard(1, CardAction.Damage, -2, false).AddCard(1, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("sun04", 1).RemoveCard(1, CardAction.Damage, 0, false).AddCard(1, CardAction.Damage, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("sun05", 2).AddCard(2, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("sun06", 2).AddCard(2, CardAction.Heal, 1, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("sun07", 1).AddCard(1, CardAction.Stun, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("sun08", 2).AddCard(2, CardAction.Light, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("sun09", 1).AddCard(1, CardAction.Shield, 1, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("sun10", 1).IgnoreItems().AddCard(2, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("sun11", 1).IgnoreScenario().Build());
            return gClass;
        }
        

        private GloomClass Quartermaster()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Quartermaster, "Valrath Quartermaster", "TripleArrow", false) 
                        
                        .WithPerk(PerkBuilder.Create("qrt01", 2).RemoveCard(2, CardAction.Damage, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("qrt02", 1).RemoveCard(4, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("qrt03", 2).RemoveCard(1, CardAction.Damage, 0, false).AddCard(1, CardAction.Damage, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("qrt04", 2).AddCard(2, CardAction.Damage, 1, true).Build())
                        .WithPerk(PerkBuilder.Create("qrt05", 1).AddCard(3, CardAction.Muddle, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("qrt06", 1).AddCard(2, CardAction.Pierce, 3, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("qrt07", 1).AddCard(1, CardAction.Stun, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("qrt08", 1).AddCard(1, CardAction.AddTarget, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("qrt09", 3).AddCard(1, CardAction.RefreshItem, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("qrt10", 1).IgnoreItems().AddCard(2, CardAction.Damage, 1, false).Build());
            return gClass;
        }
        

        private GloomClass Summoner()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Summoner, "Aesther Summoner", "Circles", false) 
                        
                        .WithPerk(PerkBuilder.Create("sum01", 1).RemoveCard(2, CardAction.Damage, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("sum02", 1).RemoveCard(1, CardAction.Damage, -2, false).AddCard(1, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("sum03", 3).RemoveCard(1, CardAction.Damage, -1, false).AddCard(1, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("sum04", 2).AddCard(1, CardAction.Damage, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("sum05", 1).AddCard(2, CardAction.Wound, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("sum06", 1).AddCard(2, CardAction.Poison, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("sum07", 3).AddCard(2, CardAction.Heal, 1, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("sum08", 1).AddCard(1, CardAction.Fire, 0, true).AddCard(1, CardAction.Air, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("sum09", 1).AddCard(1, CardAction.Dark, 0, true).AddCard(1, CardAction.Earth, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("sum10", 1).IgnoreItems().AddCard(2, CardAction.Damage, 1, false).Build());
            return gClass;
        }
        

        private GloomClass Nightshroud()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Nightshroud, "Aesther Nightshroud", "Eclipse", false) 
                        
                        .WithPerk(PerkBuilder.Create("ngt01", 2).RemoveCard(2, CardAction.Damage, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("ngt02", 1).RemoveCard(4, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("ngt03", 2).AddCard(1, CardAction.Dark, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("ngt04", 2).RemoveCard(1, CardAction.Dark, -1, false).AddCard(1, CardAction.Dark, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("ngt05", 2).AddCard(1, CardAction.Invisible, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("ngt06", 2).AddCard(3, CardAction.Muddle, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("ngt07", 1).AddCard(2, CardAction.Heal, 1, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("ngt08", 1).AddCard(2, CardAction.Curse, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("ngt09", 1).AddCard(1, CardAction.AddTarget, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("ngt10", 1).IgnoreItems().AddCard(2, CardAction.Damage, 1, false).Build());
            return gClass;
        }
        

        private GloomClass Plagueherald()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Plagueherald, "Harrower Plagueherald", "Cthulthu", false) 
                        
                        .WithPerk(PerkBuilder.Create("plg01", 1).RemoveCard(1, CardAction.Damage, -2, false).AddCard(1, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("plg02", 2).RemoveCard(1, CardAction.Damage, -1, false).AddCard(1, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("plg03", 2).RemoveCard(1, CardAction.Damage, 0, false).AddCard(1, CardAction.Damage, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("plg04", 1).AddCard(2, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("plg05", 3).AddCard(1, CardAction.Air, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("plg06", 1).AddCard(3, CardAction.Poison, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("plg07", 1).AddCard(2, CardAction.Curse, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("plg08", 1).AddCard(2, CardAction.Immobilise, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("plg09", 2).AddCard(1, CardAction.Stun, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("plg10", 1).IgnoreScenario().AddCard(1, CardAction.Damage, 1, false).Build());
            return gClass;
        }
        

        private GloomClass Berserker()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Berserker, "Inox Berserker", "Lightning", false) 
                        
                        .WithPerk(PerkBuilder.Create("brs01", 1).RemoveCard(2, CardAction.Damage, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("brs02", 1).RemoveCard(4, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("brs03", 2).RemoveCard(1, CardAction.Damage, -1, false).AddCard(1, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("brs04", 2).RemoveCard(1, CardAction.Damage, 0, false).AddCard(1, CardAction.Damage, 2, true).Build())
                        .WithPerk(PerkBuilder.Create("brs05", 2).AddCard(2, CardAction.Wound, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("brs06", 2).AddCard(1, CardAction.Stun, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("brs07", 1).AddCard(1, CardAction.Disarm, 1, true).Build())
                        .WithPerk(PerkBuilder.Create("brs08", 1).AddCard(2, CardAction.Heal, 1, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("brs09", 2).AddCard(1, CardAction.Fire, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("brs10", 1).IgnoreItems().Build());
            return gClass;
        }
        

        private GloomClass Soothsinger()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Soothsinger, "Quatryl Soothsinger", "MusicNote", false) 
                        
                        .WithPerk(PerkBuilder.Create("sth01", 2).RemoveCard(2, CardAction.Damage, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("sth02", 1).RemoveCard(1, CardAction.Damage, -2, false).Build())
                        .WithPerk(PerkBuilder.Create("sth03", 2).RemoveCard(2, CardAction.Damage, 1, false).AddCard(1, CardAction.Damage, 4, false).Build())
                        .WithPerk(PerkBuilder.Create("sth04", 1).RemoveCard(1, CardAction.Damage, 0, false).AddCard(1, CardAction.Immobilise, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("sth05", 1).RemoveCard(1, CardAction.Damage, 0, false).AddCard(1, CardAction.Disarm, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("sth06", 1).RemoveCard(1, CardAction.Damage, 0, false).AddCard(1, CardAction.Wound, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("sth07", 1).RemoveCard(1, CardAction.Damage, 0, false).AddCard(1, CardAction.Poison, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("sth08", 1).RemoveCard(1, CardAction.Damage, 0, false).AddCard(1, CardAction.Curse, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("sth09", 1).RemoveCard(1, CardAction.Damage, 0, false).AddCard(1, CardAction.Muddle, 3, false).Build())
                        .WithPerk(PerkBuilder.Create("sth10", 1).RemoveCard(1, CardAction.Damage, -1, false).AddCard(1, CardAction.Stun, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("sth11", 1).AddCard(3, CardAction.Damage, 1, true).Build())
                        .WithPerk(PerkBuilder.Create("sth12", 1).AddCard(2, CardAction.Curse, 0, true).Build());
            return gClass;
        }
        

        private GloomClass Doomstalker()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Doomstalker, "Orchid Doomstalker", "Mask", false) 
                        
                        .WithPerk(PerkBuilder.Create("dms01", 2).RemoveCard(2, CardAction.Damage, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("dms02", 3).RemoveCard(2, CardAction.Damage, 0, false).AddCard(2, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("dms03", 2).AddCard(2, CardAction.Damage, 1, true).Build())
                        .WithPerk(PerkBuilder.Create("dms04", 1).AddCard(1, CardAction.Muddle, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("dms05", 1).AddCard(1, CardAction.Poison, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("dms06", 1).AddCard(1, CardAction.Wound, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("dms07", 1).AddCard(1, CardAction.Immobilise, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("dms08", 1).AddCard(1, CardAction.Stun, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("dms09", 2).AddCard(1, CardAction.AddTarget, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("dms10", 1).IgnoreScenario().Build());
            return gClass;
        }
        

        private GloomClass Sawbones()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Sawbones, "Human Sawbones", "Saw", false) 
                        
                        .WithPerk(PerkBuilder.Create("saw01", 2).RemoveCard(2, CardAction.Damage, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("saw02", 1).RemoveCard(4, CardAction.Damage, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("saw03", 2).RemoveCard(1, CardAction.Damage, 0, false).AddCard(1, CardAction.Damage, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("saw04", 2).AddCard(1, CardAction.Damage, 2, true).Build())
                        .WithPerk(PerkBuilder.Create("saw05", 2).AddCard(1, CardAction.Immobilise, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("saw06", 2).AddCard(2, CardAction.Wound, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("saw07", 1).AddCard(1, CardAction.Stun, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("saw08", 1).AddCard(1, CardAction.Heal, 3, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("saw09", 1).AddCard(1, CardAction.RefreshItem, 0, false).Build());
            return gClass;
        }
        

        private GloomClass Elementalist()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.Elementalist, "Savvas Elementalist", "Triangle", false) 
                        
                        .WithPerk(PerkBuilder.Create("elm01", 2).RemoveCard(2, CardAction.Damage, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("elm02", 1).RemoveCard(1, CardAction.Damage, -1, false).AddCard(1, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("elm03", 2).RemoveCard(1, CardAction.Damage, 0, false).AddCard(1, CardAction.Damage, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("elm04", 1).AddCard(3, CardAction.Fire, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("elm05", 1).AddCard(3, CardAction.Ice, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("elm06", 1).AddCard(3, CardAction.Air, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("elm07", 1).AddCard(3, CardAction.Earth, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("elm08", 1).RemoveCard(2, CardAction.Damage, 0, false).AddCard(1, CardAction.Fire, 0, false).AddCard(1, CardAction.Earth, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("elm09", 1).RemoveCard(2, CardAction.Damage, 0, false).AddCard(1, CardAction.Ice, 0, false).AddCard(1, CardAction.Air, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("elm10", 1).AddCard(2, CardAction.Push, 1, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("elm11", 1).AddCard(1, CardAction.Wound, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("elm12", 1).AddCard(1, CardAction.Stun, 0, false).Build())
                        .WithPerk(PerkBuilder.Create("elm13", 1).AddCard(1, CardAction.AddTarget, 0, false).Build());
            return gClass;
        }
        

        private GloomClass BeastTyrant()
        {
            var gClass = 
                GloomClass.Create(GloomClassName.BeastTyrant, "Vermling Beast Tyrant", "TwoMinis", false) 
                        
                        .WithPerk(PerkBuilder.Create("bst01", 1).RemoveCard(2, CardAction.Damage, -1, false).Build())
                        .WithPerk(PerkBuilder.Create("bst02", 3).RemoveCard(1, CardAction.Damage, -1, false).AddCard(1, CardAction.Damage, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("bst03", 2).RemoveCard(1, CardAction.Damage, 0, false).AddCard(1, CardAction.Damage, 2, false).Build())
                        .WithPerk(PerkBuilder.Create("bst04", 2).AddCard(1, CardAction.Wound, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("bst05", 2).AddCard(1, CardAction.Immobilise, 1, false).Build())
                        .WithPerk(PerkBuilder.Create("bst06", 3).AddCard(2, CardAction.Heal, 1, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("bst07", 1).AddCard(2, CardAction.Earth, 0, true).Build())
                        .WithPerk(PerkBuilder.Create("bst08", 1).IgnoreScenario().Build());
            return gClass;
        }
    }
}
