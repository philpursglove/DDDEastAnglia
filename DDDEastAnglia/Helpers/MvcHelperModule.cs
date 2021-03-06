﻿using Ninject.Modules;
using Ninject.Extensions.Conventions;

namespace DDDEastAnglia.Helpers
{
    /// <summary>
    /// This by default binds all classes in this namespace to their appropriate interface
    /// in the singleton scope as the helpers should roughly be independent of databases
    /// etc
    /// </summary>
    public class MvcHelperModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(from => from.FromThisAssembly()
                                    .SelectAllClasses()
                                    .InNamespaceOf<IControllerInformationProvider>()
                                    .Excluding<AlphabeticalSort>()
                                    .Excluding<RandomSort>()
                                    .BindDefaultInterfaces()
                                    .Configure(binding => binding.InSingletonScope()));
            Kernel.Bind<ISortAlgorithm>()
                  .To<AlphabeticalSort>()
                  .When(request => request.Target.Name.StartsWith("default"));

            Kernel.Bind<ISortAlgorithm>()
                  .To<RandomSort>()
                  .When(request => request.Target.Name.StartsWith("voting"));

            Kernel.Bind<IResetPasswordThingy>().To<WebSecurityWrapper>();
        }
    }
}