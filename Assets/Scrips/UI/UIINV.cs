using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//public class UIINV : MonoBehaviour, BaseUI
//{
//    enum INVELEMENTS { Head, RightArm, Body, LeftArm, Amulet, Rings1,Rings2, INVMAX };

//    public Transform[] Elements = new Transform[(int)INVELEMENTS.INVMAX];

//    public Transform SlotStartpos = null;

//    public Player SC_Player = null;
//    //public Image Image = null;
//    public List<Item> itemlist = null;
//    public Item[] Equipped = null;

//    public Image[] Items = new Image[(int)Item.ITEMS.ItemMax];
//    public Button[] Items2 = new Button[(int)Item.ITEMS.ItemMax];


//    public List<int> openlist = new List<int>();

//    public int[,] inventory;
//    public int SlotSize = 25;
//    public int SlotInterval = 2;

//    public void setActive(bool val)
//    {
//        this.gameObject.SetActive(val);
        
//    }

//    //현재 장착된 아이템을 부위별로 가지고 있는다.
//    public void isEquipe()
//    {

//    }

//    public void BTNClickProc()
//    {
//        //플레이어의 아이템백에서 정보를 받아와서 보여준다.
//        if(gameObject.activeSelf==false)
//        {
//            this.gameObject.SetActive(true);
//        }
//        else
//        {
//            this.gameObject.SetActive(false);
//            return;
//        }
//        itemlist = SC_Player.itemBag.Itemlist;
//        inventory = SC_Player.itemBag.Inventory;
//        Equipped = SC_Player.itemBag.EquippedItem;//현재 장착된 아이템 정보를 따로 배열로 가지고 있는다.
        

//        for (INVELEMENTS i = INVELEMENTS.Head; i < INVELEMENTS.INVMAX; i++)
//        {
//            //Image temp;
//            //temp = Elements[(int)i].GetComponent<Image>();
//            //temp.sprite= Equipped[(int)i].GetSprite();
//            if (Equipped[(int)i] != null)
//            {
//                Elements[(int)i].GetComponent<Image>().sprite = Equipped[(int)i].GetSprite();

//            }

//        }

//        for(int y=0;y<inventory.GetLength(0);y++)
//        {
//            for(int x=0;x<inventory.GetLength(1);x++)
//            {
//                if(inventory[y, x]!=-1)
//                {
//                    //인벤토리의 값이 리스트에 없을때
//                    if(!openlist.Contains(inventory[y,x]))
//                    {
//                        Vector3 startpos = SlotStartpos.localPosition;
//                        openlist.Add(inventory[y, x]);
//                        Item temp = itemlist[inventory[y, x]];                        
//                        //해당 아이템의 크기와 스프라이트를 받아와서 크기대로 이미지를 만들어 주고 xy 값에 따라 위치 잡아서 위치해준다.
//                        //Image copy = GameObject.Instantiate<Image>(Elements[(int)INVELEMENTS.SLOT]);

//                        Transform copy = GameObject.Instantiate(this.transform.Find($"Item_{temp.name}_UI"));
//                        copy.GetComponent<UIItemScript>().SetItem(temp);
//                        copy.SetParent(this.transform);
//                        //해당 위치로 잡아주고
//                        copy.transform.localPosition = new Vector3(startpos.x + (x * 27), startpos.y - (y * 27));

//                        //this.transform.Find($"Item_{temp.name}_UI");


//                        //copy.rectTransform.localPosition
//                        //copy.rectTransform.size







//                    }
//                }
                
//            }
//        }


//        //itemlist=SC_Player.itemBag





//    }

//    //원소들 배열에 넣고 슬롯생성하고 슬롯 배열에 넣고 
//    private void Start()
//    {
//        for(INVELEMENTS i=INVELEMENTS.Head;i<INVELEMENTS.INVMAX;i++)
//        {
//            Elements[(int)i] = transform.Find($"{i.ToString()}");
//        }
//    }

//    private void Update()
//    {
        
//    }
//}
