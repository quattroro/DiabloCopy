using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item:MonoBehaviour
{
    //�⺻ �������� ������ �����鼭�� �����Լ��� �����ϰ� �ϱ� ���� �߻�Ŭ������ �ۼ�
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

    public string name;//�ڱ� �̸�
    public ITEMTYPE type;//������ Ÿ��
    public int x, y;//�������� �����ϴ� �κ��丮 ũ��
    public bool NowEquipped;//���� �����ϰ� �ִ���
    public EQUIPPARTS parts;//���� ���� ����
    public int stock = 1;//��밡�� �������� ������ ������ ����
    public Sprite itemsprite;
    public int myindex;
    //public int y;


    public abstract void Operate();//�������� ������ ����


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
        Debug.Log("�θ�Ŭ���� ���� : " + name + " " + type);
    }

    public Item(string name, ITEMTYPE type, int xsize, int ysize, EQUIPPARTS parts)
    {
        this.name = name;
        this.type = type;
        itemsprite = Resources.Load<Sprite>("Item" + name);
        this.x = xsize;
        this.y = ysize;
        this.parts = parts;
        Debug.Log("�θ�Ŭ���� ���� : " + name + " " + type);
    }



}