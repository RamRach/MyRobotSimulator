using System;
using System.Reflection;
using Logging;
using MyRobotSimulator.Implementation;
using MyRobotSimulator.Interface;
using Ninject;

namespace 
    MyRobotSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayWelcome();

            var kernel = new StandardKernel();
            LogHandler.InitialiseLoggingConfig("RobotAction Simulator", true);
            kernel.Load(Assembly.GetExecutingAssembly());
            
            var robotMover = new RobotCommander(kernel.Get<ILogger>(), kernel.Get<IRobotAction>());

            while (true)
            {
                var command = PromptForCommand();
                if (command.ToUpper() == "EXIT" || command.ToUpper() == "QUIT")
                {
                    Environment.Exit(0);
                }
                Console.WriteLine(robotMover.Command(command));
                Console.WriteLine("");
            }
        }

        private static void DisplayWelcome()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Hi There, Welcome to My RobotAction Simulator");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("");
        }
  
        private static string PromptForCommand()
        {

           Console.WriteLine("Enter a Command # ");
            return Console.ReadLine();
        }


    }
}
