using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICHAR : MonoBehaviour, BaseUI
{
    enum CHARSTATTEXT { Name, Level, Exp, NextExp, Gold, STR, MGC, DEX, VIT, StatPoint, HP, MP, ArmorClass, ToHit, Damage, ResisMGC, ResisFire, ResisLGT, TEXTMAX };
    public Player SC_Player=null;


    public void setActive(bool val)
    {
        this.gameObject.SetActive(val);
    }
    public void BTNClickProc()
    {
        if (gameObject.activeSelf == false)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
            return;
        }
        this.gameObject.SetActive(true);
        Transform obj;
        Text text;
        obj = this.transform.Find($"NameText");
        text = obj.gameObject.GetComponent<Text>();
        text.text = SC_Player.name;
        for (CHARSTATTEXT i = CHARSTATTEXT.Level; i < CHARSTATTEXT.TEXTMAX; i++)
        {
            obj = this.transform.Find($"{i.ToString()}Text");
            text = obj.gameObject.GetComponent<Text>();
            text.text = $"{SC_Player.GetStatElement((Status.STATS)i)}";
        }
    }

    private void OnEnable()
    {
        
    }









}
