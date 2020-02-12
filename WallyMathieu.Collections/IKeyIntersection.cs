namespace WallyMathieu.Collections
{
    /// <summary>
    /// Intersection values where the keys match 
    /// </summary>
    public interface IKeyIntersection<out TKey, out TLeft, out TRight>
    {
        /// <summary>
        /// The matching key
        /// </summary>
        TKey Key { get; }
        /// <summary>
        /// Right element with matching key
        /// </summary>
        TRight Right { get; }
        /// <summary>
        /// Left element with matching key
        /// </summary>
        TLeft Left { get; }
    }
}
