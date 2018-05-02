using Logging;
using MyRobotSimulator.Interface;

namespace MyRobotSimulator.Implementation
{
    /// <summary>
    /// RobotAction class.  Receives instructions from the mover, validates them and carries them out.
    /// Maintains internal state for position and facing direction.
    /// </summary>
    public class RobotAction : IRobotAction
    {
        private readonly ILogger _logger;
        public RobotAction(ILogger logger)
        {
            _logger = logger;
            Error = "";
        }

        private const int TableSize = 5;
        private int? _x;
        private int? _y;
        private Facing _facing;

       public string Error { get; set; }

        public bool Place(int x, int y, Facing facing)
        {
           // _logger.Info(string.Format("Place Command with inputs x {0}, y {1}, facing {2}",x,y,facing ));
            if (!MandateIsOnTable(x, y, "placed")) return false;
            _x = x;
            _y = y;
            _facing = facing;
            return true;
        }

        public bool Move()
        {
           // _logger.Info("In Move Command");
            if (!MandateIsPlaced("move")) return false;
            var newx = GetXAfterMove();
            var newy = GetYAfterMove();
            if (!MandateIsOnTable(newx, newy, "moved")) return false;
            _x = newx;
            _y = newy;
           // _logger.Info($"Move Successfull, New values are x {_x}, y {_y}");

            return true;
        }

       public bool Left()
        {
           // _logger.Info("In Left Command ");

            return Turn(Direction.Left);
        }

        public bool Right()
        {
           // _logger.Info("In Right Command ");

            return Turn(Direction.Right);
        }

       
        public string Report()
        {
          //  _logger.Info("In Report Command ");
            return MandateIsPlaced("report it's position")
                    ? $"{_x.Value},{_y.Value},{_facing.ToString().ToUpper()}"
                    : "Robot's position cannot be reported until it has been placed on the table.";
            
        }

        private int GetXAfterMove()
        {
            if (_facing != Facing.East)
            {
                if (_facing == Facing.West)
                {
                    return _x.Value - 1;
                }
            }
            else
            {
                return _x.Value + 1;
            }

            return _x.Value;
        }

        private int GetYAfterMove()
        {
            if (_facing != Facing.North)
            {
                if (_facing == Facing.South)
                {
                    return _y.Value - 1;
                }
            }
            else
            {
                return _y.Value + 1;
            }

            return _y.Value;
        }

        private bool MandateIsPlaced(string action)
        {
            if (_x.HasValue && _y.HasValue) return true;
            Error = $"Robot cannot {action} until it has been placed on the table.";
            return false;
        }

        private bool MandateIsOnTable(int x, int y, string action)
        {
            if (x >= 0 && y >= 0 && x <= TableSize && y <= TableSize) return true;
            Error = $"Robot cannot be {action} there.";
            return false;
        }

        private bool Turn(Direction direction)
        {
            if (!MandateIsPlaced("turn")) return false;
            var facingAsNumber = (int)_facing;
            facingAsNumber += 1 * (direction == Direction.Right ? 1 : -1);
            if (facingAsNumber == 5) facingAsNumber = 1;
            if (facingAsNumber == 0) facingAsNumber = 4;
            _facing = (Facing)facingAsNumber;
            return true;
        }
    }
}