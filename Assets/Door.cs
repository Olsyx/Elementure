using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : AgentSensor
{

    public override void Activate()
    {

        GameManager.istannce.LoadActiveRooms(roomID);


    }


}
