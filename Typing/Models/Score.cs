using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typing.Models
{
    internal class Score
    {
        public int WPM { get; set; }
        public double Accuracy { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
