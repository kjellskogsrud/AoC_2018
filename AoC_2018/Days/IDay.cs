using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2018.Days
{
    interface IDay
    {
        string Title { get; set; }
        void SolvePart1();
        void SolvePart2();

    }
}
