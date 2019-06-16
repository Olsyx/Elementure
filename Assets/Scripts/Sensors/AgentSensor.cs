using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elementure.GameLogic.Agents;

public abstract class AgentSensor : MonoBehaviour
{
    [SerializeField]
    public RoomController roomID;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Agent>() == null)
            return;

        Activate();
    }

    public virtual void Activate() { }


}
