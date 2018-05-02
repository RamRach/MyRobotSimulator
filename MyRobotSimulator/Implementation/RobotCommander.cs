using System;
using Logging;
using MyRobotSimulator.Interface;
using MyRobotSimulator.Model;

namespace MyRobotSimulator.Implementation
{
    /// <summary>
    /// RobotCommander class.  Responsible for receiving and parsing commands and passsing to RobotAction.
    /// </summary>
    public class RobotCommander : IRobotCommander
    {
        private ILogger _logger;
        private readonly IRobotAction _robotAction;
         
        public RobotCommander(ILogger logger,IRobotAction iRobotAction)
        {
            _logger = logger;
            _robotAction = iRobotAction;
        }

        
        public string Command(string command)
        {
            var response = "";
            InstructionArgs args = null;
            var instruction = GetInstruction(command, ref args);

            switch (instruction)
            {
                case Instruction.Place:
                    var placeArgs = (PlaceArgs)args;
                    response = _robotAction.Place(placeArgs.X, placeArgs.Y, placeArgs.Facing) ? "Command Success" : _robotAction.Error;
                    break;
                case Instruction.Move:
                    response = _robotAction.Move() ? "Command Success" : _robotAction.Error;
                    break;
                case Instruction.Left:
                    response = _robotAction.Left() ? "Command Success" : _robotAction.Error;
                    break;
                case Instruction.Right:
                    response = _robotAction.Right() ? "Command Success" : _robotAction.Error;
                    break;
                case Instruction.Report:
                    response = _robotAction.Report();
                    break;
                case Instruction.Invalid:
                    response = @"Invalid command. The correct command formats are as follows:
                    PLACE X, Y, DIRECTION
                    MOVE
                    RIGHT
                    LEFT
                    REPORT
                    -------------
                    Please review your input and try again.";
                    break;
                default:
                    response = @"Invalid command. The correct command formats are as follows:
                    PLACE X, Y, DIRECTION
                    MOVE
                    RIGHT
                    LEFT
                    REPORT
                    -------------
                    Please review your input and try again.";
                    break;
            }
            return response;            
        }

        private static Instruction GetInstruction(string command, ref InstructionArgs args)
        {
            Instruction result;
            var argString = "";

            if (command != null)
            {
                var argsSeperatorPosition = command.IndexOf(" ", StringComparison.Ordinal);
                if (argsSeperatorPosition > 0)
                {
                    argString = command.Substring(argsSeperatorPosition + 1);
                    command = command.Substring(0, argsSeperatorPosition);
                    command = command.ToUpper();
                }
            }

            if (Enum.TryParse(command, true, out result))
            {
                if (result != Instruction.Place) return result;
                if (!TryParsePlaceArgs(argString, ref args))
                {
                    result = Instruction.Invalid;
                }
            }
            else
            {
                result = Instruction.Invalid;
            }
            return result;
        }

        private static bool TryParsePlaceArgs(string argString, ref InstructionArgs args)
        {
            var argParts = argString.Split(','); 
            int x, y;
            Facing facing;

            if (argParts.Length != 3 || !TryGetCoordinate(argParts[0], out x) ||
                !TryGetCoordinate(argParts[1], out y) || !TryGetFacingDirection(argParts[2], out facing)) return false;
            args = new PlaceArgs
            {
                X = x,
                Y = y,
                Facing = facing,
            };
            return true;
        }

        private static bool TryGetCoordinate(string coordinate, out int coordinateValue)
        {
            return int.TryParse(coordinate, out coordinateValue);
        }

        private static bool TryGetFacingDirection(string direction, out Facing facing)
        {
            return Enum.TryParse(direction, true, out facing);
        }
    }    
}
