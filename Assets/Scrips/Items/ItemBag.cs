using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBag : MonoBehaviour
{
    public int[,] Inventory;//�ش� �������� ����Ʈ������ �ε����� ����
    public int[] test1;
    public int[] test2;
    public int[] test3;
    public int[] test4;
    public List<Item> Itemlist = new List<Item>();//���濡 ������ �������� ����
    //public List<>
    public int testcurX, testcurY;
    public string testItemName;
    public int[] QuickSlot;
    public int x, y;
    //public List<Item> EquippedItem = new List<Item>();//���� ������ �������� ���� ����
    public Item[] EquippedItem = new Item[(int)Item.EQUIPPARTS.PARTSMAX];
    



    //public ItemBag(string name, Item.ITEMTYPE type) : base(name, type)
    //{
        
    //}


    //public override void Operate()
    //{
    //    OpenSlotWindow();
    //}


    //���������� ����Ʈ���� ���� ���� ������ ����Ʈ�� �ִ´�.
    public void EquipItem(Item item)
    {
        if (item.type == Item.ITEMTYPE.EQUIP)
        {
            item.Operate();
        }
    }

    //�����ϸ� �ش� ������ �η� �ٲ۴�
    public void UnEuqipItem(Item item)
    {

    }

    //������ �κ��丮 â�� ���� ����Լ�.
    public void OpenSlotWindow()
    {
        List<int> openlist = new List<int>();
        for (int y = 0; y < this.y; y++)
        {
            for(int x=0;x<this.x;x++)
            {
                if(Inventory[y,x]!=-1)
                {
                    //�κ��丮�� �� ���鼭 ������� ������
                    //�ش� �������� �ε�����ȣ�� �����Ѵ�.
                    openlist.Add(Inventory[y, x]);
                    //�׷��� uiȭ�鿡 �����ش�.

                }
            }
        }
    }

    public void UseItem(int indexX, int indexY)
    {
        Itemlist[Inventory[indexY, indexX]].Operate();
    }
    public void UseItem(int index)
    {
        Itemlist[index].Operate();

    }

    public void UseQuickSlotItem(int Index)
    {
        Itemlist[QuickSlot[Index]].Operate();
    }

    //���濡 �������� �߰��Ѵ�.
    public void Add(Item item)
    {
        int x = 0, y = 0;
        int itemX =0;
        int itemY  =0;
        bool flag = false;
        int size = item.x * item.y;
        int count = 0;
        bool active = false;

        //if(gameObject.activeSelf==false)
        //{
        //    gameObject.SetActive(true);
        //    active = true;
        //}

        ////�������� ������ ������ �������̸� ������ ����Ʈ���� �ش� �������� �����ϰ� �ִ��� Ȯ���ϰ� ������ ������ ������Ų��.
        //if (item.type == Item.ITEMTYPE.USEABLE)
        //{
        //    for (int i = 0; i < Itemlist.Count; i++)
        //    {
        //        if (Itemlist[i].name == item.name)
        //        {
                    
        //            Itemlist[i].stock++;
        //            Debug.Log($"������ ���� ���� name = {Itemlist[i].name}");
        //            return;
        //        }
        //    }
        //}


        //���ٺ��� ���鼭 ������� ������ �ش� �������� 
        for (y = 0; y < Inventory.GetLength(0); y++)
        {
            for (x = 0; x < Inventory.GetLength(1); x++)
            {
                //�迭���� ����ִ� ������ ������ �ش� �������� �������� ũ�⸸ŭ ��������� �������� �ִ´�.
                if (Inventory[y, x] == -1)
                {
                    if (count == 0)
                    {
                        //ù��°�� ī������ �ϴ� �ε����� �����Ѵ�.
                        itemX = x;
                        itemY = y;
                    }
                    count++;
                    if (count >= size)
                    {
                        //�������� ũ�⸸ŭ ��������� ������ ����
                        flag = true;
                        break;
                    }
                    if (count % item.x == 0)
                    {
                        //�������� xũ�⸸ŭ ���� �����ٷ� �Ѿ�� ���� -1���� Ȯ���Ѵ�.
                        break;
                    }
                }
                else
                {
                    //������� ������ ī��Ʈ�� �ʱ�ȭ��
                    count = 0;
                }
            }
            if (flag)
            {
                break;
            }
        }

        //�������� �����Ҹ�ŭ ������ �Ӻ�������� �ش� ������ �ش� �������� ����Ʈ������ �ε�����ȣ�� �־��ش�.
        if (flag)
        {
            for (y = 0; y < item.y; y++)
            {
                for (x = 0; x < item.x; x++)
                {
                    Inventory[y+itemY, x+itemX] = Itemlist.Count;
                }
            }
            testcurX = itemX;
            testcurY = itemY;
            testItemName = item.name;
            item.myindex = Itemlist.Count;
            Debug.Log($"������ �߰� name = {item.name}, ��ġ ({itemX},{itemY})");
            Itemlist.Add(item);
        }

        //if(active)
        //{
        //    gameObject.SetActive(false);
        //}

    }

    //public void EquipItem(Item item)
    //{
    
    //}


    //������ �������� �����Ѵ�.
    public void Remove(Item item)
    {

    }

    //������ �������� �ű��.
    public void ItemMove(Item item, int x, int y)
    {




    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("create itembag");
        QuickSlot = new int[8];
        x = 10;
        y = 4;
        Inventory = new int[y, x];

        test1 = new int[x];
        test2 = new int[x];
        test3 = new int[x];
        test4 = new int[x];

        for (int y = 0; y < Inventory.GetLength(0); y++)
        {
            for (int x = 0; x < Inventory.GetLength(1); x++)
            {
                Inventory[y, x] = -1;
            }
        }
        for(int i=0;i<EquippedItem.GetLength(0);i++)
        {
            EquippedItem[i] = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        for(int i=0;i<10;i++)
        {
            test1[i] = Inventory[0, i];
            test2[i] = Inventory[1, i];
            test3[i] = Inventory[2, i];
            test4[i] = Inventory[3, i];
        }


    }
}
