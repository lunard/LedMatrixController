using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using LedMatrixController.Services.Impl;
using LedMatrixController.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LedMatrixController.Infrastructure.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ImageService>().As<IImageService>().InstancePerLifetimeScope();
            builder.RegisterType<Ws2812b_LedMatrixService>().As<ILedMatrixService>().SingleInstance();
            builder.RegisterType<LedMatrixLayoutService>().As<ILedMatrixLayoutService>().InstancePerLifetimeScope();
        }
    }
}
