using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ActionGroup{
    public Sprite icon = null;
    public string name;
    public ResourceCost mainResourceCost;
    public PlayCondition playCondition = null;
    public EndOfExecutionProcess endOfExecutionProcess = null;
    public string description;
    public List<Action> actions;

    public ActionGroup(string name,
                ResourceCost mainResourceCost,
                string description,
                Sprite icon,
                List<Action> actions,
                PlayCondition playCondition = null,
                EndOfExecutionProcess endOfExecutionProcess = null
                ){
        this.name = name;
        this.mainResourceCost = mainResourceCost;
        this.description = description;
        this.icon = icon;
        this.actions = actions;
        this.playCondition = playCondition;
        
    }

    public virtual new string GetType(){
        return "actionGroup";
    }
    public void ExecuteActionGroup(Character player, int mainResourceCost){
        foreach(Action action in this.actions){
            action.ExecuteAction(player,mainResourceCost);
        }
    }
    public void ExecuteEndOfExecutionProcess(Character player, int mainResourceCost){
        if (endOfExecutionProcess != null)
        {
            endOfExecutionProcess.ExecuteEndOfExecutionProcess(player,this,mainResourceCost);
        }
    }

}   
