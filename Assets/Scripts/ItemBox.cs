using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템 박스 ui가 아닌 게임 월드 상의 객체
public class ItemBox : MonoBehaviour
{
    Animator animator;
    public List<int> itemlist = null;

    ObjectPanel uipanel;
    CircleCollider2D spawnrange;

    //박스 열기
    public void BoxOpen()
    {
        //Debug.Log($"클릭이벤트 들어옴");

        //아이템 매니저가 가지고 있는 아이템 정보들 중에서 아이템 코드만 따로 리스트로 뽑아서 받아온다.
        if (itemlist.Count<=0)
            itemlist = ItemNodeManager.Instance.GetAllItemCode();

        int rnd = Random.Range(0, itemlist.Count);//해당 리스트에서 랜덤으로 아이템 코드를 뽑아서

        string temponame = ItemNodeManager.Instance.GetItemName(itemlist[rnd]);//아이템 매니저에 해당 아이템 코드를 넘겨주고 아이템 이름을 받아온다.

        Vector3 temp = Random.insideUnitCircle;//스폰 범위 지정

        float rndradius = Random.Range(0.0f, spawnrange.radius);

        ItemNodeManager.Instance.InstantiateItemObj(itemlist[rnd], temponame, this.transform.position + (temp * rndradius));//해당 아이템의 정보를 이용해서 역시 게임 월드상에 아이템 객체를 생성해준다.

    }


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spawnrange = GetComponent<CircleCollider2D>();
    }

    void Start()
    {
        uipanel = GameObject.Instantiate<ObjectPanel>(Resources.Load<ObjectPanel>("Prefabs/ObjectPanel"));

        //uipanel은 생성을해서 이렇게 링킹을 해주면 ui상의 panel이 월드상의 해당 객체를 따라다닌다.
        //그러다 해당 패널이 클릭되면 같이 등록해준 함수를 실행 시켜 준다.
        uipanel.LinkObjectPanel(this.gameObject, "ItemBox"/*표시될 이름*/, BoxOpen/*클릭되었을때 실행할 함수*/, new Vector2(0, 30)/*떨어진 위치*/);
        //UIManager.Instance.RegistUIPanel(uipanel);//uipanel을 쉽게 관리하기 위해 uimanager에 등록 시켜 준다.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
