using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileStates
{
    Normal, Fire, Water, Wind
}

public class TileController : MonoBehaviour
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
    private Vector3 targetAngle = new Vector3(0,0,0);
    


    // Update is called once per frame
    void Update()
    {
        currentAngle = new Vector3(
             Mathf.LerpAngle(currentAngle.x, targetAngle.x, (1* Time.deltaTime)),
             Mathf.LerpAngle(currentAngle.y, targetAngle.y, (1 * Time.deltaTime)),
             Mathf.LerpAngle(currentAngle.z, targetAngle.z, (1 * Time.deltaTime)));

        transform.eulerAngles = currentAngle;
    }


    public void ChangeState(TileStates newState)
    {
        switch (newState)
        {
            case TileStates.Normal:

                //NormalSprite.SetActive(true);
                //FireSprite.SetActive(false);
                //AirSprite.SetActive(false);
                //WaterSprite.SetActive(false);

                //Model.transform.rotation = Quaternion.Euler(0, 0, 0);
                targetAngle = new Vector3(0, 0, 0);



                break;

            case TileStates.Fire:

                //NormalSprite.SetActive(false);
                //FireSprite.SetActive(true);
                //AirSprite.SetActive(false);
                //WaterSprite.SetActive(false);

                //Model.transform.rotation = Quaternion.Euler(-90, -90, 0);
                targetAngle = new Vector3(-90, -90, 0);

                break;

            case TileStates.Water:

                //NormalSprite.SetActive(false);
                //FireSprite.SetActive(false);
                //AirSprite.SetActive(false);
                //WaterSprite.SetActive(true);

                //Model.transform.rotation = Quaternion.Euler(0, 0, -90);
                targetAngle = new Vector3(0, 0, -90);

                break;

            case TileStates.Wind:

                //NormalSprite.SetActive(false);
                //FireSprite.SetActive(false);
                //AirSprite.SetActive(true);
                //WaterSprite.SetActive(false);

                //Model.transform.rotation = Quaternion.Euler(90, -90, 0);
                targetAngle = new Vector3(90, -90, 0);

                break;
        }



    }
}
