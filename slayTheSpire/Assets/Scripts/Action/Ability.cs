using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ability : ActionGroup
{
    public int totalCoolDown;
    public int currentCoolDown = 0;
    public Ability(
        string name,
        ResourceCost mainResourceCost,
        string description,
        Sprite icon,
        List<Action> actions,
        PlayCondition playCondition = null,
        EndOfExecutionProcess endOfExecutionProcess = null,
        int totalCoolDown = 0
        ) :
        base(name, mainResourceCost, description,icon, actions, playCondition, endOfExecutionProcess)
    {
        this.totalCoolDown = totalCoolDown;
    }

    public virtual string GetAbilityType()
    {
        return "Ability";
    }
}
public class SkillAbility : Ability
{


    public SkillAbility(
        string name,
        ResourceCost mainResourceCost,
        string description,
        Sprite icon,
        List<Action> actions,
        PlayCondition playCondition = null,
        EndOfExecutionProcess endOfExecutionProcess = null,
        int totalCoolDown = 0
        ) :
        base(name, mainResourceCost, description, icon, actions, playCondition, endOfExecutionProcess,totalCoolDown)
    { }
    public override string GetAbilityType()
    {
        return "Skill";
    }

}
public class AttackAbility : Ability
{
    public AttackAbility(
        string name,
        ResourceCost mainResourceCost,
        string description,
        Sprite icon,
        List<Action> actions,
        PlayCondition playCondition = null,
        EndOfExecutionProcess endOfExecutionProcess = null,
        int totalCoolDown = 0
        ) :
        base(name, mainResourceCost, description, icon, actions, playCondition, endOfExecutionProcess,totalCoolDown)
    { }
    public override string GetAbilityType()
    {
        return "Attack";
    }
}
// public class Power : Ability{
//        public int id;
//     public string name;
//     public int cost;
//     public string description;
//     public List<Action> actions = new List<Action>();
//     public Power(int id,
//               string name,
//               int cost,
//               string description,
//               List<Action> actions)
//     {
//         // this.id = id;
//         // this.name = name;
//         // this.cost = cost;
//         // this.description = description;
//         // this.actions = actions;
//     }
// *//
