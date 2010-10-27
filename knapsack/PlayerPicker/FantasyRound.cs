using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayerPicker
{
    public class FantasyRound
    {
        public string Name { get; private set; }

        private readonly Dictionary<string, Team> teams = new Dictionary<string, Team>();
        private readonly List<Player> players = new List<Player>();

        public int TeamSize { get; private set; }
        public int TeamCost { get; private set; }
        public int AntiTeamSize { get; private set; }
        public int AntiTeamCost { get; private set; }

        public IEnumerable<Player> Players
        {
            get { return players; }
        }

        public FantasyRound(string name, int teamSize, int teamCost, int antiTeamSize, int antiTeamCost)
        {
            Name = name;
            TeamSize = teamSize;
            TeamCost = teamCost;
            AntiTeamSize = antiTeamSize;
            AntiTeamCost = antiTeamCost;
        }

        private int teamId = 1;
        public void AddTeam(string name, int cost, int score)
        {
            var newTeam = new Team(teamId++, name);
            teams.Add(name, newTeam);

            // Create a "dummy player"
            players.Add(new Player(newTeam, name, cost, score) {
                IsDummyPlayer = true
            });
        }

        public void AddPlayer(string name, string teamName, int cost, int score)
        {
            var team = teams[teamName];
            if (players.Any(player => player.Name == name))
            {
                Console.WriteLine("!!!WARNING!!! Duplicate Player: " + name);
                return;
            }
            players.Add(new Player(team, name, cost, score));
        }

        #region Round #7

        public static FantasyRound CreateRound7()
        {
            var round = new FantasyRound("Round 7", 6, 30, 3, 13);
            round.AddTeam("KTR", 3, 23);
            round.AddTeam("EST", 2, 23);
            round.AddTeam("WJN", 1, 23);
            round.AddTeam("MBC", 4, 23);
            round.AddTeam("WEM", 5, 23);
            round.AddTeam("HWA", 1, 12);
            round.AddTeam("CJE", 2, 33);
            round.AddTeam("STX", 3, 23);
            round.AddTeam("SKT", 4, 13);
            round.AddTeam("SAM", 5, 24);
            round.AddTeam("HIT", 1, 2);
            round.AddTeam("ACE", 2, 5);

            round.AddPlayer("815", "KTR", 2, 12);
            round.AddPlayer("Action", "EST", 3, 22);
            round.AddPlayer("Actually", "WJN", 1, 7);
            round.AddPlayer("Ameba", "MBC", 1, 8);
            round.AddPlayer("Anytime", "ACE", 2, 6);
            round.AddPlayer("Arisol", "WEM", 1, 4);
            round.AddPlayer("Baby", "WEM", 4, 23);
            round.AddPlayer("BackHo", "HWA", 1, 5);
            round.AddPlayer("Barracks", "KTR", 1, 10);
            round.AddPlayer("BByong", "CJE", 1, 3);
            round.AddPlayer("Bee", "HWA", 1, 5);
            round.AddPlayer("Best", "SKT", 3, 15);
            round.AddPlayer("Bogus", "STX", 1, 7);
            round.AddPlayer("Boxer", "SKT", 1, 6);
            round.AddPlayer("Brave", "SAM", 1, 6);
            round.AddPlayer("Britney", "HIT", 1, 5);
            round.AddPlayer("Bubble", "SKT", 1, 6);
            round.AddPlayer("Calm", "STX", 7, 26);
            round.AddPlayer("Canata", "SKT", 2, 9);
            round.AddPlayer("Chavi", "SAM", 1, 8);
            round.AddPlayer("Clare", "MBC", 1, 8);
            round.AddPlayer("Classic", "EST", 1, 4);
            round.AddPlayer("Clay", "HIT", 1, 5);
            round.AddPlayer("Crazy-Hydra", "WJN", 2, 7);
            round.AddPlayer("CuteAngel", "STX", 2, 7);
            round.AddPlayer("Daezang", "WJN", 1, 7);
            round.AddPlayer("DarkElf", "ACE", 1, 4);
            round.AddPlayer("DDong", "HWA", 1, 5);
            round.AddPlayer("Dear", "EST", 1, 4);
            round.AddPlayer("Devil", "CJE", 1, 3);
            round.AddPlayer("Doctor.K", "SKT", 1, 6);
            round.AddPlayer("Dongrae", "HWA", 1, 5);
            round.AddPlayer("Eagle", "WJN", 1, 7);
            round.AddPlayer("Effort", "CJE", 7, 22);
            round.AddPlayer("Fancy", "EST", 1, 6);
            round.AddPlayer("Fantasy", "SKT", 8, 43);
            round.AddPlayer("Favian", "SAM", 1, 6);
            round.AddPlayer("Firebathero", "SAM", 3, 24);
            round.AddPlayer("FireFist", "KTR", 2, 10);
            round.AddPlayer("Flash", "KTR", 12, 61);
            round.AddPlayer("Flying", "EST", 2, 6);
            round.AddPlayer("ForGG", "KTR", 3, 26);
            round.AddPlayer("Foru", "CJE", 1, 3);
            round.AddPlayer("Free", "WJN", 5, 22);
            round.AddPlayer("Ganzi", "WJN", 1, 7);
            round.AddPlayer("GGaemo", "HWA", 1, 7);
            round.AddPlayer("GGMan", "SKT", 1, 6);
            round.AddPlayer("Go.go", "HIT", 2, 9);
            round.AddPlayer("GoRush", "ACE", 1, 6);
            round.AddPlayer("Grape", "EST", 1, 4);
            round.AddPlayer("Great", "SAM", 3, 11);
            round.AddPlayer("Guemchi", "WJN", 3, 35);
            round.AddPlayer("Haha", "SAM", 1, 6);
            round.AddPlayer("Haitai", "SKT", 1, 6);
            round.AddPlayer("HakSoo", "HIT", 1, 5);
            round.AddPlayer("Han", "MBC", 2, 8);
            round.AddPlayer("Haran", "EST", 1, 4);
            round.AddPlayer("Herb", "HIT", 1, 5);
            round.AddPlayer("Hero", "STX", 2, 9);
            round.AddPlayer("Hero[Join]", "WEM", 1, 4);
            round.AddPlayer("Hiya", "HWA", 6, 29);
            round.AddPlayer("Hodduk", "STX", 1, 7);
            round.AddPlayer("Hoejja", "KTR", 4, 18);
            round.AddPlayer("Hogil", "HIT", 3, 15);
            round.AddPlayer("Hon_Sin", "WJN", 1, 7);
            round.AddPlayer("Horang2", "HIT", 2, 10);
            round.AddPlayer("Hungry", "WEM", 1, 4);
            round.AddPlayer("Hwan", "SKT", 1, 6);
            round.AddPlayer("Hwasin", "STX", 3, 13);
            round.AddPlayer("Hwata", "EST", 1, 4);
            round.AddPlayer("Hydra", "CJE", 2, 7);
            round.AddPlayer("Hyuk", "SKT", 2, 18);
            round.AddPlayer("Hyun", "MBC", 3, 17);
            round.AddPlayer("Hyvaa", "EST", 2, 17);
            round.AddPlayer("Idra", "CJE", 1, 3);
            round.AddPlayer("Iloveoov", "SKT", 1, 6);
            round.AddPlayer("Invade", "CJE", 1, 3);
            round.AddPlayer("Iris", "CJE", 2, 5);
            round.AddPlayer("Iron", "MBC", 1, 8);
            round.AddPlayer("Isac", "EST", 1, 4);
            round.AddPlayer("Jaedong", "HWA", 10, 41);
            round.AddPlayer("Jaehoon", "MBC", 3, 19);
            round.AddPlayer("Jaewoo", "MBC", 1, 8);
            round.AddPlayer("Jangbi", "SAM", 6, 22);
            round.AddPlayer("JJonga", "CJE", 1, 3);
            round.AddPlayer("July", "STX", 1, 7);
            round.AddPlayer("Juni", "SAM", 2, 15);
            round.AddPlayer("Just", "KTR", 1, 10);
            round.AddPlayer("Justin", "HIT", 1, 5);
            round.AddPlayer("Kal", "STX", 7, 31);
            round.AddPlayer("Killer", "HWA", 4, 21);
            round.AddPlayer("Knight", "WEM", 1, 4);
            round.AddPlayer("Koala", "MBC", 1, 8);
            round.AddPlayer("Kwanro", "WJN", 5, 28);
            round.AddPlayer("Lake[Name]", "SKT", 1, 6);
            round.AddPlayer("Lake[Shield]", "HIT", 1, 5);
            round.AddPlayer("Last", "STX", 1, 7);
            round.AddPlayer("Leta", "HIT", 9, 39);
            round.AddPlayer("Light", "MBC", 8, 61);
            round.AddPlayer("Lomo", "HWA", 2, 9);
            round.AddPlayer("Lucifer", "WEM", 1, 4);
            round.AddPlayer("Lucky", "SAM", 1, 6);
            round.AddPlayer("Luxury", "KTR", 2, 15);
            round.AddPlayer("M18M", "SAM", 2, 10);
            round.AddPlayer("Maestro", "SKT", 1, 6);
            round.AddPlayer("Major", "WEM", 1, 8);
            round.AddPlayer("Marine", "HIT", 1, 5);
            round.AddPlayer("Mell", "HWA", 1, 5);
            round.AddPlayer("Midas", "WEM", 2, 10);
            round.AddPlayer("Mind", "WEM", 4, 14);
            round.AddPlayer("Mingu", "WJN", 1, 7);
            round.AddPlayer("Miracle", "SAM", 1, 6);
            round.AddPlayer("Miso", "CJE", 1, 3);
            round.AddPlayer("Modesty", "STX", 3, 22);
            round.AddPlayer("Mong", "SKT", 1, 6);
            round.AddPlayer("Moon", "WEM", 1, 4);
            round.AddPlayer("Movie", "CJE", 4, 19);
            round.AddPlayer("Much", "ACE", 3, 10);
            round.AddPlayer("MVP", "WJN", 1, 7);
            round.AddPlayer("MVPZerg", "MBC", 1, 8);
            round.AddPlayer("Nada", "WEM", 1, 6);
            round.AddPlayer("Nbs", "CJE", 1, 3);
            round.AddPlayer("Neel", "HWA", 1, 5);
            round.AddPlayer("Notice", "STX", 1, 7);
            round.AddPlayer("Odin", "SAM", 2, 8);
            round.AddPlayer("Orion", "CJE", 1, 3);
            round.AddPlayer("Paralyze", "SKT", 1, 6);
            round.AddPlayer("Peace", "MBC", 1, 11);
            round.AddPlayer("PerfectMan", "HWA", 2, 5);
            round.AddPlayer("Piano", "WJN", 1, 9);
            round.AddPlayer("Princess", "HWA", 1, 5);
            round.AddPlayer("Protoss", "SKT", 1, 6);
            round.AddPlayer("Puma", "HIT", 2, 20);
            round.AddPlayer("Pure", "WEM", 2, 4);
            round.AddPlayer("Purple", "WEM", 1, 4);
            round.AddPlayer("Pusan", "MBC", 1, 8);
            round.AddPlayer("Rage", "KTR", 1, 10);
            round.AddPlayer("Rarity", "SAM", 1, 6);
            round.AddPlayer("Reach", "ACE", 1, 6);
            round.AddPlayer("Really", "EST", 6, 26);
            round.AddPlayer("Remember", "HWA", 1, 5);
            round.AddPlayer("Rock", "WEM", 1, 4);
            round.AddPlayer("Roro", "WEM", 5, 20);
            round.AddPlayer("Ruby", "ACE", 3, 17);
            round.AddPlayer("Rush", "CJE", 1, 3);
            round.AddPlayer("S2", "SKT", 1, 6);
            round.AddPlayer("Sacsri", "WEM", 1, 6);
            round.AddPlayer("Saint", "MBC", 2, 8);
            round.AddPlayer("Sair", "WJN", 1, 7);
            round.AddPlayer("SangHo", "EST", 2, 6);
            round.AddPlayer("Savior", "CJE", 2, 3);
            round.AddPlayer("Say", "EST", 1, 4);
            round.AddPlayer("Sea", "MBC", 6, 24);
            round.AddPlayer("Shark", "MBC", 1, 8);
            round.AddPlayer("Sharp", "SAM", 1, 6);
            round.AddPlayer("Shine", "WEM", 3, 14);
            round.AddPlayer("Sky", "HWA", 1, 5);
            round.AddPlayer("Skyhigh", "CJE", 5, 23);
            round.AddPlayer("Smile", "WEM", 1, 6);
            round.AddPlayer("Snow", "CJE", 1, 15);
            round.AddPlayer("Someday", "KTR", 2, 10);
            round.AddPlayer("Soo_SKT", "SKT", 1, 10);
            round.AddPlayer("Soo_STX", "STX", 2, 7);
            round.AddPlayer("Soulkey", "WJN", 1, 7);
            round.AddPlayer("Spear", "HWA", 1, 5);
            round.AddPlayer("Special", "WJN", 1, 7);
            round.AddPlayer("Sse", "STX", 1, 7);
            round.AddPlayer("Stats", "KTR", 1, 26);
            round.AddPlayer("Stork", "SAM", 3, 27);
            round.AddPlayer("SungEun", "HIT", 7, 5);
            round.AddPlayer("Sungsun", "SKT", 1, 6);
            round.AddPlayer("Suny", "KTR", 1, 10);
            round.AddPlayer("Tazza", "HIT", 1, 5);
            round.AddPlayer("Tears", "WEM", 1, 4);
            round.AddPlayer("Tempest", "KTR", 1, 10);
            round.AddPlayer("Tester", "EST", 1, 4);
            round.AddPlayer("Thezerg", "SKT", 1, 6);
            round.AddPlayer("Thunder", "HWA", 1, 5);
            round.AddPlayer("Tiny", "MBC", 1, 8);
            round.AddPlayer("TossGirl", "STX", 1, 7);
            round.AddPlayer("TossLove", "SKT", 1, 6);
            round.AddPlayer("Trap", "STX", 1, 7);
            round.AddPlayer("Turn", "SAM", 1, 6);
            round.AddPlayer("Type-B", "HIT", 1, 5);
            round.AddPlayer("UpMagic", "EST", 2, 12);
            round.AddPlayer("Violet", "KTR", 3, 12);
            round.AddPlayer("Wizard", "HWA", 4, 5);
            round.AddPlayer("Wrath", "WJN", 1, 7);
            round.AddPlayer("Wuk", "EST", 1, 4);
            round.AddPlayer("Xellos", "ACE", 1, 4);
            round.AddPlayer("Ych", "STX", 3, 7);
            round.AddPlayer("Yellow", "ACE", 1, 4);
            round.AddPlayer("YeongJae", "SKT", 1, 6);
            round.AddPlayer("Yoon", "SAM", 1, 6);
            round.AddPlayer("YoonJoong", "STX", 1, 25);
            round.AddPlayer("Zerg[kaL]", "SKT", 3, 6);
            round.AddPlayer("Zero", "WJN", 1, 27);
            round.AddPlayer("Zero_WEM", "WEM", 7, 4);

            return round;
        }

        #endregion

        #region Round #8

        public static FantasyRound CreateRound8()
        {
            var round = new FantasyRound("Fake Round 8", 6, 30, 3, 13);
            round.AddTeam("WEM", 5, 34);
            round.AddTeam("CJE", 5, 32);
            round.AddTeam("EST", 3, 21);
            round.AddTeam("STX", 6, 20);
            round.AddTeam("WJN", 4, 20);
            round.AddTeam("KTR", 6, 19);
            round.AddTeam("SKT", 4, 16);
            round.AddTeam("HWA", 4, 15);
            round.AddTeam("SAM", 3, 8);
            round.AddTeam("HIT", 2, 5);
            round.AddTeam("MBC", 3, 4);
            round.AddTeam("ACE", -1, -8);

            round.AddPlayer("815", "KTR", 1, 6);
            round.AddPlayer("45[Name]", "HIT", 1, 0);
            round.AddPlayer("49[ScM]", "HIT", 1, 0);
            round.AddPlayer("Action", "EST", 4, 22);
            round.AddPlayer("Actually", "WJN", 1, 4);
            round.AddPlayer("Ameba", "MBC", 1, 4);
            round.AddPlayer("Anyppi", "KTR", 1, 0);
            round.AddPlayer("Anytime", "ACE", 3, 14);
            round.AddPlayer("Arisol", "WEM", 2, 8);
            round.AddPlayer("ASuka-Jr", "HIT", 1, 4);
            round.AddPlayer("Baby", "WEM", 7, 39);
            round.AddPlayer("BackHo", "HWA", 1, 6);
            round.AddPlayer("Barracks", "KTR", 1, 6);
            round.AddPlayer("BByong", "CJE", 2, 8);
            round.AddPlayer("Bee", "HWA", 1, 6);
            round.AddPlayer("Best", "SKT", 4, 19);
            round.AddPlayer("Bisu", "SKT", 5, 16);
            round.AddPlayer("Bogus", "STX", 3, 15);
            round.AddPlayer("Boxer", "SKT", 1, 6);
            round.AddPlayer("Brave", "SAM", 2, 11);
            round.AddPlayer("Britney", "HIT", 1, 4);
            round.AddPlayer("Bubble", "SKT", 1, 6);
            round.AddPlayer("Calm", "STX", 6, 31);
            round.AddPlayer("Canata", "SKT", 2, 6);
            round.AddPlayer("Casy", "ACE", 0, 1);
            round.AddPlayer("Chavi", "SAM", 2, 11);
            round.AddPlayer("Clare", "MBC", 1, 4);
            round.AddPlayer("Classic", "EST", 3, 21);
            round.AddPlayer("Clay", "HIT", 1, 4);
            round.AddPlayer("Cloud", "ACE", 0, 1);
            round.AddPlayer("Crazy-Hydra", "WJN", 1, 4);
            round.AddPlayer("CuteAngel", "STX", 2, 12);
            round.AddPlayer("Daezang", "WJN", 1, 7);
            round.AddPlayer("DDong", "HWA", 1, 6);
            round.AddPlayer("Dear", "EST", 1, 5);
            round.AddPlayer("Devil", "CJE", 2, 8);
            round.AddPlayer("Doctor.K", "SKT", 1, 6);
            round.AddPlayer("Dongrae", "HWA", 1, 6);
            round.AddPlayer("Eagle", "WJN", 1, 4);
            round.AddPlayer("Effort", "CJE", 7, 26);
            round.AddPlayer("Fancy", "EST", 1, 5);
            round.AddPlayer("Fantasy", "SKT", 7, 33);
            round.AddPlayer("Favian", "SAM", 1, 5);
            round.AddPlayer("Firebathero", "SAM", 2, 6);
            round.AddPlayer("FireFist", "KTR", 2, 9);
            round.AddPlayer("Flash", "KTR", 11, 43);
            round.AddPlayer("Flying", "EST", 5, 25);
            round.AddPlayer("ForGG", "KTR", 5, 24);
            round.AddPlayer("Foru", "CJE", 1, 8);
            round.AddPlayer("Free", "WJN", 4, 16);
            round.AddPlayer("Ganzi", "WJN", 1, 4);
            round.AddPlayer("GGaemo", "ACE", 1, 6);
            round.AddPlayer("GGMan", "SKT", 1, 6);
            round.AddPlayer("GoRush", "ACE", 2, 8);
            round.AddPlayer("Grape", "EST", 1, 5);
            round.AddPlayer("Great", "SAM", 5, 19);
            round.AddPlayer("Guemchi", "WJN", 3, 11);
            round.AddPlayer("Haha", "SAM", 1, 5);
            round.AddPlayer("Haitai", "SKT", 1, 6);
            round.AddPlayer("Han", "MBC", 1, 7);
            round.AddPlayer("Haran", "EST", 1, 5);
            round.AddPlayer("Herb", "HIT", 1, 4);
            round.AddPlayer("Hero", "STX", 3, 22);
            round.AddPlayer("Hero[Join]", "WEM", 2, 8);
            round.AddPlayer("Hiya", "HWA", 5, 18);
            round.AddPlayer("Hodduk", "STX", 1, 9);
            round.AddPlayer("Hoejja", "KTR", 3, 10);
            round.AddPlayer("Hogil", "HIT", 4, 15);
            round.AddPlayer("Hon_Sin", "WJN", 1, 4);
            round.AddPlayer("Horang2", "HIT", 5, 24);
            round.AddPlayer("Hungry", "WEM", 2, 8);
            round.AddPlayer("Hwan", "SKT", 1, 6);
            round.AddPlayer("Hwata", "EST", 1, 5);
            round.AddPlayer("Hydra", "CJE", 3, 21);
            round.AddPlayer("Hyuk", "SKT", 5, 23);
            round.AddPlayer("Hyun", "MBC", 4, 20);
            round.AddPlayer("Hyvaa", "EST", 2, 8);
            round.AddPlayer("Iloveoov", "SKT", 1, 6);
            round.AddPlayer("Invade", "CJE", 1, 8);
            round.AddPlayer("Iris", "CJE", 3, 21);
            round.AddPlayer("Iron", "MBC", 1, 5);
            round.AddPlayer("Isac", "EST", 1, 5);
            round.AddPlayer("Jaedong", "HWA", 11, 51);
            round.AddPlayer("Jaehoon", "MBC", 2, 5);
            round.AddPlayer("Jaewoo", "MBC", 1, 4);
            round.AddPlayer("Jangbi", "SAM", 6, 23);
            round.AddPlayer("JJonga", "CJE", 1, 8);
            round.AddPlayer("July", "STX", 1, 9);
            round.AddPlayer("Juni", "SAM", 2, 8);
            round.AddPlayer("Just", "KTR", 1, 6);
            round.AddPlayer("Kal", "STX", 8, 44);
            round.AddPlayer("Killer", "HWA", 4, 13);
            round.AddPlayer("Knight", "WEM", 2, 8);
            round.AddPlayer("Koala", "MBC", 1, 4);
            round.AddPlayer("Kwanro", "WJN", 4, 20);
            round.AddPlayer("Lake[Name]", "SKT", 1, 6);
            round.AddPlayer("Lake[Shield]", "HIT", 1, 4);
            round.AddPlayer("Last", "STX", 1, 9);
            round.AddPlayer("Leta", "HIT", 7, 34);
            round.AddPlayer("Light", "MBC", 5, 20);
            round.AddPlayer("Lomo", "HWA", 4, 18);
            round.AddPlayer("Lucifer", "WEM", 2, 8);
            round.AddPlayer("Lucky", "SAM", 1, 5);
            round.AddPlayer("M18M", "SAM", 1, 5);
            round.AddPlayer("Maestro", "SKT", 1, 6);
            round.AddPlayer("Major", "WEM", 2, 11);
            round.AddPlayer("Marine", "HIT", 1, 4);
            round.AddPlayer("Mell", "HWA", 1, 6);
            round.AddPlayer("Midas", "WEM", 4, 24);
            round.AddPlayer("Mind", "WEM", 3, 11);
            round.AddPlayer("Mingu", "WJN", 1, 4);
            round.AddPlayer("Miracle", "SAM", 1, 5);
            round.AddPlayer("Miso", "CJE", 1, 8);
            round.AddPlayer("Modesty", "STX", 2, 12);
            round.AddPlayer("Mong", "SKT", 1, 6);
            round.AddPlayer("Moon", "WEM", 2, 8);
            round.AddPlayer("Movie", "CJE", 3, 15);
            round.AddPlayer("Much", "ACE", 1, 5);
            round.AddPlayer("MVP", "WJN", 1, 7);
            round.AddPlayer("MVPZerg", "MBC", 1, 4);
            round.AddPlayer("Nada", "WEM", 2, 12);
            round.AddPlayer("Nbs", "CJE", 1, 8);
            round.AddPlayer("Neel", "HWA", 1, 6);
            round.AddPlayer("Notice", "STX", 1, 9);
            round.AddPlayer("Odin", "SAM", 1, 5);
            round.AddPlayer("Orion", "CJE", 1, 11);
            round.AddPlayer("Paralyze", "SKT", 1, 6);
            round.AddPlayer("Peace", "MBC", 2, 11);
            round.AddPlayer("PerfectMan", "HWA", 2, 9);
            round.AddPlayer("Piano", "WJN", 1, 7);
            round.AddPlayer("Princess", "HWA", 1, 6);
            round.AddPlayer("Protoss", "SKT", 1, 6);
            round.AddPlayer("Puma", "HIT", 2, 4);
            round.AddPlayer("Pure", "WEM", 2, 8);
            round.AddPlayer("Purple", "WEM", 2, 8);
            round.AddPlayer("Pusan", "MBC", 1, 4);
            round.AddPlayer("Rage", "KTR", 1, 6);
            round.AddPlayer("Rarity", "SAM", 1, 5);
            round.AddPlayer("Reach", "ACE", 2, 12);
            round.AddPlayer("Reality", "SAM", 1, 5);
            round.AddPlayer("Really", "EST", 6, 30);
            round.AddPlayer("Remember", "HWA", 1, 6);
            round.AddPlayer("Rock", "ACE", 1, 8);
            round.AddPlayer("Roro", "WEM", 6, 33);
            round.AddPlayer("Ruby", "ACE", 2, 10);
            round.AddPlayer("Rush", "CJE", 1, 8);
            round.AddPlayer("S2", "SKT", 3, 16);
            round.AddPlayer("Sacsri", "WEM", 2, 8);
            round.AddPlayer("Saint", "MBC", 2, 10);
            round.AddPlayer("Sair", "WJN", 1, 4);
            round.AddPlayer("Say", "EST", 1, 5);
            round.AddPlayer("Sea", "MBC", 4, 16);
            round.AddPlayer("Shark", "MBC", 1, 4);
            round.AddPlayer("Sharp", "SAM", 1, 5);
            round.AddPlayer("Shine", "WEM", 5, 29);
            round.AddPlayer("Sky", "HWA", 1, 6);
            round.AddPlayer("Skyhigh", "CJE", 5, 23);
            round.AddPlayer("Smile_WEM", "WEM", 2, 8);
            round.AddPlayer("Snow", "CJE", 5, 31);
            round.AddPlayer("Someday", "KTR", 1, 6);
            round.AddPlayer("Soo_SKT", "SKT", 2, 12);
            round.AddPlayer("Soo_STX", "STX", 1, 9);
            round.AddPlayer("Soulkey", "WJN", 3, 16);
            round.AddPlayer("Spear", "HWA", 1, 6);
            round.AddPlayer("Special", "WJN", 1, 4);
            round.AddPlayer("Sse", "STX", 1, 9);
            round.AddPlayer("Stats", "KTR", 4, 29);
            round.AddPlayer("Stork", "SAM", 6, 23);
            round.AddPlayer("SungEun", "HIT", 1, 4);
            round.AddPlayer("Sungsun", "SKT", 1, 6);
            round.AddPlayer("Suny", "KTR", 1, 6);
            round.AddPlayer("Tears", "WEM", 2, 8);
            round.AddPlayer("Tempest", "KTR", 2, 9);
            round.AddPlayer("Thezerg", "SKT", 1, 6);
            round.AddPlayer("Thunder", "HWA", 1, 6);
            round.AddPlayer("Tiny", "MBC", 1, 4);
            round.AddPlayer("TossGirl", "STX", 1, 9);
            round.AddPlayer("TossLove", "SKT", 1, 6);
            round.AddPlayer("Trap", "STX", 1, 9);
            round.AddPlayer("Turn", "SAM", 1, 5);
            round.AddPlayer("Violet", "KTR", 5, 16);
            round.AddPlayer("Wizard", "HWA", 1, 6);
            round.AddPlayer("Wrath", "WJN", 1, 4);
            round.AddPlayer("Wuk", "EST", 1, 5);
            round.AddPlayer("Xellos", "ACE", 0, 1);
            round.AddPlayer("Ych", "STX", 1, 9);
            round.AddPlayer("Yellow", "ACE", 0, 4);
            round.AddPlayer("YeongJae", "SKT", 1, 6);
            round.AddPlayer("Yoon", "SAM", 1, 5);
            round.AddPlayer("Zerg[kaL]", "SKT", 1, 25);
            round.AddPlayer("Zero", "WJN", 5, 6);
            round.AddPlayer("Zero_WEM", "WEM", 2, 20);

            return round;
        }

        #endregion
    }
}
