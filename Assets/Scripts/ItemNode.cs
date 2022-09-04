using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//������ ������ ���� Ŭ���� mouseinputmanager���� raycast�� �̿��ؼ� �����ϰ�, ������ ����â ����� ���� ���콺 enter, exit�� �������̽��� �̿��ؼ� �����Ѵ�.
public class ItemNode : BaseNode,IPointerOverLay
{
    //ItemInfoPanel infopanel;


    [Header("������ ������")]
    public string itemName;//������ �̸�
    public int itemCode;//������ �ڵ�
    public Sprite itemSprite;//������ �̹���
    public int spritenum;
    public float[] damage = new float[2];

    public float durability;//������ ������

    public float strrequire;

    public float dexrequire;

    public float price;

    public int qurlitylevel;

    public int grade;

    public Vector2Int itemsize;//�������� �κ��丮â���� �����ϴ� ũ��

    public EnumTypes.ItemTypes itemtype;//������ Ÿ��

    public EnumTypes.EquipmentTypes equiptype;//���� ���� 

    [Header("=======================================")]

    public int _Stack;//������ ����

    public bool _isstackable = false;//�ش� �������� ������ ������ ���������� ����

    [SerializeField]
    private bool _isShowInfoWindow = false;

    [SerializeField]
    private bool _isMouseOn = false;

    public float ShowInfoTime = 0.0f;


    public Text stacktext;//���� ǥ�� �ؽ�Ʈ

    public Dictionary<int, string> itemdata;

    public Vector3 mousepos;

    //���� ������ â�� �����ְ� �ִ���
    public bool IsShowInfoWindow
    {
        get
        {
            return _isShowInfoWindow;
        }
        set
        {
            _isShowInfoWindow = value;
            if (value)
            {
                //������ ���� �����츦 �����ٶ��� ���� ���콺�� ��ġ�� �Բ� �Ѱ� �ش�.
                ShowItemInfoWindow(Input.mousePosition+new Vector3(10,-10,0));
            }
            else
            {
                HideItemInfoWindow();
            }


        }
    }

    //���� ���콺�� ��� ���� �ö�Դ���
    public bool IsMouseOn
    {
        get
        {
            return _isMouseOn;
        }
        set
        {
            _isMouseOn = value;
            if(value)
            {
                StartCoroutine(TimeCount());
            }
            else
            {
                StopCoroutine(TimeCount());
                IsShowInfoWindow = false;
            }

        }

    }

    //������ ���� �г��� ������ ������ ���� �г��� ItemNodeManager�� ������ �ִ´�.
    public void ShowItemInfoWindow(Vector3 pos)
    {
        Debug.Log("�г� ������");
        ItemNodeManager.Instance.getInfoPanel().ShowInfos(this,pos);
    }

    //������ ���� �г��� ������
    public void HideItemInfoWindow()
    {
        Debug.Log("�г� �Ⱥ�����");
        ItemNodeManager.Instance.getInfoPanel().HideInfos();
    }

    //���콺�� ����� ���� ������ ����ڰ� ������ �ð���ŭ 0.1�ʾ� ī��Ʈ�� �Ѵ�.
    //���콺�� ����� ���� ������ �ð���ŭ �ö�� ������ �׶� ������ ����â�� ����Ѵ�.
    IEnumerator TimeCount()
    {
        Debug.Log("���콺 ī���� ����");
        float count = 0.0f;

        while(true)
        {
            count += 0.1f;
            if (count >= ShowInfoTime)
            {
                IsShowInfoWindow = true;
                yield break;
            }

            if (NodeIsClicked || IsMouseOn == false)//��� ������ �����ų� ��带 Ŭ���ϸ� ������.
            {
                IsShowInfoWindow = false;
                yield break;
            }

            yield return new WaitForSeconds(0.1f);
        }

       
    }

    public override void Awake()
    {
        base.Awake();
        
    }

    //������ Ÿ�� ����
    public override EnumTypes.ItemTypes GetItemTypes()
    {
        return itemtype;
    }
    //������ ��� Ÿ�� ����
    public override EnumTypes.EquipmentTypes GetEquipTypes()
    {
        return equiptype;
    }

