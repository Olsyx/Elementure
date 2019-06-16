using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elementure.GameLogic.Agents;
using Elementure.GameLogic.Words;

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
        //base.OnTriggerEnter(other);

        if (other.GetComponent<Agent>() == null)
            return;

        ModifierTypes AgentId = other.GetComponent<Agent>().Attributes.RaceModifier;

        switch (AgentId)
        {
            case ModifierTypes.None:
                lookingDirection = Agent.lookingDirection;

                Agent.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(Respawn());
                Debug.Log("Eres el jugador");
                break;

            case ModifierTypes.Water:

                lookingDirection = Agent.lookingDirection;

                break;


            case ModifierTypes.Fire:
                lookingDirection = Agent.lookingDirection;


                break;

            case ModifierTypes.Air:

                lookingDirection = Agent.lookingDirection;


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
