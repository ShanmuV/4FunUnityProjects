using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingCopy : MonoBehaviour
{

    //NewEnemyStateManager enemy;

    [SerializeField]  GridMap grid;
    [SerializeField] Transform seeker, target;


    private void FixedUpdate()
    {
        FindPath(seeker.position, target.position);
    }

    /*private void Start()
    {
        enemy = GetComponent<NewEnemyStateManager>();
        grid = enemy.gridMap;
        seeker = enemy.Seeker;
        target = enemy.Target;
    }*/


    public void FindPathAStar(Vector3 startPos, Vector3 endPos)
    {
        Node startNode = grid.GetNodeFromWorldPosition(startPos);
        Node endNode = grid.GetNodeFromWorldPosition(endPos);

        Heap<Node> open = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closed = new HashSet<Node>();

        open.AddItem(startNode);
        while (open.Count > 0)
        {
            Node current = open.RemoveFirst();
            closed.Add(current);

            if (current == endNode)
            {
                RetracePath(startNode, endNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighboursAStar(current))
            {
                if (!neighbour.walkable || closed.Contains(neighbour)) continue;
                
                int newCost = current.gCost + CalculateDistance(current, neighbour);

                if (newCost < current.gCost || !open.Contains(neighbour))
                {
                    neighbour.gCost = newCost;
                    neighbour.hCost = CalculateDistance(neighbour, endNode);
                    neighbour.parent = current;

                    if (!open.Contains(neighbour))
                    {
                        open.AddItem(neighbour);
                    }
                    else
                    {
                        open.UpdateItem(neighbour);
                    }
                }
            }
        }
    }

    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node current = (endNode?.parent);
        while (current != startNode && current != null)
        {
            path.Add(current);
            current = current.parent;
        }

        path.Reverse();
        grid.path = path;
        return path;
    }

    private List<Vector3> ReturnPath(Node startNode, Node endNode)
    {
        List<Vector3> pathWorldPositions = new List<Vector3>();
        Node current = (((endNode?.parent)?.parent)?.parent);
        while (current != startNode && current != null)
        {
            pathWorldPositions.Add(current.GetCenterPosition());
            current = current.parent;
        }
        pathWorldPositions.Reverse();
        return pathWorldPositions;
    }

    private int  CalculateDistance(Node startNode, Node endNode)
    {
        if(startNode!=null && endNode != null)
        {
        int disX = Mathf.Abs(startNode.gridPosX - endNode.gridPosX);
        int disY = Mathf.Abs(startNode.gridPosY - endNode.gridPosY);

        if(disX> disY)
        {
            return (14*disY + 10*(disX-disY));
        }
        return (14*disX + 10*(disY-disX));
        }
        return 0;

    }

    public List<Node> FindPath(Vector3 startPos, Vector3 endPos)
    {
        Node startNode = grid.GetNodeFromWorldPosition(startPos);
        Node endNode = grid.GetNodeFromWorldPosition(endPos);


        Heap<Node> open = new Heap<Node>(grid.MaxSize);
        HashSet<Node> closed = new HashSet<Node>();

        open.AddItem(startNode);

        while (open.Count > 0)
        {
            Node current = open.RemoveFirst();
            closed.Add(current);

            if (current == endNode)
            {
                RetracePath(startNode, endNode);
                return RetracePath(startNode, endNode);
            }

            foreach (Node neighbour in grid.GetNeighbours(current))
            {
                if (!neighbour.walkable || closed.Contains(neighbour)) continue;

                int newCost = current.gCost + CalculateDistance(current, neighbour);

                if (newCost < current.gCost || !open.Contains(neighbour))
                {
                    neighbour.gCost = newCost;
                    neighbour.hCost = CalculateDistance(neighbour, endNode);
                    neighbour.parent = current;

                    if (!open.Contains(neighbour))
                    {
                        open.AddItem(neighbour);
                    }
                    else
                    {
                        open.UpdateItem(neighbour);
                    }
                }
            }
        }
        return null;
    }


}

