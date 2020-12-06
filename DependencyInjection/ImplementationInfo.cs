using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionContainer
{
    public class ImplementationInfo
    {
        public Type implementationType { get; }
        public bool isSingleton { get; }
        public object instance { get; }

        public ImplementationInfo(Type dependencyType, bool isSingleton)
        {
            this.implementationType = dependencyType;
            this.isSingleton = isSingleton;
            instance = null;
        }
    }
}