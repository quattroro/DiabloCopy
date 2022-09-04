using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임 월드 상에 존재할 아이템 객체
public class ItemObj : MonoBehaviour
{
    ObjectPanel uipanel;
    public int itemcode;
    public string itemname;

    //아이템 코드와 이름만 가지고 초기화를 해준다.
    public void InitItem(int itemcode, string itemname,Vector3 pos)
    {
        this.itemcode = itemcode;
        this.itemname = itemname;

        //이미지를 받아와서 설정
        GetComponentInChildren<SpriteRenderer>().sprite = ItemNodeManager.Instance.GetItemSprite(itemcode);

        //uipanel은 생성을해서 이렇게 링킹을 해주면 ui상의 panel이 월드상의 해당 객체를 따라다닌다.
        //그러다 해당 패널이 클릭되면 같이 등록해준 함수를 실행 시켜 준다.
        uipanel = GameObject.Instantiate<ObjectPanel>(Resources.Load<ObjectPanel>("Prefabs/ObjectPanel"));
        uipanel.LinkObjectPanel(this.gameObject, itemname/*표시될 이름*/, GetItem/*클릭시 호출될 함수*/, new Vector2(0, 30)/*떨어질 위치*/);
        UIManager.Instance.RegistUIPanel(uipanel);//uipanel을 쉽게 관리하기 위해 uimanager에 등록 시켜 준다.
        this.transform.position = pos;


    }

    //클릭되면
    public void GetItem()
    {
        BaseNode copynode = ItemNodeManager.Instance.InstantiateNode(itemcode, this.transform);//해당 코드에 해당하는 아이템을 아이템 가방에 넣어준다.
        UIManager.Instance.DeleteUIPanel(uipanel);
        ItemBag.Instance.InsertItem(copynode);
        Destroy(this.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
