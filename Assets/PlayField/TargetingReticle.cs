using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingReticle : MonoBehaviour
{
    public void MoveToReactor(Reactor moveTo)
    {
        transform.position = moveTo.transform.position;
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
