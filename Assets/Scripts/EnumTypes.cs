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

    public static Vector2Int[] dir = { new Vector2Int(0, 1), new Vector2Int(0, -1 ), new Vector2Int( 1, 0 ), new Vector2Int(-1, 0 ) };
    public static Vector2Int[] dir_all = { new Vector2Int(0, 1 ), new Vector2Int(0, -1 ), new Vector2Int(1, 0 ), new Vector2Int(-1, 0 ), new Vector2Int(1, 1 ), new Vector2Int(1, -1 ),new Vector2Int(-1, -1 ), new Vector2Int(-1, 1 ) };
    public static Vector2Int[] r_dir_all = { new Vector2Int(0, -1 ),new Vector2Int(0, 1 ), new Vector2Int(-1, 0 ), new Vector2Int(1, 0 ), new Vector2Int(-1, -1 ), new Vector2Int(-1, 1 ), new Vector2Int(1, 1 ), new Vector2Int(1, -1 ) };

    public static Dir R_Dir(Dir dir)
    {
        switch (dir)
        {
            case Dir.UP:
                return Dir.DOWN;

            case Dir.DOWN:
                return Dir.UP;

            case Dir.RIGHT:
                return Dir.LEFT;

            case Dir.LEFT:
                return Dir.RIGHT;

            case Dir.UPRIGHT:
                return Dir.DOWNLEFT;

            case Dir.DOWNRIGHT:
                return Dir.UPLEFT;

            case Dir.DOWNLEFT:
                return Dir.UPRIGHT;

            case Dir.UPLEFT:
                return Dir.DOWNRIGHT;

        }

        return Dir.DirMax;
    }

    public static int R_Dir(int dir)
    {
        switch (dir)
        {
            case (int)Dir.UP:
                return (int)Dir.DOWN;

            case (int)Dir.DOWN:
                return (int)Dir.UP;

            case (int)Dir.RIGHT:
                return (int)Dir.LEFT;

            case (int)Dir.LEFT:
                return (int)Dir.RIGHT;

            case (int)Dir.UPRIGHT:
                return (int)Dir.DOWNLEFT;

            case (int)Dir.DOWNRIGHT:
                return (int)Dir.UPLEFT;

            case (int)Dir.DOWNLEFT:
                return (int)Dir.UPRIGHT;

            case (int)Dir.UPLEFT:
                return (int)Dir.DOWNRIGHT;

        }

        return (int)Dir.DirMax;
    }

    //public enum R_Dir
    //{
    //    UP = Dir.DOWN,
    //    DOWN = Dir.UP,
    //    RIGHT = Dir.LEFT,
    //    LEFT = Dir.RIGHT,
    //    UPRIGHT = Dir.DOWNLEFT,
    //    DOWNRIGHT = Dir.UPLEFT,
    //    DOWNLEFT = Dir.UPRIGHT,
    //    UPLEFT = Dir.DOWNRIGHT,
    //    RDirMax = 8
    //}

    public enum Dir
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
        UPRIGHT,
        DOWNRIGHT,
        DOWNLEFT,
        UPLEFT,
        DirMax
    }

    public static int[] Dir_Reverse = { (int)Dir.DOWN, (int)Dir.UP, (int)Dir.LEFT, (int)Dir.RIGHT, (int)Dir.DOWNLEFT, (int)Dir.UPLEFT, (int)Dir.UPRIGHT, (int)Dir.DOWNRIGHT };
}
