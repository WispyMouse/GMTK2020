using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFieldController : MonoBehaviour
{
    public MouseInputController MouseInputControllerInstance;

    public Reactor GetHoveredReactor()
    {
        return MouseInputControllerInstance.GetHoveredReactor();
    }
}
