using System;

namespace DataStructures.Heaps
{
    public class MinHeap<T> : Heap<T> where T : IComparable
    {
        /// <summary>
        /// Creates a new instance of the Heap class.
        /// </summary>
        public MinHeap() : base()
        {
        }

        /// <summary>
        /// Creates a new instance of the Heap class from an array of elements.
        /// </summary>
        /// <param name="source">The array of elements to construct the heap from.</param>
        public MinHeap(T[] source) : base(source)
        {
        }

        /// <summary>
        /// Inserts a new element into the heap.
        /// </summary>
        /// <param name="element">The element that will be inserted into the heap.</param>
        public override void Insert(T element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            // If we overflow the array, double its size
            if (heapSize == array.Length - 1)
                Array.Resize(ref array, array.Length * 2);

            heapSize++;
            long pos = heapSize;

            // If the newly inserted element is smaller than its parent (wich sits at index pos/2)
            // then swap it with the parent and check the parent;s parent until the first element is reached.
            while (pos > 1 && element.CompareTo(array[pos / 2]) < 0)
            {
                array[pos] = array[pos / 2];
                pos /= 2;
            }

            array[pos] = element;
        }

        protected override void BuildHeap()
        {
            // Swap children with their parents if the parent's value is larger than the child's.
            // Start from the middle of the array. That will be the last parent.
            for (long i = heapSize / 2; i > 0; i--)
            {
                // get the left child
                long childIndex = i * 2;

                if (childIndex + 1 <= heapSize)
                {
                    // If right child < left child then
                    // mark this child so we know to swap it with the parent at index i.
                    if (array[childIndex + 1].CompareTo(array[childIndex]) < 0)
                    {
                        childIndex++;
                    }
                }

                // Swap child's value with parent's only if parent < child.
                if (array[i].CompareTo(array[childIndex]) > 0)
                {
                    T temp = array[i];
                    array[i] = array[childIndex];
                    array[childIndex] = temp;
                }
            }
        }
    }
}
