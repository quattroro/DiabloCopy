using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public enum ABLESTAT {  STR, MGC, DEX, VIT, HP, MP };//���� ������ ����
    public enum STATS { Name, Level, Exp, NextExp, Gold, STR, MGC, DEX, VIT, StatPoint, HP, MP, ArmorClass, ToHit, Damege, ResisMGC, ResisFire, ResisLGT, STATMAX };
    [Header("=====Status=====")]
    public string Myname;
    public int curhp;
    public int curmp;
    public int maxhp;
    public int maxmp;
    public int strength;
    public int magic;
    public int dexterity;
    public int vitality;
    public int pointstodistribute;
    public int level=1;
    public int exprtience;
    public int nextlevel = 400;
    public int gold;
    public int armorclass;
    public int tohit;
    public int damage;
    public int magicdamage;
    public int resistmagic;
    public int resisfire;
    public int resislightning;
    //[Header("==========")]
    public string Name
    {
        get
        {
            return Myname;
        }
        set
        {
            Myname = value;
        }
    }
    public int CurHP
    {
        get
        {
            return curhp;
        }
        set
        {
            curhp = value;
        }
    }
    public int CurMP
    {
        get
        {
            return curmp;
        }
        set
        {
            curmp = value;
        }
    }
    public int MaxHP//�ִ� ü��
    {
        get
        {
            return maxhp;
        }
        set
        {
            maxhp = value;
        }
    }
    public int MaxMP//�ִ� ����
    {
        get
        {
            return maxmp;
        }
        set
        {
            maxmp = value;
        }
    }

    public int Strength//��
    {
        get
        {
            return strength;
        }
        set
        {
            strength = value;
        }
    }

    public int Magic//����
    {
        get
        {
            return magic;
        }
        set
        {
            magic = value;
        }
    }

    public int Dexterity//��ø
    {
        get
        {
            return dexterity;
        }
        set
        {
            dexterity = value;
        }
    }

    public int Vitality//ü��
    {
        get
        {
            return vitality;
        }
        set
        {
            vitality = value;
        }
    }

    public int PointsToDistribute//��������Ʈ
    {
        get
        {
            return pointstodistribute;
        }
        set
        {
            pointstodistribute = value;
        }
    }

    public int Level//���� 
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }
    public int Experience//����ġ
    {
        get
        {
            return exprtience;
        }
        set
        {
            exprtience = value;
        }
    }
    public int NextLevel//���������� ���� ����ġ
    {
        get
        {
            return nextlevel;
        }
        set
        {
            nextlevel = value;
        }
    }

    public int Gold//���
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
        }
    }

    public int ArmorClass//
    {
        get
        {
            return armorclass;
        }
        set
        {
            armorclass = value;
        }
    }
    public int ToHit//
    {
        get
        {
            return tohit;
        }
        set
        {
            tohit = value;
        }
    }
    public int Damage//������
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }
    public int MagicDamege
    {
        get
        {
            return magicdamage;
        }
        set
        {
            magicdamage = value;
        }
    }
    public int ResistMagic//��������
    {
        get
        {
            return resistmagic;
        }
        set
        {
            resistmagic = value;
        }
    }
    public int ResisFire//������
    {
        get
        {
            return resisfire;
        }
        set
        {
            resisfire = value;
        }
    }
    public int ResisLightning//�ż�����
    {
        get
        {
            return resislightning;
        }
        set
        {
            resislightning = value;
        }
    }




    public int GetStatElement(STATS element)
    {
        switch (element)
        {
            case STATS.Level:
                return Level;
            case STATS.Exp:
                return Experience;
            case STATS.NextExp:
                return NextLevel;
            case STATS.Gold:
                return Gold;
            case STATS.STR:
                return Strength;
            case STATS.MGC:
                return Magic;
            case STATS.DEX:
                return Dexterity;
            case STATS.VIT:
                return Vitality;
            case STATS.StatPoint:
                return PointsToDistribute;
            case STATS.HP:
                return MaxHP;
            case STATS.MP:
                return MaxMP;
            case STATS.ArmorClass:
                return ArmorClass;
            case STATS.ToHit:
                return ToHit;
            case STATS.Damege:
                return Damage;
            case STATS.ResisMGC:
                return ResistMagic;
            case STATS.ResisFire:
                return ResisFire;
            case STATS.ResisLGT:
                return ResisLightning;
        }
        return -1;
    }

    //MP,HP ������Ű�� ��������Ʈ 3���� �ش�.
    public virtual void LevelUp()
    {
        level++;
        NextLevel = Experience + 1000;
        Experience = 0;
        PointsToDistribute += 3;
        MaxMP += 100;
        curmp = maxmp;
        MaxHP += 100;
        curhp = maxhp;
        
    }


    public virtual void GetExp(int exp)//����ġ�� �÷��ش�.
    {
        Experience += exp;
        if(Experience>=NextLevel)
        {
            LevelUp();
        }

    }

    public virtual void StatUp(int stat)//������ ����Ʈ�� ����� ������ ������ų�� ���
    {
        if(PointsToDistribute<=0)
        {
            return;
        }
        pointstodistribute--;
        switch((ABLESTAT)stat)
        {
            case ABLESTAT.HP:
                MaxHP += 50;
                break;

            case ABLESTAT.MP:
                MaxMP += 50;
                break;

            case ABLESTAT.STR:
                Strength += 1;
                break;

            case ABLESTAT.MGC:
                Magic += 1;
                break;

            case ABLESTAT.DEX:
                Dexterity += 1;
                break;

            case ABLESTAT.VIT:
                Vitality += 1;
                break;
          

        }
    }










    // Start is called before the first frame update
    void Start()
    {
        CurHP = MaxHP;
        CurMP = MaxMP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
