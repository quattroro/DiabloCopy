using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node:IComparable
{
    public AstarModule.LocalRegion localregion;
    public Node(bool _isWall, int _x, int _y, AstarModule.LocalRegion localregion = null) 
    { 
        isWall = _isWall; 
        x = _x; 
        y = _y;
        this.localregion = localregion;
    }

    public bool isWall;
    public Node ParentNode;

    public int Weight;
   

    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H;
    public int F 
    { 
        get 
        { 
            return G + H; 
        } 
        
    }

    public int CompareTo(object obj)
    {
        if ((obj is Node) == false) return 0;

        return Weight.CompareTo((obj as Node).Weight);
    }
}