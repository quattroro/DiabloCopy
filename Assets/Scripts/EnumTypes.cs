using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enum들
public class EnumTypes
{
    //아이템csv 파일의 칼럼들
    public enum ItemCollums
    {
        ItemCode,
        Name,
        Damage,
        Durability,
        StrRequire,
        DexRequire,
        Price,
        QualityLevel,
        Category,
        Parts,
        Grade,
        SpriteNum,
        Size,
        CollumMax
    }

    //슬롯 타입들
    public enum SlotTypes
    {
        Item,
        Equip,
        Quick,
        TypeMax
    }
    //아이템 타입들
    public enum ItemTypes
    {
        Equips = 1000 ,
        StackAble = 2000,
        TypeMax =3000,
    }
    
    //장비 타입들
    public enum EquipmentTypes
    {
        Head,
        Body,
        RightArm,
        LeftArm,
        TwoHand,
        Amulet,
        Ring,
        EquipMax
    }

    //
    //public enum RecipeCollums
    //{
    //    ResultItem,
    //    ResultName,
    //    Count,
    //    Slot1,
    //    Slot2,
    //    Slot3,
    //    Slot4,
    //    Slot5,
    //    Slot6,
    //    Slot7,
    //    Slot8,
    //    Slot9,
    //    CollumMax
    //}

    
}
