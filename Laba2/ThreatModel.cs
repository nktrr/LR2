using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2
{
    public class ThreatModel
    {
        public int Identificator { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string ThreatSource { get; set; }

        public string Target { get; set; }

        public List<string> Breaches { get; set; }

        public string BreachesForTable { get { return Breaches.Aggregate((a, b) => a + "\n" + b); }}

        public DateTime InclusionDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public ThreatModel PreviousVersion { get; set; }

        public ThreatModel(int identificator, string name, string description, string threatSource, string target, List<string> breaches, DateTime inclusionDate, DateTime updateDate)
        {
            Identificator = identificator;
            Name = name;
            Description = description;
            ThreatSource = threatSource;
            Target = target;
            Breaches = breaches;
            InclusionDate = inclusionDate;
            UpdateDate = updateDate;
        }

        public bool Equals(ThreatModel b)
        {
            if (Name != b.Name) return false;
            if (Description != b.Description) return false;
            if (ThreatSource != b.ThreatSource) return false;
            if (Target != b.Target) return false;
            if (!Breaches.All(x => b.Breaches.Contains(x)) || !b.Breaches.All(x => Breaches.Contains(x))) return false;
            if (UpdateDate != b.UpdateDate) return false;
            return true;
        }
    }
}
