using System.Reflection;

namespace Moq.Strategies
{
    /// <summary>
    /// Represents a strategy to mock dependencies on an object
    /// </summary>
    public interface IAutoMockingStrategy
    {
        /// <summary>
        /// Mocks the specified property on the object.
        /// </summary>
        /// <param name="toMock">To mock.</param>
        /// <param name="property">The property.</param>
        /// <returns>
        /// null if the property wasn't mocked
        /// </returns>
        object Mock(object toMock, PropertyInfo property);
    }
}
