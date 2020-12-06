using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public class DependenciesConfiguration
    {
        public ConcurrentDictionary<Type, List<Type>> dependencies { get; }

        public DependenciesConfiguration()
        {
            dependencies = new ConcurrentDictionary<Type, List<Type>>();
        }

        public bool Register<TDependency, TImplementation>(bool isSingleton)
        {
            return Register(typeof(TDependency), typeof(TImplementation), isSingleton);
        }

        public bool Register(Type tDependency, Type tImplementation, bool isSingleton)
        {
            bool result = true;

            if (!tImplementation.IsInterface && !tImplementation.IsAbstract)
            {
                dependencies.TryAdd(tDependency, new List<Type>());

                if (!dependencies[tDependency].Contains(tImplementation))
                {
                    dependencies[tDependency].Add(tImplementation);
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
