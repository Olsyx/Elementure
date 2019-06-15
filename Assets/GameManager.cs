using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{

    public static void LoadActiveRooms(RoomController NextRoom)
    {

        foreach (RoomController room in NextRoom.AdjacentRooms)
        {

            room.gameObject.SetActive(true);

        }



    }

    public static void UnloadRooms(RoomController CurrentRoom)
    {

        foreach(RoomController room in CurrentRoom.AdjacentRooms)
        {

            room.gameObject.SetActive(false);

        }


    }


}
