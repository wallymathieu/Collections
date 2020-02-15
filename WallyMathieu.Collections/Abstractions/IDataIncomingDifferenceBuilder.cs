using System;
using System.Collections.Generic;
using System.Linq;

namespace WallyMathieu.Collections.Abstractions
{

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataIncomingDifferenceBuilder<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDataDifference<T> Incoming(ISet<T> incoming);
    }
    /// <summary>
    /// 
    /// </summary>
    public interface IDataIncomingDifferenceBuilder<TKey, TExisting>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IDataDifference<TKey, TIncoming, TExisting> Incoming<TIncoming>(IDictionary<TKey, TIncoming> incoming);
    }
}
