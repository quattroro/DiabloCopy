using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MPBar : BaseUI
{
    public float _maxVal;
    public float _curVal;
    public Image _image;

    public override void Init()
    {
        base.Init();
        _type = UIManager.UITYPES.MP;
        _image = GetComponent<Image>();
    }


    public float MaxVal
    {
        get
        {
            return _maxVal;
        }
        set
        {
            _maxVal = value;



            CurVal = _maxVal;
        }
    }

    public float CurVal
    {
        get
        {
            return _curVal;
        }
        set
        {
            _curVal = value;

            if (_curVal > MaxVal)
                _curVal = MaxVal;
            if (_curVal < 0)
                _curVal = 0;

            _image.fillAmount = _curVal / MaxVal;

        }
    }


}
