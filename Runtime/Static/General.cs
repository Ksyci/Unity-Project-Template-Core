using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.Events;

namespace ProjectTemplate
{
    /// <summary>
    /// Provides general-purpose utility methods for collection manipulation and safe access.
    /// </summary>
    public static class General
    {
        #region Methods

        /// <summary>
        /// Safely retrieves the element at the specified index from the list,
        /// or returns the default value if the index is out of range.
        /// </summary>
        /// <typeparam name="T">Type of elements in the list.</typeparam>
        /// <param name="list">The list to access.</param>
        /// <param name="index">The index of the element to retrieve.</param>
        /// <returns>The element at the given index or default if out of range.</returns>
        public static T FindAt<T>(IList<T> list, int index)
            => index < 0 || index >= list.Count ? default : list[index];

        /// <summary>
        /// Safely retrieves the value associated with the specified key from the dictionary,
        /// or returns the default value if the key is not found.
        /// </summary>
        /// <typeparam name="TKey">Type of the dictionary keys.</typeparam>
        /// <typeparam name="TValue">Type of the dictionary values.</typeparam>
        /// <param name="dict">The dictionary to search.</param>
        /// <param name="key">The key whose value to retrieve.</param>
        /// <returns>The value associated with the key or default if the key is not found.</returns>
        public static TValue FindWith<TKey, TValue>(IDictionary<TKey, TValue> dict, TKey key)
        {
            try
            {
                return dict[key];
            }
            catch (KeyNotFoundException)
            {
                return default;
            }
        }

        /// <summary>
        /// Find a <see cref="Type"/> with a name;
        /// </summary>
        /// <param name="name">The name of the <see cref="Type"/>.</param>
        /// <returns></returns>
        public static Type GetType(string name)
        {
            string fullName = $"{nameof(ProjectTemplate)}.{name}";

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type type = assembly.GetType(fullName);

                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }

        /// <summary>
        /// Randomly shuffles the elements of the specified array in place.
        /// </summary>
        /// <typeparam name="T">The type of the array's elements.</typeparam>
        /// <param name="array">The array to shuffle.</param>
        public static void Shuffle<T>(IList<T> array)
        {
            Random random = new();

            for (int i = array.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }
        }

        #endregion
    }
}