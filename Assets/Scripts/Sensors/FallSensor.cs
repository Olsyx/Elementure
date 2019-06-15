using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elementure.GameLogic.Agents;

public class FallSensor : AgentSensor
{
    [SerializeField]
    public int damage;

    public Vector3 lastSafePosition;

    public GameObject Agent;

    public override void Activate()
    {

    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        string AgentId = other.GetComponent<Agent>().Id;
        Agent = other.gameObject;

        lastSafePosition = Agent.transform.position;

        Debug.Log(lastSafePosition);

        switch (AgentId)
        {
            case "Player":

                Agent.GetComponent<BoxCollider>().enabled=false;
                StartCoroutine(Respawn());
                Debug.Log("Eres el jugador");
                break;

            default:



                break;
        }

    }

    IEnumerator Respawn()
    {

        yield return new WaitForSeconds(2.0f);

        Agent.GetComponent<BoxCollider>().enabled = true;
        Debug.Log("Respawn");
        Agent.transform.position = new Vector3(lastSafePosition.x, lastSafePosition.y+1, lastSafePosition.z);
    }

}
