using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//���⼭ �׻� ������ �ϴ� ������, hp,mp���� �����Ѵ�.
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
    public Dictionary<UIBUTTONS, BaseUI> buttons = new Dictionary<UIBUTTONS, BaseUI>();//UI���� ������ �����鼭 � ��ư�� ���ȴ����� ���� ������ �ൿ�� ���ش�. Ŀ�ǵ� ����
    public List<BaseUI> testlist = new List<BaseUI>();

    [Header("���� �ʿ�")]
    public Player sc_player;
    public Image HPbar;
    public Image MPbar;
    public int[] QuickSlotInfo;
    public List<Item> Itemlist;
    public Button[] QuickSlotButton;

    //��ư�� ������ �ش� �Լ��� 
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
