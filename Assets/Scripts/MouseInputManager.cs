using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//���콺 ���ۿ� ���� UI ���� ���� ����
//���콺 ���ۿ� ���� ���� ����
public class MouseInputManager : MonoBehaviour
{
    GraphicRaycaster raycaster;
    public BaseNode ClickedObj;//���� Ŭ���� ���
    public IPointerOverLay LastOverlayNode;//���콺 �������̿� ���


    private void Awake()
    {
        //raycaster = GetComponent<GraphicRaycaster>();
        raycaster = FindObjectOfType<GraphicRaycaster>();
    }


    public void RightMouseDown(Vector2 pos)
    {

    }

    
    

    //���� ���콺�� ��ġ�� ���� Ŭ���� ����� ũ�⸸ŭ�� ����ִ� ������ �ִ��� Ȯ���Ѵ�.
    public void CheckClickNodeSlot()
    {



    }
    


    //Ŭ�� ������ Ȱ��ȭ�� UI�� �ִ��� Ȯ���Ѵ�.
    public void LeftMouseDown(Vector2 pos)
    {
        //canvas�� �ִ� graphicraycast�� �̿��� Ŭ���� ��ġ�� �ִ� ��ü���� �������� �޾ƿ´�.
        PointerEventData ped = new PointerEventData(null);
        ped.position = pos;
        List<RaycastResult> result = new List<RaycastResult>();
        raycaster.Raycast(ped, result);

        BaseNode node = null;
        BaseSlot slot = null;

        //Ŭ���� ������ UI�� ������
        if(result.Count>0)
        {
            if (ClickedObj == null)
            {
                foreach (var a in result)
                {
                    if (a.gameObject.tag == "Node")
                    {
                        node = a.gameObject.GetComponent<BaseNode>();
                        if (node.NodeIsActive)//Ȱ�����
                        {
                            if (!node.NodeIsClicked)//Ŭ���� ��尡 �ƴҶ�
                            {
                                Debug.Log("��� Ŭ����");
                                ClickedObj = node.NodeClick();
                            }
                        }

                        Debug.Log($"{a.gameObject.name} clicked");
                    }
                    else if (a.gameObject.tag == "ObjPanel")//Ŭ���� ���� ������Ʈ �г��̸� �гο� ��ϵǾ��ִ� Ŭ�� �̺�Ʈ�� ������� �ش�.
                    {
                        a.gameObject.GetComponent<ObjectPanel>().ObjectPanelClick();
                    }

                }
            }
        }
        //Ŭ���� ������ UI�� ������ ĳ���� �̵��� ������ �ǽ��Ѵ�.
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
        //canvas�� �ִ� graphicraycast�� �̿��� Ŭ���� ��ġ�� �ִ� ��ü���� �������� �޾ƿ´�.
        PointerEventData ped = new PointerEventData(null);
        ped.position = pos;
        List<RaycastResult> result = new List<RaycastResult>();
        raycaster.Raycast(ped, result);

        BaseNode node = null;
        BaseSlot slot = null;

        Debug.Log($"����{result.Count}");


        BaseSlot[] resultslot = new BaseSlot[4];
        int count = 0;
        bool flag = true;

        //�ƹ��͵� ���� ������ ��带 �θ� �ش� ���� �ı�
        if (result.Count == 1)
        {
            if (ClickedObj != null)
            {
                GameObject.Destroy(ClickedObj.gameObject);
                ClickedObj = null;
                return;
            }
        }


        //�׷����ϴ� ��尡 ������
        if (ClickedObj != null)
        {
            //�ش� ����� ��ġ�� �����ϴ� ���Ե��� ã�´�.
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

            //��� ����Ʈ�� ���Ծȿ� ���� �ְ� �� ������ ���� �����϶� 
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

                if (resultslot[0].GetSlotTypes() != EnumTypes.SlotTypes.Equip)//��� ������ �ƴ϶� �ٸ� ���Կ� �������� ������
                {
                    if (!ItemBag.Instance.SlotIsEmpty(resultslot[0].GetSlotTypes(), resultslot[0].SlotIndex, ClickedObj.GetSize()))//������ ���濡 �ش� ũ�⸸ŭ ����ִ��� Ȯ���ϰ�
                    {
                        flag = false;
                    }

                    if (flag)//�̹������� �������� �־��ش�.
                    {
                        Debug.Log("���Կ� �������");


                        if (ItemBag.Instance.SetItem(ClickedObj, resultslot[0]))
                            ClickedObj = null;

                    }
                }
                else//�������� �������� ������ ��� �����϶�
                {
                    if(flag)
                    {
                        //if (EquipmentWindow.Instance.EquipEquipments(ClickedObj, resultslot[0]))//��� ���Կ� �־��ְ� �־��ֱ� ������������ clickedobj�� �״�� �ּ� ���� ��ġ�� ���ư����� ���ش�.
                        //    ClickedObj = null;
                    }
                    
                }


            }

        }

        //�巡�� ���� ��尡 �ִµ� ���콺�� ��ġ�� ������ ������ ���� �����־��� �ڸ��� ���ư���.
        if (ClickedObj != null && slot == null)
        {
            Debug.Log("�ٽõ��ư�");

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
        Debug.Log("����ı�");

    }

    
    public void RightMouseUp(Vector2 pos)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public float lasttime = 0.0f;

    //���콺�� UI ��ҵ� ���� �ö�� �ִ��� Ȯ���Ѵ�.(0.1�ʿ� �ѹ��� ����)
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

                if (a.gameObject.tag == "Node"|| a.gameObject.tag == "ObjPanel")//�ش� ��ġ�� �ִ� ��ü�� ��� �Ǵ� ������Ʈ �г��϶�(IPointerOverLay�������̽��� ��ӹ޴� ��ü�϶�)
                {
                    obj = a.gameObject.GetComponent<IPointerOverLay>();

                    if (a.gameObject.tag == "Node")
                    {
                        node = a.gameObject.GetComponent<ItemNode>();
                        if (node.SettedSlotList.Count <= 0)
                            return;
                    }

                    //�ش� ��ü�� �������̵��� ���� ��ü�̸�
                    if(obj.GetIsNowOverLay()==false)
                    {
                        obj.SetIsNowOverLay(true);//�������̸� true�� ���ְ�

                        if (LastOverlayNode == null)
                            LastOverlayNode = obj;//�ش� ��ü�� LastOverlayNode �� �־��ش�.

                        if (LastOverlayNode !=obj)//�׸��� ���� LastOverlayNode�� ����ִ� ��ü�� ���� ���콺�� ��ġ�� ��ü�� �ٸ� ��ü�� 
                        {
                            LastOverlayNode.SetIsNowOverLay(false);//������ �־��� ��ü�� �������̸� false�� �ٲ۴�.
                            LastOverlayNode = obj;// LastOverlayNode ����
                        }
                    }
                    return;
                }
            }

            if (LastOverlayNode != null)//���� ���콺 ��ġ�� �ƹ� ��嵵 ���µ� ������ ������ �����ְ� �ִ� ��尡 ������ �׸� �����ֵ��� �Ѵ�.
            {
                if (LastOverlayNode.GetIsNowOverLay() == true)
                    LastOverlayNode.SetIsNowOverLay(false);
            }
        }


    }

    //������Ŭ�� �̵� ����Ŭ�� ������ȹ��, ���� ����
    //���콺 ������ ����
    //���콺�� ĳ���� ���� �ö󰡰ų� 
    //���� �����ϵ���
    public void MouseClick()
    {

        //���콺 ��Ŭ��
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
