using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//uipanel�� ui �� �����Ѵ�.
//uipanel�� �������ؼ� ������� ��ü�� LinkObjectPanel �Լ��� �̿��ؼ� ��ŷ�� ���ָ� ui���� panel�� ������� �ش� ��ü�� ����ٴѴ�.
//�׷��� uipanel �� Ŭ���Ǹ� ���� ������� �Լ��� ���� ���� �ش�.
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

    //���� ���콺�� �������� �Ǿ��ִ���
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
                Color temp = Backimage.color;//���콺�� ��ü ���� ��ġ�ϸ� �ܰ����� �ٲ�� ���ش�.
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

    //������� ��ü�� ��ŷ�� ���ش�.
    public void LinkObjectPanel(GameObject obj,string showtext/*ǥ�õ� �̸�*/, ClickedEvent cevent/*Ŭ���� �� �����ų �Լ�*/,Vector2 pos)
    {
        //if (transform.parent != canvas.transform)
            
        LinkedObj = obj;
        clickedevent += cevent;
        intervalpos = pos;

        text.text = showtext;
    }

    //����� ������Ʈ�� ������ �ش� ������Ʈ�� ������ ��ġ���� ����ٴϵ���
    public void SetPos()
    {
        Vector3 temp = Camera.main.WorldToScreenPoint(LinkedObj.transform.position);
        temp += intervalpos;
        this.transform.position = temp;
    }

    //�ش� �г��� Ŭ���Ǿ����� ����ȴ�.
    public void ObjectPanelClick()
    {
        clickedevent();
    }

    //������Ʈ ��ü�� ������ �Ǹ� �ش� ��ü�� ��ġ�� �ش��ϴ� ui������ ��ġ�� ���� �����̵��� ���ش�. 
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
