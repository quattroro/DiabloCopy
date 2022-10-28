using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//여기서 항상 보여야 하는 퀵슬롯, hp,mp등을 관리한다.
public class UIManager : Singleton<UIManager>
{
    public enum UITYPES
    {
        CHAR,
        INV,
        SPELLS,
        SPELL,
        UIBUTTONMAX
    };

    //List<BaseUI> buttons = new List<BaseUI>();
    public Dictionary<UITYPES, BaseUI> UITypes = new Dictionary<UITYPES, BaseUI>();//UI들을 가지고 있으면서 어떤 버튼이 눌렸는지에 따라 각각의 UI들을 실행시켜 준다.
    public List<BaseUI> testlist = new List<BaseUI>();

    //[Header("연결 필요")]
    //public Player sc_player;
    //public Image HPbar;
    //public Image MPbar;
    //public int[] QuickSlotInfo;
    //public List<Item> Itemlist;
    //public Button[] QuickSlotButton;

    //버튼이 눌리면 해당 함수로 
    public void UIButtonClick(string btn)
    {
        //for(UIBUTTONS i=UIBUTTONS.CHAR;i!=UIBUTTONS.UIBUTTONMAX;i++)
        //{
        //    if (btn == i.ToString()) 
        //    {
        //        buttons[i].BTNClickProc();
                
        //    }
        //}
    }

    public Transform objpanels;

    public void ShowUI()
    {

    }

    public void HideUI()
    {

    }

    public void RegistUI(BaseUI panel)
    {
        //panel.transform.parent = objpanels;
    }

    public void DeleteUI(BaseUI panel)
    {
        //panel.IsDestroy = true;
        //GameObject.Destroy(panel.gameObject);
    }

}
