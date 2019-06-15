using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericTileState
{

    public virtual void OnEnteringAState(GenericTileState state) { }



    public virtual void OnLeavingAState(GenericTileState state) { }


    public virtual void Activate() { }

}
