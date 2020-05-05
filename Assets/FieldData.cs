using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldData 
{
    private int stoneState;

    public int StoneState
    {
        set
        {
            this.stoneState = value;
            this.placeable = false;
        }
        get
        {
            return this.stoneState;
        }
    }
    private bool placeable;
    public bool Placeable
    {
        set
        {
            this.placeable = value;
        }
        get
        {
            return this.placeable;
        }
    }
    public FieldData()
    {
        this.stoneState = 0;
        this.placeable = false;
    }
}