using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public string name;
    public List<Goal> affectedGoals = new List<Goal>();
    public float getGoalChange(Goal g)
    {
        foreach (Goal currentGoal in affectedGoals)
        {
            if (currentGoal.name.Equals(g.name))
            {
                return currentGoal.value;
            }
        }
        return 0f;
    }

    public Action(string name)
    {
        this.name = name;
    }

    public float getDiscontentment(List<Goal> goals)
    {
        float discontentment = 0f;
        float newValue;
        foreach(Goal g in goals)
        {
            newValue = g.value + getGoalChange(g);
            discontentment += g.getDiscontentment(newValue);
        }

        return discontentment;
    }
}
