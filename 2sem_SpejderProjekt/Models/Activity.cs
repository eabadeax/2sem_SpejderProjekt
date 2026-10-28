using System;
using System.Collections.Generic;
using System.Text;

namespace _2sem_SpejderProjekt.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public DateTime Date { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public int MaxParticipants { get; set; }

        public int? MinimumAge { get; set; }

        public int? MaximumAge { get; set; }
    }
}

