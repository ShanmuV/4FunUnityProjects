using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using CodeMonkey.Utils;

public class GridMap : MonoBehaviour
{
    public LayerMask unwalkable;
    public Vector3 gridWorldSize;
    public Vector3 originPosition;
    public float nodeSize;
    Node[,] grid;


    [SerializeField] private bool showDebugGrid;


    List<Node> platforms = new List<Node>();
    public List<Node> path;

    [SerializeField] Transform playerPos, targetPos;

    private int gridSizeX, gridSizeY;

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    private void Awake()
    {
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeSize);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeSize);
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldPos;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                worldPos = GetWorldPosition(x, y) + new Vector3(nodeSize / 2, nodeSize / 2);
                bool walkable = !(Physics2D.OverlapCircle((Vector2)worldPos, 0.1f, unwalkable));
                grid[x, y] = new Node(nodeSize, false, (Vector2)(worldPos), x, y);
                if (!walkable)
                {
                    platforms.Add(grid[x, y]);
                }
            }
        }


        SetWalkable();

    }

    private void SetWalkable()
    {
        List<Node> adjacentNodes = new List<Node>();

        foreach (Node node in platforms)
        {
            Node upperNode = GetUpperNode(node);
            Node rightNode = GetRightNode(node);
            Node leftNode = GetLeftNode(node);

            if (!platforms.Contains(upperNode))
            {
                upperNode.SetWalkable(true);
            }
            if (!platforms.Contains(rightNode) && rightNode!=null)
            {
                Node adj1 = GetUpperNode(rightNode);
                adj1?.SetWalkable(true);
                Node adj2 = GetRightNode(adj1);
                adj2?.SetWalkable(true);                      
                adjacentNodes.Add(adj1);
                adjacentNodes.Add(adj2);
            }
            if (!platforms.Contains(leftNode) && leftNode!=null)
            {
                Node adj1 = GetUpperNode(leftNode);
                adj1?.SetWalkable(true);
                Node adj2 = GetLeftNode(adj1);
                adj2?.SetWalkable(true);
                adjacentNodes.Add(adj1);
                adjacentNodes.Add(adj2);
            }

        }

        foreach(Node node in adjacentNodes)
        {
            if (node == null) continue;

            Node currentNode = GetBelowNode(node);
            while (currentNode != null)
            {
                if (currentNode.walkable)
                {
                    break;
                }
                currentNode.walkable = true;
                currentNode = GetBelowNode(currentNode);
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * nodeSize + originPosition;
    }

    public Node GetNodeFromWorldPosition(Vector3 worldPos)
    {
        int x, y;
        x = (Mathf.FloorToInt((worldPos.x - originPosition.x) / nodeSize));
        y = (Mathf.FloorToInt((worldPos.y - originPosition.y) / nodeSize));
        return GetNodeFromWorldPosition(x, y);
    }

    public Node GetNodeFromWorldPosition(int x, int y)
    {
        if (x < 0 || x >= gridSizeX || y < 0 || y >= gridSizeY) return null;
        return grid[x, y];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = UtilsClass.GetMouseWorldPosition();
            Node node = GetNodeFromWorldPosition((Vector3)mousePos);
            Debug.Log(node.GetCenterPosition().x + ", " + node.GetCenterPosition().y);
        }
    }

    public List<Node> GetNeighboursAStar(Node node) // returns all the neighbours including diagonal nodes
    {
        List<Node> neighbours = new List<Node>();
        
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;
                int checkX = node.gridPosX + x;
                int checkY = node.gridPosY + y;
                if (checkX >= 0 && checkY >= 0 && checkX < gridSizeX && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    public List<Node> GetNeighbours(Node node) // returns only the top, bottom, left and right nodes
    {
        List<Node> neighbours = new List<Node>();

        int[] dirX = { -1, 0, 1, 0 };
        int[] dirY = { 0, 1, 0, -1 };

        for(int i = 0; i < 4; i++)
        {
            int checkX = node.gridPosX + dirX[i];
            int checkY = node.gridPosY + dirY[i];
            if (checkX >= 0 && checkY >= 0 && checkX < gridSizeX && checkY < gridSizeY)
            {
                neighbours.Add(grid[checkX, checkY]);
            }
        }
        return neighbours;
    }

    private void OnDrawGizmos()
    {
        if (showDebugGrid)
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y));
            if (grid != null)
            {
                Node PlayerNode = GetNodeFromWorldPosition(playerPos.position);
                Node TargetNode = GetNodeFromWorldPosition(targetPos.position);
                foreach (Node node in grid)
                {
                    Gizmos.color = (node.walkable) ? Color.black : Color.red;
                    if (path != null)
                    {
                        if (path.Contains(node))
                        {
                            Gizmos.color = Color.cyan;
                        }
                    }
                    if (node == PlayerNode)
                    {
                        Gizmos.color = Color.white;
                    }
                    if (node == TargetNode)
                    {
                        Gizmos.color = Color.yellow;
                    }
                    Gizmos.DrawWireCube(node.worldPos, new Vector3(nodeSize, nodeSize));

                }
            }

        }
    }

    public Node GetLeftNode(Node node)
    {
        if (node.gridPosX - 1 < 0)
        {
            return null;
        }
        return grid[node.gridPosX - 1, node.gridPosY];
    }

    public Node GetRightNode(Node node)
    {
        if (node.gridPosX + 1 >= gridSizeX)
        {
            return null;
        }
        return grid[node.gridPosX + 1, node.gridPosY];
    }

    public Node GetUpperNode(Node node)
    {
        if (node.gridPosY + 1 >= gridSizeY)
        {
            return null;
        }
        return grid[node.gridPosX, node.gridPosY + 1];
    }

    public Node GetBelowNode(Node node)
    {
        if (node.gridPosY - 1 < 0)
        {
            return null;
        }
        return grid[node.gridPosX, node.gridPosY - 1];
    }

 
}
