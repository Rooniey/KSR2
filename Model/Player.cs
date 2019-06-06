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

        public String Name { get; set; }

        [Name("Overall")]
        public double OverallRating { get; set; }

        public int Age { get; set; }

        public double FKAccuracy { get; set; }

        public double SprintSpeed { get; set; }

        public double Acceleration { get; set; }

        public double Crossing { get; set; }

        public double Finishing { get; set; }

        public double SlidingTackle { get; set; }

        public double StandingTackle { get; set; }

        public double Get(string property)
        {
            return (double)typeof(Player).GetProperty(property).GetValue(this);
        }
    }
}
