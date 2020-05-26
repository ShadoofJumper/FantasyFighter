using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    public UnityAction onMainAttackEnd;
    public UnityAction onDie;
    public UnityAction onHit;

    public void AlertObservers(string message)
    {
        if (message.Equals("MainAttackEnd"))
        {
            if(onMainAttackEnd != null)
            {
                //after invoke clear event
                onMainAttackEnd.Invoke();
                onMainAttackEnd = null;
            }
        }
        else if (message.Equals("Die"))
        {
            if (onDie != null)
                onDie.Invoke();
        }
        else if (message.Equals("Hit"))
        {
            if (onHit != null)
                onHit.Invoke();
        }
    }
}
