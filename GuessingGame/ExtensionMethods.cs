namespace GuessingGame
{
    using System;

    public static class ExtensionMethods
    {
        /// <summary>
        ///     Shared instance of the random class
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        ///     Select a Random item from an Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T Random<T>(this T[] array)
        {
            if (array != null)
            {
                return array[random.Next(0, array.Length - 1)];
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}