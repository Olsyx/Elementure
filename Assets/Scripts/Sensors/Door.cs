using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorStates
{
    Top, Bottom, Left, Right
}

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

    [SerializeField]
    public DoorStates CurrentState;
    [SerializeField]
    public GameObject Model;

    [SerializeField]
    public GameObject TopSprite;

    [SerializeField]
    public GameObject BottomSprite;

    [SerializeField]
    public GameObject LeftSprite;

    [SerializeField]
    public GameObject Right;

    private Vector3 currentAngle;
    private Vector3 targetAngle = new Vector3(0, 0, 0);

    private void Start()
    {
        ChangeState(CurrentState);
    }

    void Update()
    {
        currentAngle = new Vector3(
             Mathf.LerpAngle(currentAngle.x, targetAngle.x, (1 * Time.deltaTime)),
             Mathf.LerpAngle(currentAngle.y, targetAngle.y, (1 * Time.deltaTime)),
             Mathf.LerpAngle(currentAngle.z, targetAngle.z, (1 * Time.deltaTime)));

        Model.transform.eulerAngles = currentAngle;
    }

    public void ChangeState(DoorStates newState)
    {
        switch (newState)
        {
            case DoorStates.Bottom:

                targetAngle = new Vector3(0, 0, 0);

                break;

            case DoorStates.Left:

                targetAngle = new Vector3(-90, -180, 0);

                break;

            case DoorStates.Right:

                targetAngle = new Vector3(0, 0, -90);

                break;

            case DoorStates.Top:

                targetAngle = new Vector3(90, 0, -90);

                break;
        }



    }


}
