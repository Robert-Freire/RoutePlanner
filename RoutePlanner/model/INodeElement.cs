using System.Collections.Generic;

namespace RoutePlanner.Model
{
    public interface INodeElement<T>
    {
        public string Id { get; }
        public T Value { get; set; }
        public IList<INodeElement<T>> Neighbors { get; }
        public IList<int> Weights { get; }
        public int GetWeight(INodeElement<T> to);
    }
}