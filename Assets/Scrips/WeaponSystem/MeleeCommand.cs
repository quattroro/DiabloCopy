using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ŀ�ǵ忡�� ������ �Լ��� �븮��
public delegate void FunctionPointer();

//Ŀ�ǵ�� �ϳ��� Ŀ�ǵ忡 �ϳ��� �ൿ�� �ϴµ� �ϳ��� �������� Ŀ�ǵ�� �ٸ� �ൿ�鵵 ���� �� �� �ֵ��� ���ʸ�ȭ ��
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
