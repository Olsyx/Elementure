using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{

    [SerializeField]
    public string RoomId;
    [SerializeField]
    public List<RoomController> AdjacentRooms;


    public void ActivateAdjacentRooms()
    {

        foreach (RoomController room in AdjacentRooms)
        {

            room.gameObject.SetActive(true);

        }

    }



}