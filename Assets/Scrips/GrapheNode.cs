using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GrapheNode : IComparable
{
    public int Weight;
    public AstarModule.Region region;
    public int Nodeindex;
    public GrapheNode ParentNode;

    // G : �������κ��� �̵��ߴ� �Ÿ�, H : |����|+|����| ��ֹ� �����Ͽ� ��ǥ������ �Ÿ�, F : G + H
    public int x, y, G, H;
    public int F
    {
        get
        {
            return G + H;
        }

    }

    public GrapheNode(AstarModule.Region region, int weight,int nodeIndex)
    {
        this.region = region;
        this.Weight = weight;
        this.Nodeindex = nodeIndex;
    }

    public int CompareTo(object obj)
    {
        if ((obj is Node) == false) return 0;

        return Weight.CompareTo((obj as Node).Weight);
    }

    public bool IsEqual(GrapheNode n)
    {
        return this.region == n.region;
    }

    //public static bool operator !=(GrapheNode n1, GrapheNode n2)
    //{
    //    return !(n1.region == n2.region);
    //}

    //public static bool operator ==(GrapheNode n1, GrapheNode n2)
    //{
    //    return (n1.region == n2.region);
    //}

}
