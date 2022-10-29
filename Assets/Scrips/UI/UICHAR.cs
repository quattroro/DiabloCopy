using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICHAR : BaseUI,IPointerOverLay
{
    #region UICommon
    public bool _isoverlay;
    public bool IsOverlay
    {
        get
        {
            return _isoverlay;
        }
        set
        {
            _isoverlay = value;
        }

    }

    public bool GetIsNowOverLay()
    {
        throw new System.NotImplementedException();
    }

    public void SetIsNowOverLay(bool value)
    {
        throw new System.NotImplementedException();
    }
    #endregion


    public Text[] StatsTxT;

    public List<Button> StatButtons;

    public Player SC_Player=null;

    public void SetStatElement()
    {

    }

    public override void Init()
    {
        base.Init();
        _type = UIManager.UITYPES.CHAR;
        StatsTxT = new Text[(int)Status.STATS.STATMAX];
        Transform obj;

        obj = this.transform.Find($"NameText");
        StatsTxT[0] = obj.gameObject.GetComponent<Text>();
        StatsTxT[0].text = SC_Player.name;

        for (Status.STATS i = Status.STATS.Level; i < Status.STATS.STATMAX; i++)
        {
            obj = this.transform.Find($"{i}Text");
            StatsTxT[(int)i] = obj.gameObject.GetComponent<Text>();
            StatsTxT[(int)i].text = $"{SC_Player.GetStatElement(i)}";
        }


        StatButtons = new List<Button>();
        for (Status.CHANGEABLESTATS i = Status.CHANGEABLESTATS.STR;i< Status.CHANGEABLESTATS.CHANGEMAX;i++)
        {
            obj = this.transform.Find($"{i}Button");
            Button btn = obj.gameObject.GetComponent<Button>();
            Status.CHANGEABLESTATS temp = i;
            btn.onClick.AddListener(() => SC_Player.StatUp(temp));
            StatButtons.Add(btn);
        }
    }

    public override void setActive(bool val)
    {
        gameObject.SetActive(val);

        if (!val)
            return;

        ShowUIInfo();
    }

    public override void ShowUIInfo()
    {
        StatsTxT[0].text = SC_Player.name;
        for (Status.STATS i = Status.STATS.Level; i < Status.STATS.STATMAX; i++)
        {
            StatsTxT[(int)i].text = $"{SC_Player.GetStatElement(i)}";
        }

        if (SC_Player.GetStatElement(Status.STATS.StatPoint) <= 0)
        {
            for (int i = 0; i < StatButtons.Count; i++)
            {
                StatButtons[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < StatButtons.Count; i++)
            {
                StatButtons[i].gameObject.SetActive(true);
            }
        }
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
        //Transform obj;
        //Text text;
        //obj = this.transform.Find($"NameText");
        //text = obj.gameObject.GetComponent<Text>();
        //text.text = SC_Player.name;
        //for (Status.STATS i = Status.STATS.Level; i < Status.STATS.STATMAX; i++)
        //{
        //    obj = this.transform.Find($"{i.ToString()}Text");
        //    text = obj.gameObject.GetComponent<Text>();
        //    text.text = $"{SC_Player.GetStatElement((Status.STATS)i)}";
        //}
    }

}
