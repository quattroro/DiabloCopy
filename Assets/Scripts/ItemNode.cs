using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//각각의 아이템 노드는 클릭은 mouseinputmanager에서 raycast를 이용해서 동작하고, 아이템 정보창 출력을 위한 마우스 enter, exit는 인터페이스를 이용해서 동작한다.
public class ItemNode : BaseNode,IPointerOverLay
{
    //ItemInfoPanel infopanel;


    [Header("아이템 데이터")]
    public string itemName;//아이템 이름
    public int itemCode;//아이템 코드
    public Sprite itemSprite;//아이템 이미지
    public int spritenum;
    public float[] damage = new float[2];

    public float durability;//아이템 내구도

    public float strrequire;

    public float dexrequire;

    public float price;

    public int qurlitylevel;

    public int grade;

    public Vector2Int itemsize;//아이템이 인벤토리창에서 차지하는 크기

    public EnumTypes.ItemTypes itemtype;//아이템 타입

    public EnumTypes.EquipmentTypes equiptype;//장착 부위 

    [Header("=======================================")]

    public int _Stack;//아이템 개수

    public bool _isstackable = false;//해당 아이템이 스택이 가능한 아이템인지 여부

    [SerializeField]
    private bool _isShowInfoWindow = false;

    [SerializeField]
    private bool _isMouseOn = false;

    public float ShowInfoTime = 0.0f;


    public Text stacktext;//개수 표시 텍스트

    public Dictionary<int, string> itemdata;

    public Vector3 mousepos;

    //현재 아이템 창을 보여주고 있는지
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
                //아이템 정보 윈도우를 보여줄때는 현재 마우스의 위치를 함께 넘겨 준다.
                ShowItemInfoWindow(Input.mousePosition+new Vector3(10,-10,0));
            }
            else
            {
                HideItemInfoWindow();
            }


        }
    }

    //현재 마우스가 노드 위로 올라왔는지
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

    //아이템 정보 패널을 보여줌 아이템 정보 패널은 ItemNodeManager가 가지고 있는다.
    public void ShowItemInfoWindow(Vector3 pos)
    {
        Debug.Log("패널 보여줌");
        ItemNodeManager.Instance.getInfoPanel().ShowInfos(this,pos);
    }

    //아이템 정보 패널을 숨겨줌
    public void HideItemInfoWindow()
    {
        Debug.Log("패널 안보여줌");
        ItemNodeManager.Instance.getInfoPanel().HideInfos();
    }

    //마우스가 노드의 위에 들어오면 사용자가 설정한 시간만큼 0.1초씩 카운트를 한다.
    //마우스가 노드의 위에 설정한 시간만큼 올라와 있으면 그때 아이템 정보창을 출력한다.
    IEnumerator TimeCount()
    {
        Debug.Log("마우스 카운팅 시작");
        float count = 0.0f;

        while(true)
        {
            count += 0.1f;
            if (count >= ShowInfoTime)
            {
                IsShowInfoWindow = true;
                yield break;
            }

            if (NodeIsClicked || IsMouseOn == false)//노드 위에서 나가거나 노드를 클릭하면 나간다.
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

    //아이템 타입 리턴
    public override EnumTypes.ItemTypes GetItemTypes()
    {
        return itemtype;
    }
    //아이템 장비 타입 리턴
    public override EnumTypes.EquipmentTypes GetEquipTypes()
    {
        return equiptype;
    }

    //초기 세팅
    public override void InitSetting(Dictionary<int, string> itemdata,Sprite sprite)
    {
        string[] temp;
        int x, y;
        //아이템 정보들 초기화 해주고
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

        //스택이 가능한 아이템 인지 아닌지 판단
        if (itemdata[(int)EnumTypes.ItemCollums.Category] == EnumTypes.ItemTypes.StackAble.ToString())
        {
            //Debug.Log($"욜로들어옴");
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

        //장비타입 설정
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

        //이미지 설정
        GetComponent<Image>().sprite = sprite;
        ChangeStack(+1);

        originsize.x = itemsize.x * 61;
        originsize.y = itemsize.y * 54;

        NodeIsActive = true;
    }


    //초기세팅
    public override void InitSetting(int itemcode, string Name, Sprite sprite, Vector3 pos, string _type)
    {
        this.name = Name;
        this.itemName = Name;
        this.itemCode = itemcode;
        this.itemSprite = sprite;
        this.transform.localPosition = pos;
        
        if (_type == EnumTypes.ItemTypes.StackAble.ToString())
        {
            //Debug.Log($"욜로들어옴");
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

    //개수를 변경해준다.
    public override void ChangeStack(int val)
    {
        //0이 들어오면 초기화
        if (val == 0)
        {
            _Stack = 0;
            return;
        }
            
        //아이템은 최대 99개 최소 1개
        _Stack += val;

        if (_Stack > 99)
            _Stack = 99;
        if (_Stack < 1)
            _Stack = 1;

        //1개일때는 숫자가 표시되지 않도록
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

    //아이템 1개를 나눠서 리턴해준다.
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

    //아이템 병합
    public override void ItemMerge(BaseNode node)
    {
        if(node.GetItemID() == this.GetItemID())
        {
            this.ChangeStack(node.GetStack());
            GameObject.Destroy(node.gameObject);
        }
    }

    //아이템 개수 리턴
    public override int GetStack()
    {
        return _Stack;
    }
    //스택이 가능한 아이템인지
    public override bool IsStackAble()
    {
        return _isstackable;
    }
    //아이템 코드를 리턴
    public override int GetItemID()
    {
        return itemCode;
    }

    //아이템 크기를 리턴
    public override Vector2Int GetSize()
    {
        return itemsize;
    }

    

    //아이템이 클릭되었을땐 마우스를 따라다니게 해준다.
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
        //Debug.Log("마우스 들어옴");
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
