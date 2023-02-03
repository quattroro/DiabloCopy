using System;
using System.Reflection;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//FSM 각각의 state들의 최상위 클래스
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
    //각각의 메소드들을 구분해서 미리 매핑해서 가지고 있는다.
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
                        Debug.Log("[Enter transition 매핑 실패]");
                    else
                        Debug.Log("[Enter transition 매핑 성공]");
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
                        Debug.Log("[Exit transition 매핑 실패]");
                    else
                        Debug.Log("[Enter transition 매핑 성공]");
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
