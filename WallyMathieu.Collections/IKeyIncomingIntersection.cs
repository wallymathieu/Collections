namespace WallyMathieu.Collections
{
    /// <summary>
    /// Intersection values where the keys match 
    /// </summary>
    public interface IKeyIncomingIntersection<out TKey, out TIncoming, out TExisting>
    {
        /// <summary>
        /// The matching key
        /// </summary>
        TKey Key { get; }
        /// <summary>
        /// Right element with matching key
        /// </summary>
        TExisting Existing { get; }
        /// <summary>
        /// Left element with matching key
        /// </summary>
        TIncoming Incoming { get; }
    }
}
