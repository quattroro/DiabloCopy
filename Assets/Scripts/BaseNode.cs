using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNode : MonoBehaviour
{
    Vector2Int Index;
    [SerializeField]
    private bool _isactive;//활성노드 / 비활성 노드
    [SerializeField]
    private bool _isclicked;//클릭된상태 / 클릭되지 않은 상태

    //[SerializeField]
    //private bool _isSnapped;//스냅에 의해 붙어있는 상태 / 스냅에 의해 붙어있지 않은 상태

    public RectTransform rectTransform;//자기 자신의 recttransform

    public BaseSlot SettedSlot;//노드가 셋팅되어 있는 슬롯

    public List<BaseSlot> SettedSlotList;//노드가 셋팅되어 있는 슬롯들


    public BaseSlot PreSlot;//노드가 클릭되어서 마우스를 따라다닐때 원래 있던 슬롯의 위치를 저장해 놓는다.


    public Transform[] castpoint = new Transform[4];//노드가 위치한곳을 탐색할때 4개의 모서리에있는 포인트를 laycast를 이용해 탐색해서 자신의 위치에 있는 슬롯을 찾아낸다.
    public Vector2 castpointpos;

    public Vector2 originsize;


    public virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

    }

    //노드의 아이템 타입을 리턴한다.
    public virtual EnumTypes.ItemTypes GetItemTypes()
    {
        return (EnumTypes.ItemTypes)(-1);
    }
    //노드의 장비 타입을 리턴한다.
    public virtual EnumTypes.EquipmentTypes GetEquipTypes()
    {
        return (EnumTypes.EquipmentTypes)(-1);
    }
    
    //네개의 모서리를 리턴한다.
    public Vector2 GetCastPoint(int index)
    {
        return castpoint[index].position - this.transform.position;
    }

    //노드가 클릭 되었을때.
    //노드가 장착되어 있는 슬롯들에게서 자신의 정보를 지워주고 자기 자신을 리턴한다.
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

    //노드 하나가 여러개의 슬롯을 차지하기 때문에 자신이 위치한 슬롯들을 리스트로 가지고 있는다.
    public void AddSettedSlotList(BaseSlot slot)
    {
        SettedSlotList.Add(slot);
    }

    //해당 노드가 상호작용 할 수 있는 활성 노드인지를 확인
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

    //클릭된 상태인지 아닌지 
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

    //초기화 함수
    public virtual void InitSetting(Dictionary<int, string> itemdata, Sprite sprite)
    {

    }
    //초기화 함수
    public virtual void InitSetting(int itemcode, string Name, Sprite sprite, Vector3 pos, string _type)
    {

    }

    //해당 노드를 절반씩 두개의 노드로 나눠준다.
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


    //아이템 크기를 리턴
    public virtual Vector2Int GetSize()
    {
        return Vector2Int.zero;
    }

    //해당 아이템을 하나 떼어내서 리턴해준다.
    public virtual BaseNode GetDuplicateNode()
    {
        return null;
    }
    //아이템 병합
    public virtual void ItemMerge(BaseNode node)
    {

    }
    //아이템의 개수를 변경
    public virtual void ChangeStack(int val)
    {
        
    }
    //아이템의 개수를 리턴
    public virtual int GetStack()
    {
        return -1;
    }
    //아이템아이디를 리턴
    public virtual int GetItemID()
    {
        return -1;
    }
    //스택이 가능한 아이템인지 아닌지 구분
    public virtual bool IsStackAble()
    {
        return false;
    }


    public virtual void Update()
    {
        
    }

    
}
