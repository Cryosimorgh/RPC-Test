using System.Collections.Generic;

namespace Code
{
    /// <summary>
    /// A circular iterator for iterating over a list of items.
    /// </summary>
    /// <typeparam name="T">The type of items in the list.</typeparam>
    public class CircularIterator<T>
    {
        private List<T> _items;
        private int _currentIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularIterator{T}"/> class.
        /// </summary>
        /// <param name="items">The list of items to iterate over. Must not be null.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="items"/> is null.</exception>
        public CircularIterator(List<T> items)
        {
            _items = items ?? throw new System.ArgumentNullException(nameof(items));
            _currentIndex = 0;
        }

        /// <summary>
        /// Returns the next item in the list and advances the iterator to the next item.
        /// If the end of the list is reached, the iterator loops back to the start.
        /// </summary>
        /// <returns>The next item in the list.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when the list is empty.</exception>
        public T Next()
        {
            if (_items.Count == 0)
                throw new System.InvalidOperationException("The collection is empty.");

            _currentIndex = (_currentIndex + 1) % _items.Count;  // Loop back to the start when the end is reached
            T currentItem = _items[_currentIndex];
            return currentItem;
        }
    }

}