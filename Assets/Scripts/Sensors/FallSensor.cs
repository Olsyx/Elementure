using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elementure.GameLogic.Agents;
using Elementure.GameLogic.Words;

public class FallSensor : AgentSensor
{
    [SerializeField]
    public int falldamage;

    public override void Activate() {
    }

    public override void OnTriggerEnter(Collider other) {
		Agent agent = other.GetComponent<Agent>();
		if (agent == null) {
			return;
		}

        ModifierTypes agentId = agent.Attributes.RaceModifier;

        switch (agentId) {
            case ModifierTypes.None:
				agent.Collider.enabled = false;
                StartCoroutine(Respawn(agent, agent.lookingDirection));
                Debug.Log("Eres el jugador");
                break;

            case ModifierTypes.Water:
                break;

            case ModifierTypes.Fire:
                break;

            case ModifierTypes.Air:
                break;

        }

    }

    IEnumerator Respawn(Agent agent, Vector3 safeLookingDirection) {
        yield return new WaitForSeconds(2.0f);
		agent.Collider.enabled = true;
        Debug.Log($"Respawned {agent.Id}");
		agent.transform.position = gameObject.transform.position - safeLookingDirection.normalized * 2;
    }

}
