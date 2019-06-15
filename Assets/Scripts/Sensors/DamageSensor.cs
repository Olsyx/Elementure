using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSensor : AgentSensor
{

    [SerializeField]
    public int damage;


    public override void Activate()
    {
        
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        string AgentId = other.GetComponent<Agent>().Id;
        GameObject Agent = other.gameObject;

        switch (AgentId)
        {
            case "Player":

                Agent.GetComponent<Agent>().Damage(damage);
               // other.
                Debug.Log("Eres el jugador");
                break;

            default:



                break;
        }

    }


}
