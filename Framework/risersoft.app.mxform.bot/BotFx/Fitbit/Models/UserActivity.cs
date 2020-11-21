using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace risersoft.app.mxform.cog.BotFx.Fitbit.Models
{
    public partial class UserActivity
    {
        public List<ActivityInfo> Activities { get; set; }
        public GoalsInfo Goals { get; set; }
        public SummaryInfo Summary { get; set; }
    }

    public partial class ActivityInfo
    {
        public long ActivityId { get; set; }
        public long ActivityParentId { get; set; }
        public long Calories { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public long Duration { get; set; }
        public bool HasStartTime { get; set; }
        public bool IsFavorite { get; set; }
        public long LogId { get; set; }
        public string Name { get; set; }
        public string StartTime { get; set; }
        public long Steps { get; set; }
    }

    public partial class GoalsInfo
    {
        public long CaloriesOut { get; set; }
        public double Distance { get; set; }
        public long Floors { get; set; }
        public long Steps { get; set; }
    }

    public partial class SummaryInfo
    {
        public long ActivityCalories { get; set; }
        public long CaloriesBmr { get; set; }
        public long CaloriesOut { get; set; }
        public List<DistanceInfo> Distances { get; set; }
        public double Elevation { get; set; }
        public long FairlyActiveMinutes { get; set; }
        public long Floors { get; set; }
        public long LightlyActiveMinutes { get; set; }
        public long MarginalCalories { get; set; }
        public long SedentaryMinutes { get; set; }
        public long Steps { get; set; }
        public long VeryActiveMinutes { get; set; }
    }

    public partial class DistanceInfo
    {
        public string Activity { get; set; }
        public double Distance { get; set; }
    }

}
