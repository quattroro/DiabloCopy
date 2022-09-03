using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//커맨드에서 실행할 함수의 대리자
public delegate void FunctionPointer();

//커맨드는 하나의 커맨드에 하나의 행동만 하는데 하나의 근접무기 커맨드로 다른 행동들도 같이 할 수 있도록 제너릭화 함
public class MeleeCommand<Receiver> : IWeaponCommand
{
    public void Execute()
    {
        //m_reciever.Action();
        Action();

    }

    public MeleeCommand()
    {

    }
    public MeleeCommand(Receiver m, FunctionPointer p)
    {
        m_reciever = m;
        Action = p;
    }

    private Receiver m_reciever;
    private FunctionPointer Action;

    //void Start()
    //{

    //}



    //void Update()
    //{

    //}
}
