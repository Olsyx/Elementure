using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class GenericTileState
{


    public virtual void OnEnteringAState(TileStates state) { }



    public virtual void OnLeavingAState(TileStates state) { }


    public virtual void Activate() { }

}
