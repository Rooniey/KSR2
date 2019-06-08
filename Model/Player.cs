using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace Model
{
    public class Player
    {
        [Name("ID")]
        public int Id { get; set; }

        public string Name { get; set; }

        [Name("Overall")]
        public double OverallRating { get; set; }

        [Name("Potential")]
        public double PotentialRating { get; set; }

        public double HeadingAccuracy { get; set; }
        public double ShortPassing { get; set; }
        public double Volleys { get; set; }
        public double Dribbling { get; set; }
        public double Curve { get; set; }
        public double FKAccuracy { get; set; }
        public double LongPassing { get; set; }
        public double BallControl{ get; set; }
        public double Agility { get; set; }
        public double Reactions { get; set; }
        public double Balance { get; set; }
        public double ShotPower { get; set; }
        public double Jumping { get; set; }
        public double Stamina { get; set; }
        public double Strength { get; set; }
        public double LongShots { get; set; }
        public double Aggression { get; set; }
        public double Interceptions { get; set; }
        public double Positioning { get; set; }
        public double Vision { get; set; }
        public double Penalties { get; set; }
        public double Composure { get; set; }
        public double Marking { get; set; }
        public double StandingTackle { get; set; }
        public double GKDiving { get; set; }
        public double GKHandling { get; set; }
        public double GKKicking { get; set; }
        public double GKPositioning { get; set; }
        public double GKReflexes { get; set; }
        public double SprintSpeed { get; set; }
        public double Acceleration { get; set; }
        public double Crossing { get; set; }
        public double Finishing { get; set; }
        public double SlidingTackle { get; set; }

        public double Get(string property)
        {
            return (double)typeof(Player).GetProperty(property).GetValue(this);
        }
    }
}
