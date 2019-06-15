using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetType().Equals(Player))
        {

            Debug.Log("SoyPlayer");

        }



    }
}
