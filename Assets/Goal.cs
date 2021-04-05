using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal 
{
    public string name;
    public float value;

    public Goal(string name, float value)
    {
        this.name = name;
        this.value = value;
    }
    public float getDiscontentment(float value)
    {
        return value * value;
    }


}
