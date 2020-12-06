using DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;

namespace DependencyInjectionTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void SingletonTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<ITest, Test1>(true);
            provider = new DependencyProvider(config);
            ITest expected = provider.Resolve<ITest>();
            ITest actual = provider.Resolve<ITest>();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InstancePerDependencyTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<ITest, Test1>(false);
            provider = new DependencyProvider(config);
            ITest expected = provider.Resolve<ITest>();
            ITest actual = provider.Resolve<ITest>();

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void CreateDependencyTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<ITest, Test1>(false);
            config.Register<ITest, Test2>(true);
            provider = new DependencyProvider(config);
            ITest actual = provider.Resolve<ITest>();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void AsSelfRegistrationTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<Test2, Test2>(true);
            provider = new DependencyProvider(config);
            Test2 actual = provider.Resolve<Test2>();

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void CycleDependencyTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<Test1, Test1>(false);
            config.Register<Test2, Test2>(true);
            config.Register<Test3, Test3>(true);

            provider = new DependencyProvider(config);
            Test1 actual = provider.Resolve<Test1>();
            Assert.IsNotNull(actual);
            Assert.AreEqual(null, actual.example.example.example);
        }

        [TestMethod]
        public void GenericTypeTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<ISomeInterface, SomeClass>(true);
            config.Register<SomeClass2<ISomeInterface>, SomeClass2<ISomeInterface>>(false);
            provider = new DependencyProvider(config);
            SomeClass2<ISomeInterface> actual = provider.Resolve<SomeClass2<ISomeInterface>>();
            Assert.IsNotNull(actual);
            Assert.AreEqual(5, actual.GetNum());
        }

        [TestMethod]
        public void OpenGenericTypeTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<ISomeInterface, SomeClass>(false);
            config.Register(typeof(SomeClass2<>), typeof(SomeClass2<>), false);
            provider = new DependencyProvider(config);
            SomeClass2<ISomeInterface> actual = provider.Resolve<SomeClass2<ISomeInterface>>();
            Assert.IsNotNull(actual);
            Assert.AreEqual(5, actual.GetNum());
        }

        //checks if TImlementaton implements TDependency
        [TestMethod]
        public void IncompatibilityTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<Test1, Test3>(true);
            try
            {
                provider = new DependencyProvider(config);
                Test2 actual = provider.Resolve<Test2>();
                Assert.IsNotNull(actual);
            }
            catch (ConfigurationValidationException ex)
            {
                Assert.IsNotNull(ex.Message);
            }
        }

        //Try to create not registred type
        [TestMethod]
        public void NotRegisteredTypeTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<ITest, Test1>(true);
            try
            {
                provider = new DependencyProvider(config);
                Test2 actual = provider.Resolve<Test2>();
                Assert.IsNotNull(actual);
            }
            catch (TypeNotRegisterException ex)
            {
                Assert.IsNotNull(ex.Message);
            }
        }

        //Try to add dependency : interface -> interface
        [TestMethod]
        public void ImplementationIsInterfaceTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<ITest, ITest>(true);
            try
            {
                provider = new DependencyProvider(config);
                ITest actual = provider.Resolve<ITest>();
                Assert.IsNotNull(actual);
            }
            catch (ConfigurationValidationException ex)
            {
                Assert.IsNotNull(ex.Message);
            }
        }

        //Returns some implementations of one dependency
        [TestMethod]
        public void GetSomeImplementationsTest()
        {
            DependencyProvider provider;
            DependenciesConfiguration config = new DependenciesConfiguration();

            config.Register<ITest, Test1>(true);
            config.Register<ITest, Test2>(false);
            provider = new DependencyProvider(config);
            IEnumerable<ITest> actual = provider.Resolve<IEnumerable<ITest>>();

            Assert.IsNotNull(actual);
            Assert.AreEqual(2, ((IList)actual).Count);
        }
    }
}
