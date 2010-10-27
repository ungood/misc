using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SolverFoundation.Services;

namespace PlayerPicker
{
    public class TeamPicker
    {
        public FantasyRound Round { get; set; }

        #region Model

        private SolverContext context;
        private Model model;
        private Set items;
        private Parameter cost;
        private Parameter score;
        private Parameter team;
        private Parameter dummy;
        private Decision choose;

        private void CreateModel()
        {
            context = SolverContext.GetContext();
            context.ClearModel();
            model = context.CreateModel();
            items = new Set(Domain.Any, "items");

            cost = new Parameter(Domain.Integer, "cost", items);
            cost.SetBinding(Round.Players, "Cost", "Name");
            
            score = new Parameter(Domain.Integer, "score", items);
            score.SetBinding(Round.Players, "Score", "Name");
            
            team = new Parameter(Domain.IntegerNonnegative, "team", items);
            team.SetBinding(Round.Players, "TeamId", "Name");

            dummy = new Parameter(Domain.Boolean, "dummy", items);
            dummy.SetBinding(Round.Players, "IsDummyPlayer", "Name");

            model.AddParameters(cost, score, team, dummy);

            choose = new Decision(Domain.IntegerRange(0, 1), "choose", items);
            choose.SetBinding(Round.Players, "SelectedValue", "Name");
            model.AddDecision(choose);
        }

        #endregion

        public IEnumerable<Player> SelectTeam()
        {
            CreateModel();

            // Constraint: Players on Team
            var totalItems = Model.Sum(Model.ForEach(items, i => choose[i]));
            model.AddConstraint("size", totalItems == (Round.TeamSize + 1)); // +1 to account for the Team selection.

            // Constraint: Exactly one DummyTeamPlayer
            var totalDummies = Model.Sum(Model.ForEach(items, i => choose[i] * dummy[i]));
            model.AddConstraint("oneDummy", totalDummies == 1);

            // Constraint: Max one from each team
            //model.AddConstraint("uniqueTeam", Model.AllDifferent(Model.ForEach(items, i => choose[i] * team[i])));

            // Constraint: Total Cost <= 30
            var totalCost = Model.Sum(Model.ForEach(items, i => (choose[i] * cost[i])));
            model.AddConstraint("capacity", totalCost <= Round.TeamCost);

            // Goal: Maximize Score
            var totalProfit = Model.Sum(Model.ForEach(items, i => (choose[i] * score[i])));
            model.AddGoal("maxProfit", GoalKind.Maximize, totalProfit);

            context.Solve();
            context.PropagateDecisions();

            return Round.Players.Where(player => player.IsSelected);
        }

        public IEnumerable<Player> SelectAntiTeam()
        {
            CreateModel();

            // Constraint: No DummyTeamPlayer
            var totalDummies = Model.Sum(Model.ForEach(items, i => choose[i] * dummy[i]));
            model.AddConstraint("oneDummy", totalDummies == 0);

            // Constraint: Number of Players on Team
            var totalItems = Model.Sum(Model.ForEach(items, i => choose[i]));
            model.AddConstraint("size", totalItems == (Round.AntiTeamSize));

            // Constraint: Total Cost
            var totalWeight = Model.Sum(Model.ForEach(items, i => (choose[i] * cost[i])));
            model.AddConstraint("capacity", totalWeight >= Round.AntiTeamCost);

            // Goal: Minimize Score
            var totalProfit = Model.Sum(Model.ForEach(items, i => (choose[i] * score[i])));
            model.AddGoal("minProfit", GoalKind.Minimize, totalProfit);

            context.Solve();
            context.PropagateDecisions();

            return Round.Players.Where(player => player.IsSelected);
        }
    }
}
