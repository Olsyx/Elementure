using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientTileController : MonoBehaviour
{
    [SerializeField]
    public TileStates CurrentState;
    [SerializeField]
    public GameObject Model;

    [SerializeField]
    public GameObject FireSprite;

    [SerializeField]
    public GameObject WaterSprite;

    [SerializeField]
    public GameObject NormalSprite;

    [SerializeField]
    public GameObject AirSprite;

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

    public void ChangeState(TileStates newState)
    {
        switch (newState)
        {
            case TileStates.Normal:

                targetAngle = new Vector3(0, 0, 0);

                break;

            case TileStates.Fire:

                targetAngle = new Vector3(-90, -90, 0);

                break;

            case TileStates.Water:

                targetAngle = new Vector3(0, -90,-90);

                break;

            case TileStates.Wind:

                targetAngle = new Vector3(90, -90, 0);

                break;
        }



    }
}

