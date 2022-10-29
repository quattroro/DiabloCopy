using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//여기서 항상 보여야 하는 퀵슬롯, hp,mp등을 관리한다.
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
    public Dictionary<UITYPES, BaseUI> UIObjList = new Dictionary<UITYPES, BaseUI>();//UI들을 가지고 있으면서 어떤 버튼이 눌렸는지에 따라 각각의 UI들을 실행시켜 준다.
    public List<BaseUI> Test = new List<BaseUI>();
    public List<BaseUI> CurActiveUI = new List<BaseUI>();
    public Button[] UIButtons = new Button[(int)UITYPES.UITYPEMAX];



    //[Header("연결 필요")]
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
    //모든 UI들을 Ini을 할때 탐색해서 가지고 있는다.
    public void Init()
    {
        LoadAllBaseUI();


    }

    public void LoadAllBaseUI()
    {
        BaseUI[] UI = FindObjectsOfType<BaseUI>();
        Debug.Log($"ui{UI.Length}개 불러옴");
        foreach(BaseUI a in UI)
        {
            //UI 초기세팅 해주고
            a.Init();

            //UI 타입에 해당하는 활성화 버튼을 찾아서 활성화 함수를 연결해준다.
            Button activeBTN = GameObject.Find($"Button({a.GetUIType()})").GetComponent<Button>();
            activeBTN.onClick.AddListener(a.setActive);
            UIButtons[(int)a.GetUIType()] = activeBTN;

            //UI는 비활성화된 상태로 시작
            if (a.GetUIType()!=UITYPES.MAIN)
                a.setActive(false);

            //딕셔너리에 넣어서 관리
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

    //버튼이 눌리면 해당 함수로 UI를 끄고 킨다.
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




//uipanel을 관리한다.
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