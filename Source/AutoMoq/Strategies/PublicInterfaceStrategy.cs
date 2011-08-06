using System;
using System.Reflection;

namespace Moq.Strategies
{
    /// <summary>
    /// Strategy to mock public interfaces
    /// </summary>
    public class PublicInterfaceStrategy : IAutoMockingStrategy
    {
        /// <summary>
        /// Mocks the specified property on the object.
        /// </summary>
        /// <param name="toMock">To mock.</param>
        /// <param name="property">The property.</param>
        /// <returns>
        /// null if the property wasn't mocked
        /// </returns>
        public object Mock(object toMock, PropertyInfo property)
        {
            object mock = null;

            if (property.PropertyType.IsInterface)
            {
                mock = Activator.CreateInstance(typeof(Mock<>).MakeGenericType(property.PropertyType));

                var obj = mock.GetType().GetProperty("Object", property.PropertyType).GetValue(mock, null);

                property.SetValue(toMock, obj, null);                
            }

            return mock;
        }
    }
}
