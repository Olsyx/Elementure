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


    private 

    void Start()
    {

        ChangeState(TileStates.Water);

      
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeState(TileStates newState)
    {
        switch (newState)
        {
            case TileStates.Normal:

                //Model.transform.rotation = Vector3.Lerp()

                Model.transform.rotation = Quaternion.Euler(0, 0, 0);

                break;

            case TileStates.Fire:

                Model.transform.rotation = Quaternion.Euler(-90, -90, 0);

                break;

            case TileStates.Water:

                Model.transform.rotation = Quaternion.Euler(0, 0, -90);

                break;

            case TileStates.Wind:

                Model.transform.rotation = Quaternion.Euler(90, -90, 0);

                break;
        }



    }

}
