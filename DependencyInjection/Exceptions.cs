using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
    public class ConfigurationValidationException : Exception
    {
        public ConfigurationValidationException(string message) : base(message) { }
    }
    public class CycleDependencyException : Exception
    {
        public CycleDependencyException(string message) : base(message) { }
    }

    public class ConstructorNotFoundException : Exception
    {
        public ConstructorNotFoundException(string message) : base(message) { }
    }

    public class TypeNotRegisterException : Exception
    {
        public TypeNotRegisterException(string message) : base(message) { }
    }
}
