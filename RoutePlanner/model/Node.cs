using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RoutePlanner.Model
{
    public class Node<T> : INodeElement<T>, IEquatable<INodeElement<T>>
    {
        private T data;
        private string id;
        private IList<INodeElement<T>> neighbors = new List<INodeElement<T>>();
        private IList<int> weights = new List<int>();
        public Node(T data, string id)
        {
            this.data = data;
            this.id = id;
        }

        public string Id { get => id; }
        public T Value { get => data; set => data = value; }
        public IList<INodeElement<T>> Neighbors { get => this.neighbors; }
        public IList<int> Weights { get => this.weights; }

        public bool Equals([AllowNull] INodeElement<T> other)
        {
            return this.id == other.Id;
        }
        public int GetWeight(INodeElement<T> to)
        {
            var pos = getNeighbor(to);
            if (pos >= 0)
                return this.Weights[pos];

            throw new System.Exception("Route not found");
        }
        private int getNeighbor(INodeElement<T> to)
        {
            for (var i = 0; i < this.neighbors.Count; i++)
            {
                if (to.Id == this.neighbors[i].Id)
                    return i;
            }
            return -1;
        }
    }
}