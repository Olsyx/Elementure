using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : AgentSensor
{
    [SerializeField]
    public RoomController CurrentRoom;
    [SerializeField]
    public RoomController NextRoom;

    public override void Activate()
    {

        GameManager.UnloadRooms(CurrentRoom);
        NextRoom.gameObject.SetActive(true);
        GameManager.LoadActiveRooms(NextRoom);

    }


}
