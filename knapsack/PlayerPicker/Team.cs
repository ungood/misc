using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlayerPicker
{
    public class Team
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Cost { get; private set; }
        public string Score { get; private set; }

        public Team(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
