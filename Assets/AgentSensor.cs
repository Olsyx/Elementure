using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AgentSensor : MonoBehaviour
{
    [SerializeField]
    public RoomController roomID;

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.gameObject.GetType());
        Activate();
    }

    public virtual void Activate() { }


}
