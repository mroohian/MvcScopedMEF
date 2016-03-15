using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using System.IO;
using System.Linq;
using System.Reflection;
using DepInj.Contract;
using DepInj.Contract.Attributes;

namespace DepInj {
    // ReSharper disable once InconsistentNaming
    public sealed class DI {
        private readonly CompositionHost _container;

        public DI(List<Assembly> assemblies) {

            var conventionBuilderRuleProviders = assemblies.SelectMany(a => a.GetTypes())
                      .Where(t => t.GetCustomAttribute(typeof(ConventionBuilderRuleProviderAttribute), false) != null)
                      .Select(t => Activator.CreateInstance(t) as IConventionBuilderRuleProvider).ToList();

            // Add convention builder rules
            var conventions = new ConventionBuilder();
            conventionBuilderRuleProviders.ForEach(provider => provider.AddRules(conventions));

            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies, conventions);

            _container = configuration.CreateContainer();
        }

        public CompositionHost GetContainer() {
            return _container; 
        }

        public static List<Assembly> AssembliesInDirectory(string assemblyDir) {
            return Directory.GetFiles(assemblyDir, "*.dll")
                            .Union(Directory.GetFiles(assemblyDir, "*.exe"))
                            .Select(Assembly.LoadFrom)
                            .ToList();
        }
    }
}