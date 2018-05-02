namespace MyRobotSimulator.Model
{
    public class PlaceArgs : InstructionArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Facing Facing { get; set; }
    }
}
