using System.Collections.Generic;

namespace RoutePlanner.Business.Graph
{
    public interface IGraph<T>
    {
        Dictionary<string, INodeElement<T>> NodeDictionary { get; }
        void AddConnection(INodeElement<T> from, INodeElement<T> to, int weight);
        void AddNode(INodeElement<T> node);
        int GetConnection(INodeElement<T> from, INodeElement<T> to);
        int ShortestRoute(INodeElement<T> from, INodeElement<T> to);
    }
}