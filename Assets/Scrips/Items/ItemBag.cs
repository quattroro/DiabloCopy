using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBag : MonoBehaviour
{
    public int[,] Inventory;//해당 아이템의 리스트에서의 인덱스를 저장
    public int[] test1;
    public int[] test2;
    public int[] test3;
    public int[] test4;
    public List<Item> Itemlist = new List<Item>();//가방에 수납된 아이템을 저장
    //public List<>
    public int testcurX, testcurY;
    public string testItemName;
    public int[] QuickSlot;
    public int x, y;
    //public List<Item> EquippedItem = new List<Item>();//현재 장착된 아이템은 따로 보관
    public Item[] EquippedItem = new Item[(int)Item.EQUIPPARTS.PARTSMAX];
    



    //public ItemBag(string name, Item.ITEMTYPE type) : base(name, type)
    //{
        
    //}


    //public override void Operate()
    //{
    //    OpenSlotWindow();
    //}


    //수납아이템 리스트에서 빼서 장착 아이템 리스트로 넣는다.
    public void EquipItem(Item item)
    {
        if (item.type == Item.ITEMTYPE.EQUIP)
        {
            item.Operate();
        }
    }

    //해제하면 해당 부위는 널로 바꾼다
    public void UnEuqipItem(Item item)
    {

    }

    //가방의 인벤토리 창을 여는 멤버함수.
    public void OpenSlotWindow()
    {
        List<int> openlist = new List<int>();
        for (int y = 0; y < this.y; y++)
        {
            for(int x=0;x<this.x;x++)
            {
                if(Inventory[y,x]!=-1)
                {
                    //인벤토리를 쭉 돌면서 비어있지 않으면
                    //해당 아이템의 인덱스번호를 저장한다.
                    openlist.Add(Inventory[y, x]);
                    //그러곤 ui화면에 보여준다.

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

    //가방에 아이템을 추가한다.
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

        ////아이템이 스택이 가능한 아이템이면 아이템 리스트에서 해당 아이템을 소지하고 있는지 확인하고 있으면 갯수만 증가시킨다.
        //if (item.type == Item.ITEMTYPE.USEABLE)
        //{
        //    for (int i = 0; i < Itemlist.Count; i++)
        //    {
        //        if (Itemlist[i].name == item.name)
        //        {
                    
        //            Itemlist[i].stock++;
        //            Debug.Log($"아이템 개수 증가 name = {Itemlist[i].name}");
        //            return;
        //        }
        //    }
        //}


        //윗줄부터 돌면서 빈공간이 있음면 해당 공간부터 
        for (y = 0; y < Inventory.GetLength(0); y++)
        {
            for (x = 0; x < Inventory.GetLength(1); x++)
            {
                //배열에서 비어있는 공간이 있으면 해당 공간에서 아이템의 크기만큼 비어있으면 아이템을 넣는다.
                if (Inventory[y, x] == -1)
                {
                    if (count == 0)
                    {
                        //첫번째로 카운팅을 하는 인덱스를 저장한다.
                        itemX = x;
                        itemY = y;
                    }
                    count++;
                    if (count >= size)
                    {
                        //아이템의 크기만큼 비어있으면 아이템 수납
                        flag = true;
                        break;
                    }
                    if (count % item.x == 0)
                    {
                        //아이템의 x크기만큼 오면 다음줄로 넘어가서 역시 -1인지 확인한다.
                        break;
                    }
                }
                else
                {
                    //비어있지 않으면 카운트를 초기화함
                    count = 0;
                }
            }
            if (flag)
            {
                break;
            }
        }

        //아이템을 수납할만큼 공간ㅇ ㅣ비어있으면 해당 공간에 해당 아이템의 리스트에서의 인덱스번호를 넣어준다.
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
            Debug.Log($"아이템 추가 name = {item.name}, 위치 ({itemX},{itemY})");
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


    //가방의 아이템을 삭제한다.
    public void Remove(Item item)
    {

    }

    //가방의 아이템을 옮긴다.
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
