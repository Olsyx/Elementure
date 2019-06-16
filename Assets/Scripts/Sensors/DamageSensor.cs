using Elementure.GameLogic.Agents;
using Elementure.GameLogic.Words;
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
        if (other.GetComponent<Agent>() == null)
            return;
        base.OnTriggerEnter(other);


        ModifierTypes AgentId = other.GetComponent<Agent>().Attributes.RaceModifier;
        GameObject Agent = other.gameObject;

        switch (AgentId)
        {
            case ModifierTypes.Air:

                Agent.GetComponent<Agent>().Damage(damage);
                Debug.Log("Eres Aire y te estoy haciendo daño");
                break;

            case ModifierTypes.Fire:

                Debug.Log("Eres Fuego y te dejo");
                break;

            case ModifierTypes.Water:

                Agent.GetComponent<Agent>().Damage(damage);
                Debug.Log("Eres Agua y daño");
                break;

            case ModifierTypes.None:

                Agent.GetComponent<Agent>().Damage(damage);
                Debug.Log("Eres Nada y daño");
                break;
        }

    }


}
