using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//여기서 항상 보여야 하는 퀵슬롯, hp,mp등을 관리한다.
public class UIManager : Singleton<UIManager>
{
    public enum UIBUTTONS
    {
        CHAR,
        INV,
        SPELLS,
        SPELL,
        UIBUTTONMAX
    };

    //List<BaseUI> buttons = new List<BaseUI>();
    public Dictionary<UIBUTTONS, BaseUI> buttons = new Dictionary<UIBUTTONS, BaseUI>();//UI들을 가지고 있으면서 어떤 버튼이 눌렸는지에 따라 각각의 행동을 해준다. 커맨드 패턴
    public List<BaseUI> testlist = new List<BaseUI>();

    [Header("연결 필요")]
    public Player sc_player;
    public Image HPbar;
    public Image MPbar;
    public int[] QuickSlotInfo;
    public List<Item> Itemlist;
    public Button[] QuickSlotButton;

    //버튼이 눌리면 해당 함수로 
    public void ButtonClick(string btn)
    {
        for(UIBUTTONS i=UIBUTTONS.CHAR;i!=UIBUTTONS.UIBUTTONMAX;i++)
        {
            if (btn == i.ToString()) 
            {
                buttons[i].BTNClickProc();
                
            }
        }
    }

    public Transform objpanels;
    public void RegistUIPanel(ObjectPanel panel)
    {
        panel.transform.parent = objpanels;
    }

    public void DeleteUIPanel(ObjectPanel panel)
    {
        panel.IsDestroy = true;
        GameObject.Destroy(panel.gameObject);
    }

    private void Awake()
    {
        //BaseUI obj = FindObjectOfType<UICHAR>();
        //buttons.Add(UIBUTTONS.CHAR, obj);
       // obj.setActive(false);


        //obj = FindObjectOfType<UIINV>();
        //buttons.Add(UIBUTTONS.INV, obj);
        //obj.setActive(false);

        //testlist.Add(FindObjectOfType<UICHAR>());
        //testlist.Add(FindObjectOfType<UIINV>());
    }

    void ShowQuickSlot()
    {

    }

    void Start()
    {
        //sc_player = GameManager.GetI.CS_Palyer;
        //QuickSlotInfo = sc_player.itemBag.QuickSlot;
        //Itemlist = sc_player.itemBag.Itemlist;
    }

    void Update()
    {
        //HPbar.fillAmount = (float)sc_player.CurHP / (float)sc_player.MaxHP;
        //MPbar.fillAmount = (float)sc_player.CurMP / (float)sc_player.MaxMP;
        
    }
}
