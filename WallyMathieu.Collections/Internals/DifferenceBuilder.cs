using System.Collections.Generic;
using WallyMathieu.Collections.Abstractions;

namespace WallyMathieu.Collections.Internals
{
    class DifferenceBuilder : IDifferenceBuilder
    {
        /// <summary>
        /// Compare two dictionaries with potentially different values for key differences.
        /// </summary>
        /// <typeparam name="TExisting"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="left"></param>
        /// <returns>A comparison builder</returns>
        public IDataIncomingDifferenceBuilder<TKey, TExisting> Existing<TKey, TExisting>(
            IDictionary<TKey, TExisting> left)
        {
            return new DataIncomingDifferenceBuilder<TKey, TExisting>(left);
        }

        /// <summary>
        /// Compare two sets.
        /// </summary>
        /// <example>
        /// </example>
        public IDataIncomingDifferenceBuilder<T> Existing<T>(ISet<T> existing)
        {
            return new DataIncomingDifferenceBuilder<T>(existing);
        }
    }
}
