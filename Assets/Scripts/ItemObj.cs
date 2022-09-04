using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ���� �� ������ ������ ��ü
public class ItemObj : MonoBehaviour
{
    ObjectPanel uipanel;
    public int itemcode;
    public string itemname;

    //������ �ڵ�� �̸��� ������ �ʱ�ȭ�� ���ش�.
    public void InitItem(int itemcode, string itemname,Vector3 pos)
    {
        this.itemcode = itemcode;
        this.itemname = itemname;

        //�̹����� �޾ƿͼ� ����
        GetComponentInChildren<SpriteRenderer>().sprite = ItemNodeManager.Instance.GetItemSprite(itemcode);

        //uipanel�� �������ؼ� �̷��� ��ŷ�� ���ָ� ui���� panel�� ������� �ش� ��ü�� ����ٴѴ�.
        //�׷��� �ش� �г��� Ŭ���Ǹ� ���� ������� �Լ��� ���� ���� �ش�.
        uipanel = GameObject.Instantiate<ObjectPanel>(Resources.Load<ObjectPanel>("Prefabs/ObjectPanel"));
        uipanel.LinkObjectPanel(this.gameObject, itemname/*ǥ�õ� �̸�*/, GetItem/*Ŭ���� ȣ��� �Լ�*/, new Vector2(0, 30)/*������ ��ġ*/);
        UIManager.Instance.RegistUIPanel(uipanel);//uipanel�� ���� �����ϱ� ���� uimanager�� ��� ���� �ش�.
        this.transform.position = pos;


    }

    //Ŭ���Ǹ�
    public void GetItem()
    {
        BaseNode copynode = ItemNodeManager.Instance.InstantiateNode(itemcode, this.transform);//�ش� �ڵ忡 �ش��ϴ� �������� ������ ���濡 �־��ش�.
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
