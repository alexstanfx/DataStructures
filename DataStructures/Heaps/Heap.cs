using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Heaps
{
    public abstract class Heap<T> where T : IComparable
    {
        private const int InitialCapacity = 2;

        protected long heapSize;
        protected T[] array;

        public Heap()
        {
            array = new T[InitialCapacity];
            heapSize = 0;
        }

        public Heap(T[] source)
        {
            array = new T[source.Length + 1];
            heapSize = source.Length;

            Array.Copy(source, 0, array, 1, source.Length);

            BuildHeap();
        }

        /// <summary>
        /// Gets the total number of elements in the heap.
        /// </summary>
        public long Length
        {
            get
            {
                return heapSize;
            }
        }

        /// <summary>
        /// Returns the first element in the heap without removing it from the heap
        /// or default if there is no element.
        /// </summary>
        /// <returns>The first element in the heap</returns>
        public T Peek()
        {
            if (heapSize > 0)
            {
                return array[1];
            }

            return default(T);
        }

        /// <summary>
        /// Returns the smallest element in the heap and removes it
        /// or default if there is no element.
        /// </summary>
        /// <returns>The smallest element in the heap</returns>
        public T Pop()
        {
            T result = default(T);

            if (heapSize > 0)
            {
                result = array[1];
                array[1] = array[heapSize];
                heapSize--;

                BuildHeap();
            }

            return result;
        }

        public abstract void Insert(T element);

        protected abstract void BuildHeap();
    }
}
