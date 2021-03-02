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
      return new Skill(2,"Defend",1,"Gain 5 block.",actions);
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
      return new Skill(3,"Shrug it Off",1,"Gain 5 block.",actions);
  }

}