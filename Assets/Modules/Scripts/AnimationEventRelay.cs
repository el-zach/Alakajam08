using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventRelay : MonoBehaviour
{
    public UnityEvent[] OnAnimationEvent;

    public void CallEvent0()
    {
        OnAnimationEvent[0].Invoke();
    }
    public void CallEvent1()
    {
        OnAnimationEvent[1].Invoke();
    }
    public void CallEvent2()
    {
        OnAnimationEvent[2].Invoke();
    }
    public void CallEvent3()
    {
        OnAnimationEvent[3].Invoke();
    }

}
