using Elementure.GameLogic.Words;
using Elementure.GameLogic.Agents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWaterSensor : AgentSensor
{
    [SerializeField]
    public int damage;


    public override void Activate()
    {

    }

    public override void OnTriggerEnter(Collider other)
    {
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

                Agent.GetComponent<Agent>().Damage(damage);
                Debug.Log("Eres Fuego y te dejo");
                break;


            case ModifierTypes.Water:

                Debug.Log("Eres Agua y daño");
                break;

            case ModifierTypes.None:

                Agent.GetComponent<Agent>().Damage(damage);
                Debug.Log("Eres Nada y daño");
                break;
        }

    }


}


