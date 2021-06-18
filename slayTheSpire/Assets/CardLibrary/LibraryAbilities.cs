using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class AbilityLibrary
{
    // public static returnCard(int cardID){

    // }
    public static Ability MoveLeft()
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(
        new SelfTarget(),
        new MoveTargetAmountOfColumnsEffect(1,FacingDirection.LEFT)
      ));
        return new Ability("Move Left", new FlatCost(1), "Move Left One Columnn.",ActionGroupIconLibrary.ArrowLeft(), actions);
    }
    public static Ability MoveRight()
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(
        new SelfTarget(),
        new MoveTargetAmountOfColumnsEffect(1,FacingDirection.RIGHT)
      ));
        return new Ability("Move Right", new FlatCost(1), "Move Right One Columnn.",ActionGroupIconLibrary.ArrowRight(), actions);
    }
    public static Ability Strike()
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(
        new FocusTarget(),
        new DamageEffect(6,1,DamageType.SLASH)
      ));
        return new Ability("Strike", new FlatCost(1), "Deal 6 damage to focus.",ActionGroupIconLibrary.Empty(), actions);
    }
   
}