using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public class DependencyProvider
    {
        private DependenciesConfiguration _configuration;

        public DependencyProvider(DependenciesConfiguration configuration)
        {
            _configuration = configuration;
        }

        private bool ValidateConfiguration(DependenciesConfiguration configuration)
        {
            foreach (Type tDependency in configuration.dictionary.Keys)
            {
                if (!tDependency.IsValueType)
                {
                    foreach (Type tImplementation in configuration.dictionary[tDependency])
                    {
                        if (tImplementation.IsAbstract || tImplementation.IsInterface)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private object CreateGeneric(Type t)
        {
            List<Type> implementations;
            Type tResolve = t.GetGenericArguments()[0];

            _configuration.dictionary.TryGetValue(tResolve, out implementations);
            if (implementations != null)
            {
                var result = Activator.CreateInstance(typeof(List<>).MakeGenericType(tResolve));
                foreach (Type tImplementation in implementations)
                {

                }
                return result;
            }
            return null;
        }
    }
}
