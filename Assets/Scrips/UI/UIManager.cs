using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//���⼭ �׻� ������ �ϴ� ������, hp,mp���� �����Ѵ�.
public class UIManager : Singleton<UIManager>
{
    public enum UITYPES
    {
        MAIN,
        CHAR,
        INV,
        SPELLS,
        SPELL,
        UITYPEMAX
    };

    //List<BaseUI> buttons = new List<BaseUI>();
    public Dictionary<UITYPES, BaseUI> UIObjList = new Dictionary<UITYPES, BaseUI>();//UI���� ������ �����鼭 � ��ư�� ���ȴ����� ���� ������ UI���� ������� �ش�.
    public List<BaseUI> Test = new List<BaseUI>();
    public List<BaseUI> CurActiveUI = new List<BaseUI>();
    public Button[] UIButtons = new Button[(int)UITYPES.UITYPEMAX];



    //[Header("���� �ʿ�")]
    //public Player sc_player;
    //public Image HPbar;
    //public Image MPbar;
    //public int[] QuickSlotInfo;
    //public List<Item> Itemlist;
    //public Button[] QuickSlotButton;

    private void Start()
    {
        Init();
    }
    //��� UI���� Ini�� �Ҷ� Ž���ؼ� ������ �ִ´�.
    public void Init()
    {
        LoadAllBaseUI();


    }

    public void LoadAllBaseUI()
    {
        BaseUI[] UI = FindObjectsOfType<BaseUI>();
        Debug.Log($"ui{UI.Length}�� �ҷ���");
        foreach(BaseUI a in UI)
        {
            //UI �ʱ⼼�� ���ְ�
            a.Init();

            //UI Ÿ�Կ� �ش��ϴ� Ȱ��ȭ ��ư�� ã�Ƽ� Ȱ��ȭ �Լ��� �������ش�.
            Button activeBTN = GameObject.Find($"Button({a.GetUIType()})").GetComponent<Button>();
            activeBTN.onClick.AddListener(a.setActive);
            UIButtons[(int)a.GetUIType()] = activeBTN;

            //UI�� ��Ȱ��ȭ�� ���·� ����
            if (a.GetUIType()!=UITYPES.MAIN)
                a.setActive(false);

            //��ųʸ��� �־ ����
            UIObjList.Add(a.GetUIType(), a);
            Test.Add(a);

        }
        
    }

    public List<BaseUI> GetCurActiveUI()
    {
        return CurActiveUI;
    }

    public BaseUI GetUIInstance(UITYPES type)
    {
        return UIObjList[type];
    }

    public void LoadBaseUI(UITYPES type)
    {

    }

    //��ư�� ������ �ش� �Լ��� UI�� ���� Ų��.
    public void UIButtonClick(string btn)
    {
        
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




//uipanel�� �����Ѵ�.
//public class UIManager : Singleton<UIManager>
//{
//    public Transform objpanels;
//    public void RegistUIPanel(ObjectPanel panel)
//    {
//        panel.transform.parent = objpanels;
//    }

//    public void DeleteUIPanel(ObjectPanel panel)
//    {
//        panel.IsDestroy = true;
//        GameObject.Destroy(panel.gameObject);
//    }

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}