using System.Collections.Generic;
using System.Linq;

namespace RoutePlanner.Model
{
    public class Graph<T>
    {
        private Dictionary<string, INodeElement<T>> dictNodes = new Dictionary<string, INodeElement<T>>();
        public Dictionary<string, INodeElement<T>> NodeDictionary { get => dictNodes; }
        public void AddNode(INodeElement<T> node)
        {
            this.dictNodes.Add(node.Id, node);
        }
        public void AddConnection(INodeElement<T> from, INodeElement<T> to, int weight)
        {
            from = this.getNodeInGraph(from);
            to = this.getNodeInGraph(to);

            from.Neighbors.Add(to);
            from.Weights.Add(weight);
        }

        public int ShortestRoute(INodeElement<T> from, INodeElement<T> to)
        {

            var elements = dictNodes.Select(d => d.Value).ToList();
            int[] distances = new int[dictNodes.Count];
            bool[] visited = new bool[dictNodes.Count];

            int MinDist()
            {
                var minIndex = 0;
                var minWeight = int.MaxValue;
                for (var i = 0; i < visited.Length; i++)
                {
                    if (!visited[i] && minWeight > distances[i])
                    {
                        minWeight = distances[i];
                        minIndex = i;
                    }
                }
                return minIndex;
            }

            for (var i = 0; i < dictNodes.Count; i++)
            {
                distances[i] = int.MaxValue;
                visited[i] = false;
            }

            distances[elements.FindIndex(e => e.Id == from.Id)] = 0;


            while (visited.Count(s => !s) > 0)
            {
                var elemActual = MinDist();
                foreach (var neighbor in elements[elemActual].Neighbors)
                {
                    var nextVisit = elements.FindIndex(e => e.Id == neighbor.Id);
                    if (distances[elemActual] < int.MaxValue) // this node is out of reach
                    {
                        if (distances[nextVisit] > distances[elemActual] + elements[elemActual].GetWeight(neighbor))
                        {
                            distances[nextVisit] = distances[elemActual] + elements[elemActual].GetWeight(neighbor);
                        };
                    }

                }
                visited[elemActual] = true;
            }
            return distances[elements.FindIndex(e => e.Id == to.Id)];
        }
        public int GetConnection(INodeElement<T> from, INodeElement<T> to)
        {
            try
            {
                return this.dictNodes[from.Id].GetWeight(to);
            }
            catch (System.Exception)
            {
                return -1;
            }
        }
        private INodeElement<T> getNodeInGraph(INodeElement<T> node)
        {
            INodeElement<T> existingKey = null;
            if (!dictNodes.TryGetValue(node.Id, out existingKey))
            {
                this.dictNodes[node.Id] = node;
                return node;
            }

            return existingKey;
        }
    }
}