using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLeaf : Item
{

    public ItemLeaf(string name, ITEMTYPE type, int xsize, int ysize) : base(name, type, xsize, ysize)
    {

    }

    public ItemLeaf(string name, ITEMTYPE type, int xsize, int ysize, EQUIPPARTS parts) : base(name, type, xsize, ysize, parts)
    {
    }

    public override void Operate()
    {

    }
}
