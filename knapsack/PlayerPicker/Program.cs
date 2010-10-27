using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SolverFoundation.Services;

namespace PlayerPicker
{
    public class Program
    {
        static void Main(string[] args)
        {
            var round = FantasyRound.CreateRound8();

            Console.WriteLine(round.Name);
            Console.WriteLine("Num Players: " + round.Players.Count());
            Console.WriteLine();

            var picker = new TeamPicker { Round = round };
            
            var team = picker.SelectTeam();
            Console.WriteLine("Team".PadRight(40, '='));
            foreach(var player in team)
                Console.WriteLine(player.ToString());

            var teamScore = team.Sum(player => player.Score);
            Console.WriteLine("Team Score".PadRight(20) + ": " + teamScore);
            Console.WriteLine();

            var antiteam = picker.SelectAntiTeam();
            Console.WriteLine("Anti-Team".PadRight(40, '='));
            foreach(var player in antiteam)
                Console.WriteLine(player.ToString());

            var antiteamScore = antiteam.Sum(player => player.Score);
            Console.WriteLine("Anti-Team Score".PadRight(20) + ": " + antiteamScore);
            Console.WriteLine();
            
            var score = teamScore - antiteamScore;
            Console.WriteLine("Total Score".PadRight(20) + ": " + score);
        }
    }
}
