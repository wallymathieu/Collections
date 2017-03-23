using System;

namespace Tests
{
    internal static class Extensions
    {  
        /// <summary>
       /// Execute an action on the object. Return value is the object.
       /// </summary>
        public static T Tap<T>(this T value, Action<T> action)
        {
            action?.Invoke(value);
            return value;
        }
    }
}
