using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging;
using MyRobotSimulator.Implementation;
using MyRobotSimulator.Interface;
using Ninject.Modules;

namespace MyRobotSimulator.App_Start
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().ToMethod(m => LogHandler.InitializeLoggerWithCorrelationId(Guid.NewGuid().ToString())).InTransientScope();
            Bind<IRobotAction>().To<RobotAction>().InSingletonScope();
            Bind<IRobotCommander>().To<RobotCommander>().InSingletonScope();

        }
    }
}
