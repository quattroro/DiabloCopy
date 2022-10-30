using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//uipanel은 ui 상에 존재한다.
//uipanel은 생성을해서 월드상의 객체와 LinkObjectPanel 함수를 이용해서 링킹을 해주면 ui상의 panel이 월드상의 해당 객체를 따라다닌다.
//그러다 uipanel 이 클릭되면 같이 등록해준 함수를 실행 시켜 준다.
public class ObjectPanel : MonoBehaviour,IPointerOverLay
{
    public GameObject LinkedObj;
    public delegate void ClickedEvent();
    protected ClickedEvent clickedevent;

    public Vector3 intervalpos;

    public Text text;

    public Image Backimage;

    public bool _isoverlay;

    public bool IsDestroy = false;

    public Canvas canvas;

    //현재 마우스가 오버레이 되어있는지
    public bool IsOverlay
    {
        get
        {
            return _isoverlay;
        }
        set
        {
            if(!IsDestroy)
            {
                _isoverlay = value;
                Color temp = Backimage.color;//마우스가 객체 위에 위치하면 외곽선이 바뀌도록 해준다.
                if (IsOverlay)
                {
                    temp.a = 1;
                }
                else
                {
                    temp.a = 0;
                }
                Backimage.color = temp;

            }
            
            
        }

    }

    private void Start()
    {
        IsOverlay = false;
        canvas = FindObjectOfType<Canvas>(); 
        transform.parent = canvas.transform;
    }

    //월드상의 객체와 링킹을 해준다.
    public void LinkObjectPanel(GameObject obj,string showtext/*표시될 이름*/, ClickedEvent cevent/*클릭될 시 실행시킬 함수*/,Vector2 pos)
    {
        //if (transform.parent != canvas.transform)
            
        LinkedObj = obj;
        clickedevent += cevent;
        intervalpos = pos;

        text.text = showtext;
    }

    //연결된 오브젝트가 있으면 해당 오브젝트를 일정한 위치에서 따라다니도록
    public void SetPos()
    {
        Vector3 temp = Camera.main.WorldToScreenPoint(LinkedObj.transform.position);
        temp += intervalpos;
        this.transform.position = temp;
    }

    //해당 패널이 클릭되었을때 실행된다.
    public void ObjectPanelClick()
    {
        clickedevent();
    }

    //오브젝트 객체와 연결이 되면 해당 객체의 위치에 해당하는 ui에서의 위치에 따라 움직이도록 해준다. 
    void Update()
    {
        if(LinkedObj!=null)
            SetPos();
        //LinkedObj.

    }

    public bool GetIsNowOverLay()
    {
        return IsOverlay;
    }

    public void SetIsNowOverLay(bool value)
    {
        IsOverlay = value;
    }

}
