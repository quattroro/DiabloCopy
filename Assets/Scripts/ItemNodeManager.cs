using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����ۿ� ���� ������ csv ���Ͽ��� �޾ƿ��� ���ο� ������ ��尡 �ʿ��Ҷ����� �����ϰ� �ʱ�ȭ�ؼ� �����ش�.
public class ItemNodeManager : Singleton<ItemNodeManager>
{
    public ItemDataLoader dataloader;
    public BaseNode nodeprefab;
    public Sprite[] itemsprites;
    public ItemInfoPanel infoPanel;
    public ItemObj itemobj;

    //������ �ڵ带 �̿��ؼ� �ش� �ڵ��� ������ �̸��� ���� ���ش�.
    public string GetItemName(int code)
    {
        return dataloader.iteminfos[code][(int)EnumTypes.ItemCollums.Name];
    }

    //������ �ڵ带 �̿��ؼ� �ش� �������� �̹����� ã�Ƽ� ���� ���ش�.
    public Sprite GetItemSprite(int code)
    {
        int spritenum;
        int.TryParse(dataloader.iteminfos[code][(int)EnumTypes.ItemCollums.SpriteNum], out spritenum);
        return itemsprites[spritenum];
    }

    //���� �ε�Ǿ� �ִ� ��� �����۵��� �ڵ带 ����Ʈ�� ���� �Ѱ��ش�.
    public List<int> GetAllItemCode()
    {
        List<int> codelist = new List<int>();
        foreach(KeyValuePair <int, Dictionary<int, string>> a in dataloader.iteminfos)
        {
            codelist.Add(a.Key);
        }
        return codelist;
    }

    //������ ������ �ִ� �����۵��� �ڵ带 ����Ʈ�� ���� �Ѱ��ش�.
    public List<int> GetSelectItemCode(EnumTypes.ItemTypes min, EnumTypes.ItemTypes max)
    {
        List<int> codelist = new List<int>();
        foreach (KeyValuePair<int, Dictionary<int, string>> a in dataloader.iteminfos)
        {
            if(a.Key>=(int)min&&a.Key<(int)max)
                codelist.Add(a.Key);
        }
        return codelist;
    }

    
    //�ش� �ڵ��� ������ ��带 ���� �������ش�.
    public BaseNode InstantiateNode(int itemcode, Transform parent)
    {
        Dictionary<int, string> data = dataloader.iteminfos[itemcode];

        BaseNode copynode = GameObject.Instantiate<BaseNode>(nodeprefab);
        copynode.transform.parent = parent;

        int rannum = Random.Range(0, dataloader.iteminfos.Count);



        int code;
        int.TryParse(data[(int)EnumTypes.ItemCollums.ItemCode], out code);
        string name = data[(int)EnumTypes.ItemCollums.Name];
        int spritenum;
        int.TryParse(data[(int)EnumTypes.ItemCollums.SpriteNum], out spritenum);
        string category = data[(int)EnumTypes.ItemCollums.Category];

        string parts = data[(int)EnumTypes.ItemCollums.Parts];


        copynode.InitSetting(data, itemsprites[spritenum]);
        
        
        return copynode;
    }

    //�ش� �ڵ��� ������ ��带 ���� �������ش�.
    public ItemObj InstantiateItemObj(int itemcode, string itemname, Vector3 pos)
    {
        ItemObj copy = GameObject.Instantiate<ItemObj>(itemobj);
        copy.InitItem(itemcode, itemname, pos);

        return copy;
    }

    private void Awake()
    {
        itemsprites = Resources.LoadAll<Sprite>("Sprites/items");
        infoPanel = GetComponentInChildren<ItemInfoPanel>();
        infoPanel.gameObject.SetActive(false);
        itemobj = Resources.Load<ItemObj>("Prefabs/ItemObj");
    }

    //������ ���� �г��� ���� ���ش�.
    public ItemInfoPanel getInfoPanel()
    {
        if (infoPanel.gameObject.activeSelf == false)
            infoPanel.gameObject.SetActive(true);
        return infoPanel;
    }

    // Start is called before the first frame update
    void Start()
    {
        dataloader = GetComponent<ItemDataLoader>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //
        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    //�������� �ϳ� �������� �����ؼ� �����۰��濡 �־��ش�.  
        //    Debug.Log("������ ����");
        //    BaseNode copynode = InstantiateNode(1001, this.transform);
        //    ItemBag.Instance.InsertItem(copynode);
        //}

        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    //�������� �ϳ� �������� �����ؼ� �����۰��濡 �־��ش�.  
        //    Debug.Log("������ ����");
        //    BaseNode copynode = InstantiateNode(2001, this.transform);
        //    ItemBag.Instance.InsertItem(copynode);
        //}
    }
}
