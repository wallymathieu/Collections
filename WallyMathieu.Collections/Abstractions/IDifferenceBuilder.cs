using System.Collections.Generic;

namespace WallyMathieu.Collections.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDifferenceBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDataIncomingDifferenceBuilder<T> Existing<T>(ISet<T> existing);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDataIncomingDifferenceBuilder<TKey, TExisting> Existing<TKey, TExisting>(IDictionary<TKey, TExisting> existing);
    }
}
