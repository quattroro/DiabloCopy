using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : ItemLeaf
{
    public Helmet(string name, ITEMTYPE type, int xsize, int ysize) : base(name, type, xsize, ysize)
    {
        //Debug.Log("MPPotion Add");
    }

    //ItemLeaf�� Operate�� ������� ����
    public override void Operate()
    {

    }

    //onclick
    private void OnMouseDown()
    {
        Debug.Log($"{name} ����");
    }
}
