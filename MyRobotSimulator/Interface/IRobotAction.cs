using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRobotSimulator.Interface
{
    public interface IRobotAction
    {
        bool Place(int x, int y, Facing facing);
        bool Move();
        bool Left();
        bool Right();
        string Report();
        string Error { get; set; }

    }
}
