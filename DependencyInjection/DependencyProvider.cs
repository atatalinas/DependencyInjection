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
    }
}
