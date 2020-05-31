using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachin : MonoBehaviour
{

    protected StateBoss State;

    public void SetState(StateBoss state) {

        State = state;

        StartCoroutine(State.State_1());

    }

}
