using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//마우스가 객체 위에 올라왔는지 아닌지 확인하기 위한 인터페이스
public interface IPointerOverLay
{
    public bool GetIsNowOverLay();
    public void SetIsNowOverLay(bool value);

}
