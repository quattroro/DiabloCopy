using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightArmor : ItemLeaf
{
    public LightArmor(string name, ITEMTYPE type, int xsize, int ysize) : base(name, type, xsize, ysize)
    {
        //Debug.Log("MPPotion Add");
    }

    //ItemLeaf의 Operate는 실행되지 않음
    public override void Operate()
    {

    }

    //onclick
    private void OnMouseDown()
    {
        Debug.Log($"{name} 눌림");
    }
}
