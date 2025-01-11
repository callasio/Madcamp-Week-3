using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace GamePlay.StageData.Player.PathFinder
{
    public class BFS
    {
        private static readonly IEnumerable<Direction> _movingDirections = new List<Direction>
        {
            Direction.Up,
            Direction.Down,
            Direction.Left,
            Direction.Right,
        };
        
        [CanBeNull]
        public static List<Coordinates> GetPath(StageElementData[] elements, Coordinates from, Coordinates to)
        {
            var excludePathElements = elements.Where(element => element.Type == StageElementType.Speaker);
            var pathElements = elements.Where(element => element.Type == StageElementType.Tile);
            var excludeCoordinates = excludePathElements.Select(element => element.Coordinates);
            var pathCoordinates = pathElements.Select(element => element.Coordinates).Except(excludeCoordinates).ToHashSet();
            var queue = new Queue<Coordinates>();
            var visited = new HashSet<Coordinates>();
            var parentMap = new Dictionary<Coordinates, Coordinates>();
            
            queue.Enqueue(from);
            visited.Add(from);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                if (current == to)
                {
                    return ReconstructPath(parentMap, from, to);
                }

                foreach (var direction in _movingDirections)
                {
                    var neighbor = current + direction;

                    if (pathCoordinates.Contains(neighbor) && !visited.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                        parentMap[neighbor] = current;
                    }
                }
            }

            return null;
        }
        
        private static List<Coordinates> ReconstructPath(Dictionary<Coordinates, Coordinates> parentMap, Coordinates start, Coordinates end)
        {
            var path = new List<Coordinates>();
            var current = end;

            while (!current.Equals(start))
            {
                path.Add(current);
                current = parentMap[current];
            }
            
            path.Reverse();
            return path;
        }
    }
}