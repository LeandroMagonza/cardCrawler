using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class PlayCondition
{
    public virtual bool CheckIfPlayable(Character executer, ActionGroup actionGroupToPlay)
    {
        return true;
    }

}
public abstract class PlayConditionGroup : PlayCondition
{
    protected List<PlayCondition> playConditions = new List<PlayCondition>();
    public PlayConditionGroup(List<PlayCondition> playConditions)
    {
        this.playConditions = playConditions;
    }
    public override bool CheckIfPlayable(Character executer, ActionGroup actionGroupToPlay)
    {
        return false;
    }

}
public class ConjunctionPlayConditionGroup : PlayConditionGroup
{
    public ConjunctionPlayConditionGroup(List<PlayCondition> playConditions) : base(playConditions) { }
    public override bool CheckIfPlayable(Character executer, ActionGroup actionGroupToPlay)
    {
        bool playable = true;
        foreach (PlayCondition playCondition in playConditions)
        {
            playable = playable && playCondition.CheckIfPlayable(executer,actionGroupToPlay);
        }
        return playable;
    }

}

public class CharacterDistanceToFocusPlayCondition : PlayCondition
{
    int minDistance;
    int maxDistance;
    public CharacterDistanceToFocusPlayCondition(int minDistance, int maxDistance)
    {
        this.minDistance = minDistance;
        this.maxDistance = maxDistance;
    }
    public override bool CheckIfPlayable(Character executer, ActionGroup actionGroupToPlay)
    {
        int distanceBetweenExecuterAndFocus = GameManager.Instance.GetDistanceBetweenIntancedCharacters(executer, executer.focus);
        // Debug.Log(minDistance+"<"+distanceBetweenExecuterAndFocus+"<"+maxDistance);
        if (minDistance <= distanceBetweenExecuterAndFocus && distanceBetweenExecuterAndFocus <= maxDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
// public class XCost:PlayCondition
// {
//     public XCost(){
//     }
//    public override int GetPlayCondition(Character character){
//        return character.mainResource.CurrentResourceAmount();
//    }

// }