    //�ʱ� ����
    public override void InitSetting(Dictionary<int, string> itemdata,Sprite sprite)
    {
        string[] temp;
        int x, y;
        //������ ������ �ʱ�ȭ ���ְ�
        this.itemdata = itemdata;
        itemName = itemdata[(int)EnumTypes.ItemCollums.Name];
        temp = itemdata[(int)EnumTypes.ItemCollums.Size].Split(',');
        int.TryParse(temp[0], out x);
        int.TryParse(temp[1], out y);
        itemsize = new Vector2Int(x, y);

        int.TryParse(itemdata[(int)EnumTypes.ItemCollums.ItemCode], out itemCode);
        int.TryParse(itemdata[(int)EnumTypes.ItemCollums.SpriteNum], out spritenum);
        temp = itemdata[(int)EnumTypes.ItemCollums.Damage].Split(',');
        for (int i = 0; i < temp.Length; i++)
        {
            float.TryParse(temp[i], out damage[i]);
        }

        //������ ������ ������ ���� �ƴ��� �Ǵ�
        if (itemdata[(int)EnumTypes.ItemCollums.Category] == EnumTypes.ItemTypes.StackAble.ToString())
        {
            //Debug.Log($"��ε���");
            this._isstackable = true;
            this.itemtype = EnumTypes.ItemTypes.StackAble;
        }
        else
        {
            this._isstackable = false;
            this.itemtype = EnumTypes.ItemTypes.Equips;
        }


        float.TryParse(itemdata[(int)EnumTypes.ItemCollums.Durability], out durability);
        float.TryParse(itemdata[(int)EnumTypes.ItemCollums.Durability], out strrequire);
        float.TryParse(itemdata[(int)EnumTypes.ItemCollums.Durability], out dexrequire);
        float.TryParse(itemdata[(int)EnumTypes.ItemCollums.Durability], out price);
        int.TryParse(itemdata[(int)EnumTypes.ItemCollums.Durability], out qurlitylevel);
        int.TryParse(itemdata[(int)EnumTypes.ItemCollums.Durability], out grade);

        //���Ÿ�� ����
        if(itemtype==EnumTypes.ItemTypes.Equips)
        {
            for (EnumTypes.EquipmentTypes  i = 0; i < EnumTypes.EquipmentTypes.EquipMax; i++)
            {
                if(itemdata[(int)EnumTypes.ItemCollums.Parts]==i.ToString())
                {
                    this.equiptype = i;
                    break;
                }
            }
        }

        //�̹��� ����
        GetComponent<Image>().sprite = sprite;
        ChangeStack(+1);

        originsize.x = itemsize.x * 61;
        originsize.y = itemsize.y * 54;

        NodeIsActive = true;
    }


    //�ʱ⼼��
    public override void InitSetting(int itemcode, string Name, Sprite sprite, Vector3 pos, string _type)
    {
        this.name = Name;
        this.itemName = Name;
        this.itemCode = itemcode;
        this.itemSprite = sprite;
        this.transform.localPosition = pos;
        
        if (_type == EnumTypes.ItemTypes.StackAble.ToString())
        {
            //Debug.Log($"��ε���");
            this._isstackable = true;
            this.itemtype = EnumTypes.ItemTypes.StackAble;
        }
        else
        {
            this._isstackable = false;
            this.itemtype = EnumTypes.ItemTypes.Equips;
        }

        ChangeStack(+1);

        GetComponent<Image>().sprite = sprite;
        
    }

    //������ �������ش�.
    public override void ChangeStack(int val)
    {
        //0�� ������ �ʱ�ȭ
        if (val == 0)
        {
            _Stack = 0;
            return;
        }
            
        //�������� �ִ� 99�� �ּ� 1��
        _Stack += val;

        if (_Stack > 99)
            _Stack = 99;
        if (_Stack < 1)
            _Stack = 1;

        //1���϶��� ���ڰ� ǥ�õ��� �ʵ���
        if(_Stack == 1)
        {
            if (stacktext.gameObject.activeSelf)
                stacktext.gameObject.SetActive(false);
        }
        else
        {
            if (!stacktext.gameObject.activeSelf)
                stacktext.gameObject.SetActive(true);

            stacktext.text = string.Format("{0}", _Stack);
        }
    }

    //������ 1���� ������ �������ش�.
    public override BaseNode GetDuplicateNode()
    {
        BaseNode copynode = null;
        if (GetStack() <= 1)
            return copynode;

        copynode = GameObject.Instantiate<BaseNode>(this);
        copynode.NodeIsActive = true;
        copynode.ChangeStack(0);
        copynode.ChangeStack(1);
        this.ChangeStack(-1);

        return copynode;
    }

    //������ ����
    public override void ItemMerge(BaseNode node)
    {
        if(node.GetItemID() == this.GetItemID())
        {
            this.ChangeStack(node.GetStack());
            GameObject.Destroy(node.gameObject);
        }
    }

    //������ ���� ����
    public override int GetStack()
    {
        return _Stack;
    }
    //������ ������ ����������
    public override bool IsStackAble()
    {
        return _isstackable;
    }
    //������ �ڵ带 ����
    public override int GetItemID()
    {
        return itemCode;
    }

    //������ ũ�⸦ ����
    public override Vector2Int GetSize()
    {
        return itemsize;
    }

    

    //�������� Ŭ���Ǿ����� ���콺�� ����ٴϰ� ���ش�.
    public override void Update()
    {
        if(NodeIsActive)
        {
            if(NodeIsClicked)
            {
                if (IsShowInfoWindow == true)
                    IsShowInfoWindow = false;
                
                Vector3 temp = new Vector3(-(rectTransform.sizeDelta / 2).x, (rectTransform.sizeDelta / 2).y, 0);
                this.transform.position = Input.mousePosition ;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ////mousepos = eventData.position;
        //Debug.Log("���콺 ����");
        //if(IsMouseOn==false)
        //    IsMouseOn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if(IsMouseOn==true)
        //    IsMouseOn = false;
    }

    public bool GetIsNowOverLay()
    {
        return IsMouseOn;
    }

    public void SetIsNowOverLay(bool value)
    {
        if(!NodeIsClicked)
            IsMouseOn = value;
    }
}
