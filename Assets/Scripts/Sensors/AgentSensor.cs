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

        Debug.Log(other.gameObject.GetComponent<Agent>().Id);
        Activate();
    }

    public virtual void Activate() { }


}
