using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DependencyInjection
{
    public class DependenciesConfiguration
    {
        private ConcurrentDictionary<Type, List<Type>> dictionary;

        public DependenciesConfiguration()
        {
            dictionary = new ConcurrentDictionary<Type, List<Type>>();
        }

        public bool Register<TDependency, TImplementation>()
        {
            return Register(typeof(TDependency), typeof(TImplementation));
        }

        public bool Register(Type tDependency, Type tImplementation)
        {
            if (!tImplementation.IsInterface && !tImplementation.IsAbstract)
            {

            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
