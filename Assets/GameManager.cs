using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    List<RoomController> rooms;

    public GameManager instance;


    private void Start()
    {

        instance = new GameManager();

    }

    public GameManager()
    {

       
    }

    public void LoadActiveRooms(RoomController room)
    {

        



    }


}
