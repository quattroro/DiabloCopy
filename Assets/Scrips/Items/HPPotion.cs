using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//프리팹으로 게임오브젝트와 같이 묶여있는다.
public class HPPotion : ItemLeaf
{
    public Text text;

    public HPPotion(string name, ITEMTYPE type, int xsize, int ysize) : base(name, type, xsize, ysize)
    {
        type = ITEMTYPE.USEABLE;
        //Debug.Log("HPPotion Add");
    }

    //ItemLeaf의 Operate는 실행되지 않음
    public override void Operate()
    {
        GameManager.GetI.CS_Palyer.CurHP += 30;
        if (GameManager.GetI.CS_Palyer.CurHP >= GameManager.GetI.CS_Palyer.MaxHP)
        {
            GameManager.GetI.CS_Palyer.CurHP = GameManager.GetI.CS_Palyer.MaxHP;
        }
    }

    //onclick
    private void OnMouseDown()
    {
        Debug.Log($"{name} 눌림");
    }

    // Start is called before the first frame update
    void Start()
    {
        stock = 1;
    }


    private void OnEnable()
    {
        //text = transform.Find("Text").gameObject.GetComponent<Text>();
        text.GetComponent<Text>().text = $"{stock}";
    }

    // Update is called once per frame
    void Update()
    {
        //Text text = transform.Find("Text").gameObject.GetComponent<Text>();
        //text.text = $"{stock}";
    }
}
