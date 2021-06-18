using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class CardLibrary {

  public static Card Defend()
  {
      List<Action> actions = new List<Action>();
      actions.Add(new Action(
        new SelfTarget(),
        new BlockEffect(5)
      ));
      return new Skill("Defend",new FlatCost(1),"Gain 5 block.",ActionGroupIconLibrary.Empty(),actions);
  }
  
  public static Card GetBehind()
  {
      List<Action> actions = new List<Action>();
      actions.Add(new Action(
        new FocusAllyOrSelfTarget(),
        new BlockEffect(10)
      ));
      return new Skill("Get Behind!",new FlatCost(2),"You or an ally gain 5 block.",ActionGroupIconLibrary.Empty(),actions);
  }

  public static Card ShrugItOff()
  {
      List<Action> actions = new List<Action>();
      actions.Add(
        new Action(
          new SelfTarget(),
          new BlockEffect(5)
        )
      );
      actions.Add(
        new Action(
          new SelfTarget(),
          new DrawEffect(1)
        )
      );
      return new Skill("Shrug it Off",new FlatCost(1),"Gain 5 block.",ActionGroupIconLibrary.Empty(),actions);
  }

  public static Card Bellow()
  {
      List<Action> actions = new List<Action>();
      actions.Add(
        new Action(
          new SelfTarget(),
          new ModifyFlatOffensesEffect(0,3,DamageType.BLUNT)
        )
      );
      actions.Add(
        new Action(
          new SelfTarget(),
          new BlockEffect(6)
        )
      );
      return new Skill("Bellow(3str6b)", new FlatCost(1),"Gain 3 STR. Gain 6 block.",ActionGroupIconLibrary.Empty(),actions);
  }

  public static Card DeadlyPoison()
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(
          new FocusTarget(),
          new ApplyBuffEffect(BuffLibrary.Poison,new OnBeforeNpcTurnTrigger())
        ));
        return new Skill( "Deadly Poison", new FlatCost(1), "Apply 7 poison.", ActionGroupIconLibrary.Empty(),actions);
    }
  
  
}