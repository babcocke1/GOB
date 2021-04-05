using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Text[] text;
    //0 hunger
    //1 sleep
    //2 bathroom
    //3 discontentment
    //4 is action


    public List<Goal> AIGoals = new List<Goal>();
    public List<Action> AIActions= new List<Action>();
    Action tick;
    const float TICK_LENGTH = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        AIGoals.Add(new Goal("Eat", 4));
        AIGoals.Add(new Goal("Sleep", 3));
        AIGoals.Add(new Goal("Bathroom", 3));   


        AIActions.Add(new Action("eat a turkey"));
        AIActions[0].affectedGoals.Add(new Goal("Eat", -3f));
        AIActions[0].affectedGoals.Add(new Goal("Sleep", +2f));
        AIActions[0].affectedGoals.Add(new Goal("Bathroom", +1f));

        AIActions.Add(new Action("eat trail mix"));
        AIActions[1].affectedGoals.Add(new Goal("Eat", -2f));
        AIActions[1].affectedGoals.Add(new Goal("Sleep", -0f));
        AIActions[1].affectedGoals.Add(new Goal("Bathroom", +1f));

        AIActions.Add(new Action("go to sleep"));
        AIActions[2].affectedGoals.Add(new Goal("Eat", +2f));
        AIActions[2].affectedGoals.Add(new Goal("Sleep", -4f));
        AIActions[2].affectedGoals.Add(new Goal("Bathroom", +2f));

        AIActions.Add(new Action("take a quick nap"));
        AIActions[3].affectedGoals.Add(new Goal("Eat", +1f));
        AIActions[3].affectedGoals.Add(new Goal("Sleep", -2f));
        AIActions[3].affectedGoals.Add(new Goal("Bathroom", +1f));

        AIActions.Add(new Action("drink a smoothie"));
        AIActions[4].affectedGoals.Add(new Goal("Eat", -2f));
        AIActions[4].affectedGoals.Add(new Goal("Sleep", -0f));
        AIActions[4].affectedGoals.Add(new Goal("Bathroom", +3f));

        AIActions.Add(new Action("visit the bathroom"));
        AIActions[5].affectedGoals.Add(new Goal("Eat", 0f));
        AIActions[5].affectedGoals.Add(new Goal("Sleep", 0f));
        AIActions[5].affectedGoals.Add(new Goal("Bathroom", -4f));

        tick = new Action("tick");
        tick.affectedGoals.Add(new Goal("Eat", +4f));
        tick.affectedGoals.Add(new Goal("Sleep", +1f));
        tick.affectedGoals.Add(new Goal("Bathroom", +2f));

        Debug.Log("Starting clock. One hour will pass every " + TICK_LENGTH + " seconds.");
        InvokeRepeating("gameTick", 0f, TICK_LENGTH);

        Debug.Log("Hit A to do something.");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

            Action bestThingToDo = chooseAction(AIActions, AIGoals);
            
            text[4].text = "I think I will " + bestThingToDo.name;

            foreach (Goal goal in AIGoals)
            {
                goal.value += bestThingToDo.getGoalChange(goal);
                goal.value = Mathf.Max(goal.value, 0);
            }

            
            PrintGoals();
        }
    }
    public void gameTick()
    {
        foreach (Goal goal in AIGoals)
        {
            goal.value += tick.getGoalChange(goal);    
            goal.value = Mathf.Max(goal.value, 0);
        }
        PrintGoals();
    }
    float CurrentDiscontentment()
    {
        float total = 0f;
        foreach (Goal goal in AIGoals)
        {
            total += (goal.value * goal.value);
        }
        return total;
    }
    void PrintGoals()
    {
        string goalString;

        goalString = "Discontentment: " + CurrentDiscontentment();
        Debug.Log(goalString);
        text[0].text = AIGoals[0].value.ToString();
        text[1].text = AIGoals[1].value.ToString();
        text[2].text = AIGoals[2].value.ToString();
        text[3].text = goalString;
    }

    public Action chooseAction (List<Action> actions, List<Goal> goals)
    {
        Action bestAction = null;
        float bestValue = Mathf.Infinity;
        float currentValue;
        foreach (Action a in actions)
        {
            currentValue = a.getDiscontentment(goals);
            if(currentValue < bestValue)
            {
                bestValue = currentValue;
                bestAction = a;
            }
        }
        return bestAction;
    }
}
