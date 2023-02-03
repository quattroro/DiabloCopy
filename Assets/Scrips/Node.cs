using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node:IComparable
{
    public Node(bool _isWall, int _x, int _y) 
    { 
        isWall = _isWall; 
        x = _x; 
        y = _y; 
    }

    public bool isWall;
    public Node ParentNode;

    public int Weight;
   

    // G : �������κ��� �̵��ߴ� �Ÿ�, H : |����|+|����| ��ֹ� �����Ͽ� ��ǥ������ �Ÿ�, F : G + H
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