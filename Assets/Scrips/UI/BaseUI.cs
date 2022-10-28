using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BaseUI:MonoBehaviour
{
    public RectTransform rectTransform;
    public UIManager.UITYPES _type;

    public virtual void Init(UIManager.UITYPES type)
    {
        rectTransform = GetComponent<RectTransform>();
        _type = type;
    }


    public virtual void setActive(bool val)
    {
        gameObject.SetActive(val);
    }

    public virtual bool IsActive()
    {
        return gameObject.activeSelf;
    }
    

}
