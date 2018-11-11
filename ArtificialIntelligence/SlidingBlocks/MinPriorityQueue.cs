using System;
using System.Collections.Generic;
using System.Text;

namespace SlidingBlocks
{

    public class MinPriorityQueue<T> where T : IComparable<T>
    {
        private readonly IList<T> heap;

        public MinPriorityQueue()
        {
            this.heap = new List<T>();
        }

        public int Count => this.heap.Count;

        public void Enqueue(T item)
        {
            this.heap.Add(item);
            this.HeapifyUp(this.heap.Count - 1);
        }

        public T Peek()
        {
            return this.heap[0];
        }

        public T Dequeue()
        {
            if (this.Count <= 0)
            {
                throw new InvalidOperationException();
            }

            T item = this.heap[0];

            this.Swap(0, this.heap.Count - 1);
            this.heap.RemoveAt(this.heap.Count - 1);
            this.HeapifyDown(0);

            return item;
        }

        private void HeapifyUp(int index)
        {
            while (index > 0 && IsLess(index, this.Parent(index)))
            {
                this.Swap(index, this.Parent(index));
                index = this.Parent(index);
            }
        }

        private void HeapifyDown(int index)
        {
            while (index < this.heap.Count / 2)
            {
                int child = this.Left(index);
                if (this.HasChild(child + 1) && this.IsLess(child + 1, child))
                {
                    child = child + 1;
                }

                if (this.IsLess(index, child))
                {
                    break;
                }

                this.Swap(index, child);
                index = child;
            }
        }

        private bool HasChild(int child)
        {
            return child < this.heap.Count;
        }

        private int Parent(int index)
        {
            return (index - 1) / 2;
        }

        private int Left(int index)
        {
            return 2 * index + 1;
        }

        private int Right(int index)
        {
            return this.Left(index) + 1;
        }

        private bool IsLess(int a, int b)
        {
            return this.heap[a].CompareTo(this.heap[b]) < 0;
        }

        private void Swap(int a, int b)
        {
            T temp = this.heap[a];
            this.heap[a] = this.heap[b];
            this.heap[b] = temp;
        }
    }

}
