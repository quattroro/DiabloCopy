using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//���⼭ �׻� ������ �ϴ� ������, hp,mp���� �����Ѵ�.
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
    public Dictionary<UITYPES, BaseUI> UITypes = new Dictionary<UITYPES, BaseUI>();//UI���� ������ �����鼭 � ��ư�� ���ȴ����� ���� ������ UI���� ������� �ش�.
    public List<BaseUI> testlist = new List<BaseUI>();

    //[Header("���� �ʿ�")]
    //public Player sc_player;
    //public Image HPbar;
    //public Image MPbar;
    //public int[] QuickSlotInfo;
    //public List<Item> Itemlist;
    //public Button[] QuickSlotButton;

    //��ư�� ������ �ش� �Լ��� 
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
