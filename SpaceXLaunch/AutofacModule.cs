using Application;
using Autofac;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
{
    public class AutofacModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LaunchpadService>().As<ILaunchpadService>();
            builder.RegisterType<SpaceXInfoService>().As<ILaunchpadRepository>();

        }
    }
}
