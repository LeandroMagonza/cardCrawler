using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card : ActionGroup
{
    public Card(
        string name,
        ResourceCost mainResourceCost,
        string description,
        Sprite icon,
        List<Action> actions,
        PlayCondition playCondition = null,
        EndOfExecutionProcess endOfExecutionProcess = null
        ) :
        base(name, mainResourceCost, description, icon, actions, playCondition,endOfExecutionProcess)
    {
        if (endOfExecutionProcess == null)
        {
            this.endOfExecutionProcess = new AddToDiscardEndOfExecutionProcess();
        }
    }
    public override string GetType()
    {
        return "Card";
    }
    
}
public class Skill : Card
{
    public Skill(
        string name,
        ResourceCost mainResourceCost,
        string description,
        Sprite icon,
        List<Action> actions,
        PlayCondition playCondition = null,
        EndOfExecutionProcess endOfExecutionProcess = null
        ) :
        base(name, mainResourceCost, description, icon, actions, playCondition,endOfExecutionProcess)
    { }

    public override string GetType()
    {
        return "Skill";
    }

}
public class Attack : Card
{
    public Attack(
        string name,
        ResourceCost mainResourceCost,
        string description,
        Sprite icon,
        List<Action> actions,
        PlayCondition playCondition = null,
        EndOfExecutionProcess endOfExecutionProcess = null
        ) :
        base(name, mainResourceCost, description, icon, actions, playCondition,endOfExecutionProcess)
    { }
    public override string GetType()
    {
        return "Attack";
    }
}
