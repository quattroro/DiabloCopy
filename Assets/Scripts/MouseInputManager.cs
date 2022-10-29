using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//마우스 조작에 따른 UI 노드와 슬롯 조작
//마우스 조작에 따른 월드 조작
public class MouseInputManager : MonoBehaviour
{
    GraphicRaycaster raycaster;
    public BaseNode ClickedObj;//형재 클릭된 노드
    public IPointerOverLay LastOverlayNode;//마우스 오버레이에 사용


    private void Awake()
    {
        //raycaster = GetComponent<GraphicRaycaster>();
        raycaster = FindObjectOfType<GraphicRaycaster>();
    }


    public void RightMouseDown(Vector2 pos)
    {

    }

    
    

    //현재 마우스의 위치에 현재 클릭된 노드의 크기만큼의 비어있는 슬롯이 있는지 확인한다.
    public void CheckClickNodeSlot()
    {



    }
    


    //클릭 영역에 활성화된 UI가 있는지 확인한다.
    public void LeftMouseDown(Vector2 pos)
    {
        //canvas에 있는 graphicraycast를 이용해 클릭된 위치에 있는 객체들의 정보들을 받아온다.
        PointerEventData ped = new PointerEventData(null);
        ped.position = pos;
        List<RaycastResult> result = new List<RaycastResult>();
        raycaster.Raycast(ped, result);

        BaseNode node = null;
        BaseSlot slot = null;

        //클릭된 영역에 UI가 있을때
        if(result.Count>0)
        {
            if (ClickedObj == null)
            {
                foreach (var a in result)
                {
                    if (a.gameObject.tag == "Node")
                    {
                        node = a.gameObject.GetComponent<BaseNode>();
                        if (node.NodeIsActive)//활성노드
                        {
                            if (!node.NodeIsClicked)//클릭된 노드가 아닐때
                            {
                                Debug.Log("노드 클릭됨");
                                ClickedObj = node.NodeClick();
                            }
                        }

                        Debug.Log($"{a.gameObject.name} clicked");
                    }
                    else if (a.gameObject.tag == "ObjPanel")//클릭된 것이 오브젝트 패널이면 패널에 등록되어있는 클릭 이벤트를 실행시켜 준다.
                    {
                        a.gameObject.GetComponent<ObjectPanel>().ObjectPanelClick();
                    }

                }
            }
        }
        //클릭된 영역에 UI가 없을때 캐릭터 이동과 공격을 실시한다.
        else
        {
            Player player;
            if (Input.mousePosition.y >= 144f)
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);


                RaycastHit2D[] hit = Physics2D.CircleCastAll(point, 0.1f, Vector2.zero, 0);
                player = GameManager.Instance.GetPlayer();
                foreach (RaycastHit2D a in hit)
                {
                    if (a.transform.tag == "Wall")
                    {
                        return;
                    }

                    if (a.transform.tag == "Enemy")
                    {
                        player.AttackMove(player.transform.position, a.point, a.transform);
                        Debug.Log("Attackmove");
                        //this.AttackMove(this.transform.position, a.point, a.transform);
                        return;
                    }


                }
                player.Move(player.transform.position, hit[0].point);
                Debug.Log("nomalmove");
                //this.Move(this.transform.position, hit[0].point);

            }
        }
        
    }

    

    public void LeftMouseUp(Vector2 pos)
    {
        //canvas에 있는 graphicraycast를 이용해 클릭된 위치에 있는 객체들의 정보들을 받아온다.
        PointerEventData ped = new PointerEventData(null);
        ped.position = pos;
        List<RaycastResult> result = new List<RaycastResult>();
        raycaster.Raycast(ped, result);

        BaseNode node = null;
        BaseSlot slot = null;

        Debug.Log($"개수{result.Count}");


        BaseSlot[] resultslot = new BaseSlot[4];
        int count = 0;
        bool flag = true;

        //아무것도 없는 공간에 노드를 두면 해당 노드는 파괴
        if (result.Count == 1)
        {
            if (ClickedObj != null)
            {
                GameObject.Destroy(ClickedObj.gameObject);
                ClickedObj = null;
                return;
            }
        }


        //그래그하는 노드가 있을때
        if (ClickedObj != null)
        {
            //해당 노드의 위치에 존재하는 슬롯들을 찾는다.
            for (int i = 0; i < 4; i++)
            {
                Vector2 temppos = ClickedObj.GetCastPoint(i);
                ped.position = pos + temppos;
                result.Clear();
                raycaster.Raycast(ped, result);

                foreach (var aa in result)
                {
                    if (aa.gameObject.tag == "Slot")
                    {
                        count++;
                        resultslot[i] = aa.gameObject.GetComponent<BaseSlot>();
                        break;
                    }
                }
            }

            //모든 포인트가 슬롯안에 들어와 있고 들어간 슬롯이 같은 종류일때 
            if (count >= 4)
            {
                EnumTypes.SlotTypes type = resultslot[0].GetSlotTypes();
                for (int i = 1; i < resultslot.Length; i++)
                {
                    if (type != resultslot[i].GetSlotTypes())
                    {
                        //ClickedObj = ClickedObj.NodeClick();
                        flag = false;
                        break;
                    }

                }

                if (resultslot[0].GetSlotTypes() != EnumTypes.SlotTypes.Equip)//장비 슬롯이 아니라 다른 슬롯에 아이템을 넣을때
                {
                    if (!ItemBag.Instance.SlotIsEmpty(resultslot[0].GetSlotTypes(), resultslot[0].SlotIndex, ClickedObj.GetSize()))//아이템 가방에 해당 크기만큼 비어있는지 확인하고
                    {
                        flag = false;
                    }

                    if (flag)//이버있으면 아이템을 넣어준다.
                    {
                        Debug.Log("슬롯에 집어넣음");


                        if (ItemBag.Instance.SetItem(ClickedObj, resultslot[0]))
                            ClickedObj = null;

                    }
                }
                else//아이템을 넣으려는 슬롯이 장비 슬롯일때
                {
                    if(flag)
                    {
                        //if (EquipmentWindow.Instance.EquipEquipments(ClickedObj, resultslot[0]))//장비 슬롯에 넣어주고 넣어주기 실패했을때는 clickedobj를 그대로 둬서 이전 위치로 돌아가도록 해준다.
                        //    ClickedObj = null;
                    }
                    
                }


            }

        }

        //드래그 중인 노드가 있는데 마우스의 위치에 슬롯이 없으면 노드는 원래있었던 자리로 돌아간다.
        if (ClickedObj != null && slot == null)
        {
            Debug.Log("다시돌아감");

            if (ClickedObj.PreSlot.GetSlotTypes() != EnumTypes.SlotTypes.Equip)
                ItemBag.Instance.SetItem(ClickedObj, ClickedObj.PreSlot);
            //else
            //    EquipmentWindow.Instance.EquipEquipments(ClickedObj, ClickedObj.PreSlot);

            //ItemBag.Instance.SetItem(ClickedObj, ClickedObj.PreSlot);
            //ClickedObj.PreSlot.SetNode(ClickedObj);
            ClickedObj = null;
        }

        if (ClickedObj != null)
            ClickedObj = null;
        Debug.Log("노드파괴");

    }

    
    public void RightMouseUp(Vector2 pos)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public float lasttime = 0.0f;

    //마우스가 UI 요소들 위에 올라와 있는지 확인한다.(0.1초에 한번씩 실행)
    public void CheckMouseOverlay()
    {
        if (Time.time - lasttime >= 0.1f)
        {
            lasttime = Time.time;
            PointerEventData ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> result = new List<RaycastResult>();
            raycaster.Raycast(ped, result);
            ItemNode node;
            IPointerOverLay obj;

            foreach (var a in result)
            {

                if (a.gameObject.tag == "Node"|| a.gameObject.tag == "ObjPanel")//해당 위치에 있는 객체가 노드 또는 오브젝트 패널일때(IPointerOverLay인터페이스를 상속받는 객체일때)
                {
                    obj = a.gameObject.GetComponent<IPointerOverLay>();

                    if (a.gameObject.tag == "Node")
                    {
                        node = a.gameObject.GetComponent<ItemNode>();
                        if (node.SettedSlotList.Count <= 0)
                            return;
                    }

                    //해당 객체가 오버레이되지 않은 객체이면
                    if(obj.GetIsNowOverLay()==false)
                    {
                        obj.SetIsNowOverLay(true);//오버레이를 true로 해주고

                        if (LastOverlayNode == null)
                            LastOverlayNode = obj;//해당 객체를 LastOverlayNode 에 넣어준다.

                        if (LastOverlayNode !=obj)//그리고 만약 LastOverlayNode에 들어있는 객체와 현재 마우스가 위치한 객체가 다른 객체면 
                        {
                            LastOverlayNode.SetIsNowOverLay(false);//이전에 있었던 객체는 오버레이를 false로 바꾼다.
                            LastOverlayNode = obj;// LastOverlayNode 갱신
                        }
                    }
                    return;
                }
            }

            if (LastOverlayNode != null)//현재 마우스 위치에 아무 노드도 없는데 아이템 정보를 보여주고 있는 노드가 있으면 그만 보여주도록 한다.
            {
                if (LastOverlayNode.GetIsNowOverLay() == true)
                    LastOverlayNode.SetIsNowOverLay(false);
            }
        }


    }

    //오른쪽클릭 이동 왼쪽클릭 아이템획득, 몬스터 공격
    //마우스 움직임 관리
    //마우스가 캐릭터 위에 올라가거나 
    //몬스터 공격하도록
    public void MouseClick()
    {

        //마우스 왼클릭
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.y >= 144f)
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);


                RaycastHit2D[] hit = Physics2D.CircleCastAll(point, 0.1f, Vector2.zero, 0);
                foreach (RaycastHit2D a in hit)
                {
                    if (a.transform.tag == "Wall")
                    {
                        return;
                    }

                    if (a.transform.tag == "Enemy")
                    {
                        //Debug.Log("Attackmove");
                        //this.AttackMove(this.transform.position, a.point, a.transform);
                        return;
                    }


                }
                //Debug.Log("nomalmove");
                //this.Move(this.transform.position, hit[0].point);

            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseDown(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            LeftMouseUp(Input.mousePosition);
        }


        if(ClickedObj!=null)
        {
            //DraggingItem(Input.mousePosition);
        }

        CheckMouseOverlay();
    }
}
