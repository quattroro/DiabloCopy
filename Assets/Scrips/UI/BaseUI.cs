using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BaseUI:MonoBehaviour
{
    public RectTransform _rectTransform;
    public UIManager.UITYPES _type;
    //public Image _image;

    public virtual void Init()
    {
        _rectTransform = GetComponent<RectTransform>();
        //_image = GetComponent<Image>();
    }

    public UIManager.UITYPES GetUIType()
    {
        return _type;
    }

    public void SetUIType(UIManager.UITYPES _type)
    {
        this._type = _type;
    }

    public virtual void setActive()
    {
        if(gameObject.activeSelf)
        {
            setActive(false);
        }
        else
        {
            setActive(true);
        }
    }

    public virtual void setActive(bool val)
    {
        gameObject.SetActive(val);
    }

    public virtual bool IsActive()
    {
        return gameObject.activeSelf;
    }
    public virtual void ShowUIInfo()
    {

    }



}
