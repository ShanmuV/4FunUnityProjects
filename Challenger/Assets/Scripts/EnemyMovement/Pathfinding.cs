using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private GridMap gridMap;

    [SerializeField] private Transform enemyPosition;
    [SerializeField] private Transform playerPosition;


    private void Update()
    {
        FindPath(enemyPosition.position, playerPosition.position);
    }


    public List<Vector3> FindPath(Vector3 startPos, Vector3 endPos) 
    {
        Node startNode = gridMap.GetNodeFromWorldPosition(startPos);
        Node endNode = gridMap.GetNodeFromWorldPosition(endPos);

        Heap<Node> open = new Heap<Node>(gridMap.MaxSize);
        HashSet<Node> closed = new HashSet<Node>();

        open.AddItem(startNode);
        while(open.Count > 0)
        {
            Node current = open.RemoveFirst();
            closed.Add(current);

            if(current == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            foreach(Node node in gridMap.GetNeighbours(current))
            {
                if(!node.walkable || closed.Contains(node))
                {
                    continue;
                }

                int newCost = CalculateDistance(current, node);

                if (newCost<current.gCost || !open.Contains(node))
                {
                    node.gCost = newCost;
                    node.hCost = CalculateDistance(node, endNode);
                    node.parent = current;

                    if(!open.Contains(node))
                    {
                        open.AddItem(node);
                    }
                    else
                    {
                        open.UpdateItem(node);
                    }
                }
            }
        }
        return null;
    }

    private int CalculateDistance(Node nodeA, Node nodeB)
    {
        if (nodeA != null && nodeB != null)
        {
            int disX = Mathf.Abs(nodeA.gridPosX - nodeB.gridPosX);
            int disY = Mathf.Abs(nodeA.gridPosY - nodeB.gridPosY);

            if (disX > disY)
            {
                return (14 * disY + 10 * (disX - disY));
            }
            return (14 * disX + 10 * (disY - disX));
        }
        return 0;
    }

    private List<Vector3> RetracePath(Node startNode, Node endNode)
    {
        List<Vector3> path = new List<Vector3>();
        List<Node> pathNodes = new List<Node>();
        Node current = ((endNode?.parent)?.parent);
        while(current != null && current != startNode)
        {
            path.Add(current.GetCenterPosition());
            pathNodes.Add(current);
            current = current.parent;
        }
        path.Reverse();
        pathNodes.Reverse();
        gridMap.path = pathNodes;
        return path;
    }
}
