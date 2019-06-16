using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elementure.GameLogic.Agents;

public class FallSensor : AgentSensor
{
    [SerializeField]
    public int falldamage;

    public Vector3 lookingDirection;

    public Agent Agent;

    public override void Activate()
    {

    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        Agent = other.GetComponent<Agent>();
        string AgentId = Agent.Id;

        lookingDirection = Agent.lookingDirection;
        switch (AgentId)
        {
            case "Player":

                Agent.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(Respawn());
                Debug.Log("Eres el jugador");
                break;

            case "Gorgon":


                break;


            case "Dragon":


                break;
            case "Djin":

                break;

            case "Slime":

                break;
        }

    }

    IEnumerator Respawn()
    {

        yield return new WaitForSeconds(2.0f);

        Agent.GetComponent<BoxCollider>().enabled = true;
        Debug.Log("Respawn");
        Agent.transform.position = gameObject.transform.position - lookingDirection.normalized * 2;
    }

}
