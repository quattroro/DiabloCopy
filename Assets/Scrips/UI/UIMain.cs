using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMain : BaseUI, IPointerOverLay
{
    public override void Init()
    {
        base.Init();
        _type = UIManager.UITYPES.MAIN;
    }
    public bool _isoverlay;
    public bool IsOverlay
    {
        get
        {
            return _isoverlay;
        }
        set
        {
            _isoverlay = value;
        }

    }
    public bool GetIsNowOverLay()
    {
        throw new System.NotImplementedException();
    }

    public void SetIsNowOverLay(bool value)
    {
        throw new System.NotImplementedException();
    }
}
