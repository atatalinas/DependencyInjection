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
            if (ValidateConfiguration(configuration))
            {
                _configuration = configuration;
            }
            else
            {
                throw new Exception("Configuration is not valid");
            }
        }

        private bool ValidateConfiguration(DependenciesConfiguration configuration)
        {
            foreach (Type tDependency in configuration.dependencies.Keys)
            {
                if (!tDependency.IsValueType)
                {
                    foreach (Type tImplementation in configuration.dependencies[tDependency])
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

            _configuration.dependencies.TryGetValue(tResolve, out implementations);
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
