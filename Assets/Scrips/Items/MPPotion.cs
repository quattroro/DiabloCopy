using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���������� ���ӿ�����Ʈ�� ���� �����ִ´�.
public class MPPotion : ItemLeaf
{
    public Text text;
    public MPPotion(string name, ITEMTYPE type, int xsize, int ysize) : base(name, type, xsize, ysize)
    {
        //Debug.Log("MPPotion Add");
    }

    //ItemLeaf�� Operate�� ������� ����
    public override void Operate()
    {
        GameManager.GetI.CS_Palyer.CurMP += 30;
        if(GameManager.GetI.CS_Palyer.CurMP >= GameManager.GetI.CS_Palyer.MaxMP)
        {
            GameManager.GetI.CS_Palyer.CurMP = GameManager.GetI.CS_Palyer.MaxMP;
        }
    }

    //onclick
    private void OnMouseDown()
    {
        Debug.Log($"{name} ����");
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
