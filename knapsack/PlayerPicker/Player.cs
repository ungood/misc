using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayerPicker
{
    public class Player
    {
        public string Name { get; private set; }
        public Team Team { get; private set; }
        public int TeamId
        {
            get { return Team.Id; }
        }
        public int Cost { get; private set; }
        public int Score { get; private set; }


        public bool IsDummyPlayer { get; set; }

        public Player(Team team, string name, int cost, int score)
        {
            Team = team;
            Name = name;
            Cost = cost;
            Score = score;
        }

        public double SelectedValue { get; set; }

        public bool IsSelected
        {
            get { return SelectedValue > 0; }
        }

        public new string ToString()
        {
            return string.Format("{1} - {0}: {2}/{3}", Name.PadRight(14), Team.Name, Cost, Score);
        }
    }
}
