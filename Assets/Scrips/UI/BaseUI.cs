using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public interface BaseUI
{
    ///ui 기본 인터페이스
    //ui가 실행할 동작
    public void BTNClickProc();
    public void setActive(bool val);

}
