using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//각각의 FSM 들의 최상위
public class StateMachine : MonoBehaviour
{
    bool isInTransition = false;

    public string curStateName;

    public BaseState curState;
    public BaseState nextState;
    public BaseState queuedState;
    public BaseState destState;
    public BaseState lastState;

    IEnumerator transitionRoutine = null;

    public IEnumerator enterRoutine;
    public IEnumerator exitRoutine;
    private IEnumerator currentTransition;
    private IEnumerator queuedChange;

    public virtual void Update()
    {
        if(curState!=null)
        {
            if(curState.UpdateCall!=null)
                curState.UpdateCall();
        }
    }

    public string GetCurstate
    {
        get
        {
            return curStateName;
        }
    }

    public void ChangeState(BaseState newState)
    {
        if(curState==null)
        {
            curState = newState;
            if (curState.EnterCall != null)
                curState.EnterCall();
            return;
        }

        if (curState == newState)
            return;

        if(curState.hasExitRoutine||newState.hasEnterRoutine)
        {
            isInTransition = true;
            transitionRoutine = ChangeStateRoutine(newState);
            StartCoroutine(transitionRoutine);
        }
        else
        {
            destState = newState;

            if (curState != null)
            {
                if (curState.ExitCall != null)
                    curState.ExitCall();
                //라스트 이벤트
            }

            lastState = curState;
            curState = destState;
            curStateName = curState.ToString();


            if (curState != null)
            {
                if (curState.EnterCall != null)
                    curState.EnterCall();
                //체인지 이벤트
            }

            isInTransition = false;
        }

        //MethodInfo[] methods = newState.GetType().GetMethods();
        //MethodInfo methodinfo = newState.GetType().GetMethod("Exit");

        //curState의 Exit 또는 newState의 Enter가 코루틴 함수일때
        //if (methodinfo.ReturnType == typeof(IEnumerator))
        //{
        //    newState.isInTransition = true;
        //    nextState = newState;
        //    //transitionRoutine = Delegate.CreateDelegate(typeof(Func<IEnumerator>), methodinfo)as Func<IEnumerator>;
        //    //if (transitionRoutine == null)
        //    //    Debug.Log("[transition 매핑 실패]");

            

        //    StartCoroutine(transitionRoutine());

        //    //WaitForPreviousState(curState);
        //}
        //else
        //{

        //}
    }


    public IEnumerator ChangeStateRoutine(BaseState newState)
    {
        destState = newState;

        //exit가 코루틴일때
        if (curState.hasExitRoutine)
        {
            exitRoutine = curState.ExitRoutine();
            yield return StartCoroutine(exitRoutine);
            exitRoutine = null;
        }
        else
        {
            if (curState.ExitCall != null)
                curState.ExitCall();
        }


        lastState = curState;
        curState = destState;
        curStateName = curState.ToString();


        if (curState.hasEnterRoutine)
        {
            enterRoutine = curState.EnterRoutine();
            yield return StartCoroutine(enterRoutine);
            enterRoutine = null;
        }
        else
        {
            if (curState.EnterCall != null)
                curState.EnterCall();
        }

        isInTransition = false;
    }


    IEnumerator WaitForPreviousState(BaseState waitState)
    {
        //queuedState = nextState; //Cache this so fsm.NextState is accurate;

        while (isInTransition)
        {
            yield return null;
        }

        //queuedState = null;

        ChangeState(nextState);
    }

}
