using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item:MonoBehaviour
{
    //기본 정보들을 가지고 있으면서도 동작함수를 구현하게 하기 위해 추상클래스로 작성
    public enum ITEMS
    {
        Helmet,
        Axe,
        Staff,
        HPPotion,
        MPPotion,
        HeavyArmor,
        LightArmor,
        ItemMax
    }


    public enum ITEMTYPE
    {
        USEABLE,
        EQUIP,
    }

    public enum EQUIPPARTS
    {
        HEAD,
        RIGHTARM,
        BODY,
        LEFTARM,
        AMULET,
        RINGS1,
        RINGS2,
        PARTSMAX
    }

    public string name;//자기 이름
    public ITEMTYPE type;//아이템 타입
    public int x, y;//아이템이 차지하는 인벤토리 크기
    public bool NowEquipped;//현재 장착하고 있는지
    public EQUIPPARTS parts;//장착 가능 부위
    public int stock = 1;//사용가능 아이템은 여러개 스택이 가능
    public Sprite itemsprite;
    public int myindex;
    //public int y;


    public abstract void Operate();//아이템이 수행할 동작


    public Sprite GetSprite()
    {
        return itemsprite;
    }

    public int GetX()
    {
        return x;
    }

    public int GetY()
    {
        return y;
    }

    public int GetStock()
    {
        return stock;
    }




    public Item(string name, ITEMTYPE type,int xsize,int ysize)
    {
        this.name = name;
        this.type = type;
        itemsprite = Resources.Load<Sprite>("Item" + name);
        this.x = xsize;
        this.y = ysize;
        Debug.Log("부모클래스 세팅 : " + name + " " + type);
    }

    public Item(string name, ITEMTYPE type, int xsize, int ysize, EQUIPPARTS parts)
    {
        this.name = name;
        this.type = type;
        itemsprite = Resources.Load<Sprite>("Item" + name);
        this.x = xsize;
        this.y = ysize;
        this.parts = parts;
        Debug.Log("부모클래스 세팅 : " + name + " " + type);
    }



}