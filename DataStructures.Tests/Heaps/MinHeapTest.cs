using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.Heaps;

namespace DataStructures.Tests
{
    [TestClass]
    public class MinHeapTest
    {
        [TestMethod]
        public void Heap_Has_Correct_Minimum_After_Insertion()
        {
            var heap = new MinHeap<int>();

            heap.Insert(9);
            heap.Insert(2);
            heap.Insert(4);
            heap.Insert(1);

            Assert.AreEqual(1, heap.Peek());
        }

        [TestMethod]
        public void Peek_Returns_Same_Values_Twice()
        {
            var heap = new MinHeap<int>();

            heap.Insert(9);
            heap.Insert(2);
            heap.Insert(4);

            Assert.AreEqual(2, heap.Peek());
            Assert.AreEqual(2, heap.Peek());
        }

        [TestMethod]
        public void Peek_Doesnt_Change_Length()
        {
            var heap = new MinHeap<int>();

            heap.Insert(9);
            heap.Insert(2);
            heap.Insert(4);

            heap.Peek();
            Assert.AreEqual(3, heap.Length);
        }

        [TestMethod]
        public void Minimum_Is_Correctly_Restored_After_Pop()
        {
            var heap = new MinHeap<int>();

            heap.Insert(9);
            heap.Insert(2);
            heap.Insert(4);

            Assert.AreEqual(2, heap.Pop());
            Assert.AreEqual(4, heap.Pop());
            Assert.AreEqual(9, heap.Pop());
        }

        [TestMethod]
        public void Length_Is_Correctly_Set_After_Pop()
        {
            var heap = new MinHeap<int>();

            heap.Insert(9);
            heap.Insert(2);
            heap.Insert(4);

            Assert.AreEqual(3, heap.Length);
            heap.Pop();
            Assert.AreEqual(2, heap.Length);
            heap.Pop();
            Assert.AreEqual(1, heap.Length);
            heap.Pop();
            Assert.AreEqual(0, heap.Length);
        }

        [TestMethod]
        public void Heap_From_Array_Ctor_Should_Properly_Set_Min()
        {
            var heap = new MinHeap<int>(new[] { 9, 3, 2, 2, 3, 54, 6, 87, 7, 4, 6, 6, 5 });
            Assert.AreEqual(2, heap.Peek());
        }

        [TestMethod]
        public void Heap_From_Array_Ctor_Sets_Correct_Length_Of_Heap()
        {
            var heap = new MinHeap<int>(new[] { -1, 9, 3, 2, 2, 3, 54, 6, 87, 7, 4, 6, 6, 5 });
            Assert.AreEqual(14, heap.Length);
        }
    }
}
