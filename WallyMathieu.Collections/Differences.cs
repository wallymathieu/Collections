using WallyMathieu.Collections.Abstractions;
using WallyMathieu.Collections.Internals;

namespace WallyMathieu.Collections
{
    /// <summary>
    /// 
    /// </summary>
    public static class Differences
    {
        /// <summary>
        /// Start composing difference
        /// </summary>
        /// <returns></returns>
        public static IDifferenceBuilder Of() => new DifferenceBuilder();
    }
}
