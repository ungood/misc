using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnapsackSolver
{
    public class KnapsackItem
    {
        public int Profit { get; private set; }
        public int Weight { get; private set; }
        //public float ProfitPerWeight { get; private set; }

        internal double SelectedValue { get; set; }

        public bool IsSelected
        {
            get { return SelectedValue > 0; }
        }

        public KnapsackItem(int profit, int weight)
        {
            Profit = profit;
            Weight = weight;
            //ProfitPerWeight = ((float) profit) / weight;
        }
    }
}
