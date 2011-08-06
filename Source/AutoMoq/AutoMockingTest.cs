using System;
using System.Collections.Generic;
using System.Reflection;
using Moq.Strategies;

namespace Moq
{
    /// <summary>
    /// Automatically Mock public dependencies
    /// </summary>
    public abstract class AutoMockingTest
    {
        private readonly IDictionary<Type, Object> mocks = new Dictionary<Type, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoMockingTest"/> class.
        /// </summary>
        public AutoMockingTest()
        {
            Strategies = new List<IAutoMockingStrategy>();

            Strategies.Add(new PublicInterfaceStrategy());
        }

        /// <summary>
        /// Retrieves the mock of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected Mock<T> Mock<T>() where T : class
        {
            var type = typeof(T);

            return (Mock<T>) mocks[type];
        }

        /// <summary>
        /// Gets the auto mocking strategies.
        /// </summary>
        public IList<IAutoMockingStrategy> Strategies { get; private set; }

        /// <summary>
        /// Creates an object of the specified type.
        /// </summary>
        /// <typeparam name="T">A type to create.</typeparam>
        /// <returns>
        /// Object of the type <typeparamref name="T"/>.
        /// </returns>
        /// <remarks>Usually used to create objects to test. Any non-existing dependencies
        /// are mocked.
        /// <para>Container is used to resolve build dependencies.</para></remarks>
        protected T Create<T>() where T : class, new()
        {
            var result = new T();

            mocks.Clear();

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

            foreach (var property in properties)
            {
                foreach (var strategy in Strategies)
                {
                    var mock = strategy.Mock(result, property);

                    if (mock == null || mocks.ContainsKey(property.PropertyType)) continue;

                    mocks.Add(property.PropertyType, mock);

                    break;
                }                
            }

            return result;
        }
    }
}
