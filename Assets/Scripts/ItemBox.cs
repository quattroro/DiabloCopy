using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ �ڽ� ui�� �ƴ� ���� ���� ���� ��ü
public class ItemBox : MonoBehaviour
{
    Animator animator;
    public List<int> itemlist = null;

    ObjectPanel uipanel;
    CircleCollider2D spawnrange;

    //�ڽ� ����
    public void BoxOpen()
    {
        //Debug.Log($"Ŭ���̺�Ʈ ����");

        //������ �Ŵ����� ������ �ִ� ������ ������ �߿��� ������ �ڵ常 ���� ����Ʈ�� �̾Ƽ� �޾ƿ´�.
        if (itemlist.Count<=0)
            itemlist = ItemNodeManager.Instance.GetAllItemCode();

        int rnd = Random.Range(0, itemlist.Count);//�ش� ����Ʈ���� �������� ������ �ڵ带 �̾Ƽ�

        string temponame = ItemNodeManager.Instance.GetItemName(itemlist[rnd]);//������ �Ŵ����� �ش� ������ �ڵ带 �Ѱ��ְ� ������ �̸��� �޾ƿ´�.

        Vector3 temp = Random.insideUnitCircle;//���� ���� ����

        float rndradius = Random.Range(0.0f, spawnrange.radius);

        ItemNodeManager.Instance.InstantiateItemObj(itemlist[rnd], temponame, this.transform.position + (temp * rndradius));//�ش� �������� ������ �̿��ؼ� ���� ���� ����� ������ ��ü�� �������ش�.

    }


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spawnrange = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        uipanel = GameObject.Instantiate<ObjectPanel>(Resources.Load<ObjectPanel>("Prefabs/ObjectPanel"));

        //uipanel�� �������ؼ� �̷��� ��ŷ�� ���ָ� ui���� panel�� ������� �ش� ��ü�� ����ٴѴ�.
        //�׷��� �ش� �г��� Ŭ���Ǹ� ���� ������� �Լ��� ���� ���� �ش�.
        uipanel.LinkObjectPanel(this.gameObject, "ItemBox"/*ǥ�õ� �̸�*/, BoxOpen/*Ŭ���Ǿ����� ������ �Լ�*/, new Vector2(0, 30)/*������ ��ġ*/);
        //UIManager.Instance.RegistUIPanel(uipanel);//uipanel�� ���� �����ϱ� ���� uimanager�� ��� ���� �ش�.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
