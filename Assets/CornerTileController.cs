using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CornerStates
{
    TopL, TopR, BottomL, BottomR
}

public class CornerTileController : MonoBehaviour
{
    [SerializeField]
    public CornerStates CurrentState;
    [SerializeField]
    public GameObject Model;

    [SerializeField]
    public GameObject TopLSprite;

    [SerializeField]
    public GameObject TopRSprite;

    [SerializeField]
    public GameObject BottomLSprite;

    [SerializeField]
    public GameObject BottomRSprite;

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

    public void ChangeState(CornerStates newState)
    {
        switch (newState)
        {
            case CornerStates.TopL:

                targetAngle = new Vector3(0, -90, 0);

                break;

            case CornerStates.TopR:

                targetAngle = new Vector3(-90, 0, 0);

                break;

            case CornerStates.BottomL:

                targetAngle = new Vector3(0, 180, -90);

                break;

            case CornerStates.BottomR:

                targetAngle = new Vector3(90, -90, 0);

                break;
        }



    }
}
