using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//애니메이션 이벤트들을 관리
//애니메이션 이벤트에 해당 함수들 OnBeginEventString, OnMidEventString, OnEndEventString 을 등록해주고
//각 이벤트들의 대리자에 실행하고자 하는 함수를 AddEvent(beginCallback begin, midCallback mid, endCallback end) 함수를 이용해 연결시켜주면 이벤트가 실행되면 해당 함수가 실행됨

public class AnimationEventSystem : MonoBehaviour
{
	AnimationController animator;

	public Dictionary<string, AnimationClip> clips;
	//public AnimationEvent[][] eventlist;
	public List<AnimationEvent[]> eventlist = new List<AnimationEvent[]>();

	public delegate void beginCallback(string s_val);
	public delegate void midCallback(string s_val);
	public delegate void endCallback(string s_val);

    public Dictionary<string, beginCallback> BeginEventInvokers = new Dictionary<string, beginCallback>();
	public Dictionary<string, midCallback> MidEventInvokers = new Dictionary<string, midCallback>();
	public Dictionary<string, endCallback> EndEventInvokers = new Dictionary<string, endCallback>();

	
	public beginCallback _beginCallback;
	public midCallback _midCallback;
	public endCallback _endCallback;

    public delegate void beginCallbackT<T>(T s_val);
    public delegate void midCallbackT<T>(T s_val);
    public delegate void endCallbackT<T>(T s_val);


    private void Start()
    {
		//animator = GetComponent<AnimationController>();
		//clips = animator.GetAnimationClips();
	}

    //애니메이션이벤트에 함수를 등록 하려면 해당 이벤트를 가지고 있는 애니메이션클립의 이름을 같이 넣어 준다.
    //  public void AddEvent(beginCallback begin, midCallback mid, endCallback end)
    //  {
    //if(begin != null)
    //	_beginCallback += begin;
    //if (mid != null)
    //	_midCallback += mid;
    //if (end != null)
    //	_endCallback += end;
    //  }

    public void AddEvent(KeyValuePair<string, beginCallback> begin, KeyValuePair<string, midCallback> mid, KeyValuePair<string, endCallback> end)
    {
        //AnimationEvent[] events = animator.m_clips[begin.Key].events;
        //events[0].str

        if (begin.Key != null)
            BeginEventInvokers.Add(begin.Key, begin.Value);
        if (mid.Key != null)
            MidEventInvokers.Add(mid.Key, mid.Value);
        if (end.Key != null)
            EndEventInvokers.Add(end.Key, end.Value);
    }

    //각각의 애니메이션에 실행시킬 이벤트들을 넣어준다.
    public void AddEvent(KeyValuePair<string, beginCallback> begin, float begintime, KeyValuePair<string, midCallback> mid, float midtime, KeyValuePair<string, endCallback> end, float endtime)
    {
        AnimationEvent aevent;
        float time;

        if (animator == null)
            animator = GetComponent<AnimationController>();

        if (begin.Key != null)
        {
            //time = begintime / animator.m_clips[begin.Key].length;

            aevent = new AnimationEvent();
            aevent.time = begintime;
            aevent.functionName = "OnBeginEvent";
            aevent.stringParameter = begin.Key;
            if (!animator.m_clips.ContainsKey(begin.Key))
            {
                Debug.Log($"AnimationEventSystem 오류 키값이 존재하기 않음 {begin.Key}");
            }
            animator.m_clips[begin.Key].AddEvent(aevent);

            BeginEventInvokers.Add(begin.Key, begin.Value);
        }

        if (mid.Key != null)
        {
            //time = midtime / animator.m_clips[begin.Key].length;

            aevent = new AnimationEvent();
            aevent.time = midtime;
            aevent.functionName = "OnMidEvent";
            aevent.stringParameter = mid.Key;

            if (!animator.m_clips.ContainsKey(mid.Key))
            {
                Debug.Log($"AnimationEventSystem 오류 키값이 존재하기 않음 {mid.Key}");
            }
            animator.m_clips[mid.Key].AddEvent(aevent);

            MidEventInvokers.Add(mid.Key, mid.Value);
        }

        if (end.Key != null)
        {
            //time = endtime / animator.m_clips[begin.Key].length;

            aevent = new AnimationEvent();
            aevent.time = endtime;
            aevent.functionName = "OnEndEvent";
            aevent.stringParameter = end.Key;
            if (!animator.m_clips.ContainsKey(end.Key))
            {
                Debug.Log($"AnimationEventSystem 오류 키값이 존재하기 않음 {end.Key}");
            }
            animator.m_clips[end.Key].AddEvent(aevent);

            EndEventInvokers.Add(end.Key, end.Value);
        }

    }


    //public void AddTempletEvent(KeyValuePair<string, beginCallbackT<Object>> begin, KeyValuePair<string, midCallback> mid, KeyValuePair<string, endCallback> end)
    //{
    //    if (begin.Key != null)
    //        BeginEventInvokers.Add(begin.Key, begin.Value);
    //    if (mid.Key != null)
    //        MidEventInvokers.Add(mid.Key, mid.Value);
    //    if (end.Key != null)
    //        EndEventInvokers.Add(end.Key, end.Value);
    //}

    //Animation Event
    public void OnBeginEventString(string s_val)
	{
		//_beginCallback?.Invoke(s_val);
        
            
		if(BeginEventInvokers.TryGetValue(s_val,out _beginCallback))
        {
			_beginCallback.Invoke(s_val);
            //_beginCallback.
        }
	}

	public void OnMidEventString(string s_val)
	{
		//_midCallback?.Invoke(s_val);
		if (MidEventInvokers.TryGetValue(s_val, out _midCallback))
		{
			_midCallback.Invoke(s_val);
		}
	}

	public void OnEndEventString(string s_val)
	{
		//_endCallback?.Invoke(s_val);
		if (EndEventInvokers.TryGetValue(s_val, out _endCallback))
		{
			_endCallback.Invoke(s_val);
		}
	}



    public void OnBeginEvent(string s_val)
    {
        //_beginCallback?.Invoke(s_val);


        if (BeginEventInvokers.TryGetValue(s_val, out _beginCallback))
        {
            _beginCallback.Invoke(s_val);
            //_beginCallback.
        }
    }

    public void OnMidEvent(string s_val)
    {
        //_midCallback?.Invoke(s_val);
        if (MidEventInvokers.TryGetValue(s_val, out _midCallback))
        {
            _midCallback.Invoke(s_val);
        }
    }

    public void OnEndEvent(string s_val)
    {
        //_endCallback?.Invoke(s_val);
        if (EndEventInvokers.TryGetValue(s_val, out _endCallback))
        {
            _endCallback.Invoke(s_val);
        }
    }
}

