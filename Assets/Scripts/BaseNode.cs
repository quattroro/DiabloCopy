using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNode : MonoBehaviour
{
    Vector2Int Index;
    [SerializeField]
    private bool _isactive;//Ȱ����� / ��Ȱ�� ���
    [SerializeField]
    private bool _isclicked;//Ŭ���Ȼ��� / Ŭ������ ���� ����

    //[SerializeField]
    //private bool _isSnapped;//������ ���� �پ��ִ� ���� / ������ ���� �پ����� ���� ����

    public RectTransform rectTransform;//�ڱ� �ڽ��� recttransform

    public BaseSlot SettedSlot;//��尡 ���õǾ� �ִ� ����

    public List<BaseSlot> SettedSlotList;//��尡 ���õǾ� �ִ� ���Ե�


    public BaseSlot PreSlot;//��尡 Ŭ���Ǿ ���콺�� ����ٴҶ� ���� �ִ� ������ ��ġ�� ������ ���´�.


    public Transform[] castpoint = new Transform[4];//��尡 ��ġ�Ѱ��� Ž���Ҷ� 4���� �𼭸����ִ� ����Ʈ�� laycast�� �̿��� Ž���ؼ� �ڽ��� ��ġ�� �ִ� ������ ã�Ƴ���.
    public Vector2 castpointpos;

    public Vector2 originsize;


    public virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

    }

    //����� ������ Ÿ���� �����Ѵ�.
    public virtual EnumTypes.ItemTypes GetItemTypes()
    {
        return (EnumTypes.ItemTypes)(-1);
    }
    //����� ��� Ÿ���� �����Ѵ�.
    public virtual EnumTypes.EquipmentTypes GetEquipTypes()
    {
        return (EnumTypes.EquipmentTypes)(-1);
    }
    
    //�װ��� �𼭸��� �����Ѵ�.
    public Vector2 GetCastPoint(int index)
    {
        return castpoint[index].position - this.transform.position;
    }

    //��尡 Ŭ�� �Ǿ�����.
    //��尡 �����Ǿ� �ִ� ���Ե鿡�Լ� �ڽ��� ������ �����ְ� �ڱ� �ڽ��� �����Ѵ�.
    public BaseNode NodeClick()
    {
        //BaseNode temp = this;
        NodeIsClicked = true;
        //this.rectTransform.pivot = new Vector2(0.5f, 0.5f);
        PreSlot = SettedSlotList[0];
        foreach(var a in SettedSlotList)
        {
            a.ClearSettingNode();
        }
        SettedSlotList.Clear();
        return this;
    }    

    //��� �ϳ��� �������� ������ �����ϱ� ������ �ڽ��� ��ġ�� ���Ե��� ����Ʈ�� ������ �ִ´�.
    public void AddSettedSlotList(BaseSlot slot)
    {
        SettedSlotList.Add(slot);
    }

    //�ش� ��尡 ��ȣ�ۿ� �� �� �ִ� Ȱ�� ��������� Ȯ��
    public bool NodeIsActive
    {
        get
        {
            return _isactive;
        }
        set
        {
            _isactive = value;
        }
    }

    //Ŭ���� �������� �ƴ��� 
    public bool NodeIsClicked
    {
        get
        {
            return _isclicked;
        }
        set
        {
            _isclicked = value;
            if(_isclicked)
            {
                this.rectTransform.sizeDelta = originsize;
                this.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            }
            else
            {
                this.rectTransform.pivot = new Vector2(0.0f, 1.0f);
            }

        }
    }

    //�ʱ�ȭ �Լ�
    public virtual void InitSetting(Dictionary<int, string> itemdata, Sprite sprite)
    {

    }
    //�ʱ�ȭ �Լ�
    public virtual void InitSetting(int itemcode, string Name, Sprite sprite, Vector3 pos, string _type)
    {

    }

    //�ش� ��带 ���ݾ� �ΰ��� ���� �����ش�.
    public virtual BaseNode DivideNode()
    {
        BaseNode temp = null;
        if(IsStackAble()&&GetStack()>=2)
        {
            int cur = GetStack() / 2;
            
            temp = GameObject.Instantiate<BaseNode>(this);
            temp.transform.parent = this.transform.parent;
            temp.NodeIsClicked = true;
            temp.PreSlot = SettedSlot;
            temp.ChangeStack(0);
            temp.ChangeStack(cur);
            this.ChangeStack(-cur);
        }
        else
        {
            temp = SettedSlot.GetSettingNode();
        }

        return temp;
    }


    //������ ũ�⸦ ����
    public virtual Vector2Int GetSize()
    {
        return Vector2Int.zero;
    }

    //�ش� �������� �ϳ� ����� �������ش�.
    public virtual BaseNode GetDuplicateNode()
    {
        return null;
    }
    //������ ����
    public virtual void ItemMerge(BaseNode node)
    {

    }
    //�������� ������ ����
    public virtual void ChangeStack(int val)
    {
        
    }
    //�������� ������ ����
    public virtual int GetStack()
    {
        return -1;
    }
    //�����۾��̵� ����
    public virtual int GetItemID()
    {
        return -1;
    }
    //������ ������ ���������� �ƴ��� ����
    public virtual bool IsStackAble()
    {
        return false;
    }


    public virtual void Update()
    {
        
    }

    
}
