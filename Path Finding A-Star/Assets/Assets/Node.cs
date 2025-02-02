using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{

    public bool walkable;
    public Vector3 worldPos;

    public int gridPosX;
    public int gridPosY;
    public float nodeSize;

    private int heapIndex;
    public int gCost;
    public int hCost;

    public Node parent;

    public int fCost
    {
        get { return gCost + hCost; }
    }

    public Node(float nodeSize,bool walkable, Vector2 worldPos, int gridPosX, int gridPosY)
    {
        this.walkable = walkable;
        this.worldPos = worldPos;
        this.gridPosX = gridPosX;
        this.gridPosY = gridPosY;
        this.nodeSize = nodeSize;
    }

    

    public Vector3 GetCenterPosition() // used for the square sprite
    {
        return worldPos + new Vector3((float)nodeSize/2f  , (float)nodeSize/2f );
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node A)
    {
        int compare =  fCost.CompareTo(A.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(A.hCost);
        }
        return -compare;
    }

    public void SetWalkable(bool walkable)
    {
        this.walkable = walkable;
    }
}
