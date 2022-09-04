using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSlot : MonoBehaviour
{
    public Vector2Int SlotIndex;//���� �ε���

    public BaseNode SettingNode;//���Կ� ���õǾ� �ִ� ���

    public RectTransform rectTransform;//������ recttransform ���Կ� ��带 �����Ҷ� ����� ũ�⸦ ���Կ� �����ֱ� ���� ������ �ִ´�.

    public delegate void InsertSlotEvent();//�������� �̺�Ʈ, �ش� ���Կ� ��尡 �����Ǹ� �ش� �븮�ڰ� ����ȴ�.
    protected InsertSlotEvent insertevent;

    public delegate void PickUpSlotEvent();//�����Ⱦ� �̺�Ʈ, ���Կ��� �������� �������� �ش� �븮�ڰ� ����ȴ�.
    protected PickUpSlotEvent pickupevent;

    [SerializeField]
    protected EnumTypes.SlotTypes slottype;//���� Ÿ�� 

    public EnumTypes.SlotTypes GetSlotTypes()//���� Ÿ�� ����
    {
        return slottype;
    }

    //��� ���� Ÿ�� ����
    public virtual EnumTypes.EquipmentTypes GetEquipTypes()
    {
        return (EnumTypes.EquipmentTypes)(-1);
    }

    //�����ϳ��� ����ϳ��� ����
    public virtual void SetNode(BaseNode node)
    {
        node.SettedSlotList.Clear();
        //��带 ���� �ڽ��� ������ �ΰ� ũ�⸦ �����ش�.
        node.transform.parent = this.transform;
        node.rectTransform.sizeDelta = this.rectTransform.sizeDelta;
        node.transform.localPosition = new Vector3(0, 0, 0);
        if (node.NodeIsClicked)
            node.NodeIsClicked = false;

        node.AddSettedSlotList(this);
        //node.SettedSlot = this;
        this.SettingNode = node;
    }

    //���� �������� ���� ��带 �����Ҷ� ���
    public virtual void SetNodeData(BaseNode node)
    {
        //node.SettedSlotList.Clear();
        if (node.NodeIsClicked)
            node.NodeIsClicked = false;
        //node.SettedSlot = this;
        this.SettingNode = node;
        node.AddSettedSlotList(this);

    }

    ////�ش� ���Կ� ������ ���� ����� ũ�⸦ �־��ָ� �ش� ũ�⸸ŭ �������� �������ش�.
    //public virtual void SetNode(BaseNode node, Vector2Int size)
    //{

    //}

    //���Կ� ��� �������� ���ԵǾ����� ����� �̺�Ʈ
    public virtual void InsertEvent(InsertSlotEvent _event)
    {
        insertevent += _event;
    }
    //���Կ� �ִ� �������� �������� �� ����� �̺�Ʈ
    public virtual void PickUpEvent(PickUpSlotEvent _event)
    {
        pickupevent += _event;
    }

    //���� ���Կ� ���ִ� �������� ����
    public void RemoveNode()
    {
        if(SettingNode!=null)
        {
            GameObject.Destroy(SettingNode.gameObject);
            SettingNode = null;
        }
    }

    //���� ���Կ� �����ִ� �������� ����
    public void RemoveNode(BaseNode node)
    {
        if(SettingNode == node)
        {
            GameObject.Destroy(SettingNode.gameObject);
            SettingNode = null;
        }
    }

    //���õǾ��ִ� ��������� �����ش�.
    public void ClearSettingNode()
    {
        SettingNode = null;
    }

    //���� ���Կ� �����ִ� �������� �������ش�.
    public virtual BaseNode GetSettingNode()
    {
        SettingNode.NodeIsClicked = true;
        SettingNode.PreSlot = this;
        BaseNode temp = SettingNode;
        SettingNode = null;
        return temp;
    }

    //���� ���Կ� �����ִ� �������� �ڵ带 �Ѱ��ش�.
    public int GetSettingNodeID()
    {
        if (SettingNode == null)
            return -1;

        return SettingNode.GetItemID();
    }


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
