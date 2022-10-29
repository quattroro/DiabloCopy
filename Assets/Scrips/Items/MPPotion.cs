using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//프리팹으로 게임오브젝트와 같이 묶여있는다.
public class MPPotion : ItemLeaf
{
    public Text text;
    public MPPotion(string name, ITEMTYPE type, int xsize, int ysize) : base(name, type, xsize, ysize)
    {
        //Debug.Log("MPPotion Add");
    }

    //ItemLeaf의 Operate는 실행되지 않음
    public override void Operate()
    {
        GameManager.GetI.CS_Player.CurMP += 30;
        if(GameManager.GetI.CS_Player.CurMP >= GameManager.GetI.CS_Player.MaxMP)
        {
            GameManager.GetI.CS_Player.CurMP = GameManager.GetI.CS_Player.MaxMP;
        }
    }

    //onclick
    private void OnMouseDown()
    {
        Debug.Log($"{name} 눌림");
    }

    private void Start()
    {
        stock = 1;
    }

    private void OnEnable()
    {
        //text = transform.Find("Text").gameObject.GetComponent<Text>();
        text.GetComponent<Text>().text = $"{stock}";
    }

}
