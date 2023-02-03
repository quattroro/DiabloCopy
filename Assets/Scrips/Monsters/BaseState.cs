using System;
using System.Reflection;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//FSM ������ state���� �ֻ��� Ŭ����
public class BaseState
{
    //public bool isInTransition = false;

    public StateMachine stateMachine;

    //Enter
    public bool hasEnterRoutine = false;
    public Action EnterCall = null;
    public Func<IEnumerator> EnterRoutine = null;

    //Exit
    public bool hasExitRoutine = false;
    public Action ExitCall = null;
    public Func<IEnumerator> ExitRoutine = null;

    //Update
    public Action UpdateCall = null;

    public BaseState(StateMachine _stateMachine)
    {
        InitSetting(_stateMachine);
    }
    //������ �޼ҵ���� �����ؼ� �̸� �����ؼ� ������ �ִ´�.
    public virtual void InitSetting(StateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        MethodInfo[] methods = this.GetType().GetMethods();
        
        foreach (MethodInfo method in methods)
        {
            if( method.Name.Equals("Enter"))
            {
                if (method.ReturnType == typeof(IEnumerator))
                {
                    hasEnterRoutine = true;
                    EnterRoutine = Delegate.CreateDelegate(typeof(Func<IEnumerator>), this, method) as Func<IEnumerator>;
                    if (EnterRoutine == null)
                        Debug.Log("[Enter transition ���� ����]");
                    else
                        Debug.Log("[Enter transition ���� ����]");
                }
                else
                {
                    EnterCall = Delegate.CreateDelegate(typeof(Action), this, method) as Action;
                }
            }
            else if(method.Name.Equals("Exit"))
            {
                if (method.ReturnType == typeof(IEnumerator))
                {
                    hasExitRoutine = true;
                    ExitRoutine = Delegate.CreateDelegate(typeof(Func<IEnumerator>), this, method) as Func<IEnumerator>;
                    if (ExitRoutine == null)
                        Debug.Log("[Exit transition ���� ����]");
                    else
                        Debug.Log("[Enter transition ���� ����]");
                }
                else
                {
                    ExitCall = Delegate.CreateDelegate(typeof(Action), this, method) as Action;
                }
            }
            else if(method.Name.Equals("Update"))
            {
                UpdateCall = Delegate.CreateDelegate(typeof(Action), this, method) as Action;

            }
        }

    }

    


}
