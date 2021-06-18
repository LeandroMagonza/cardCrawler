using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class CardLibrary
{
    public static Card Strike()
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(
        new FocusTarget(),
        new DamageEffect(6,1,DamageType.SLASH)
      ));
        return new Attack("Strike", new FlatCost(1), "Deal 6 damage.", ActionGroupIconLibrary.Empty(), actions, new CharacterDistanceToFocusPlayCondition(0,1));
    }
    public static Card Skewer()
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(
        new FocusTarget(),
        new XDamageEffect(7,1,DamageType.SLASH)
      ));
        return new Attack("Skewer", new XCost(), "Deal 7X damage.", ActionGroupIconLibrary.Empty(), actions, new CharacterDistanceToFocusPlayCondition(0,1));
    }

    public static Card TwinStrike()
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(
        new FocusTarget(),
        new DamageEffect(5,2,DamageType.SLASH)
      ));
        return new Attack("Twin Strike", new FlatCost(1), "Deal 5 damage twice.", ActionGroupIconLibrary.Empty(), actions, new CharacterDistanceToFocusPlayCondition(0,1));
    }
    public static Card Bash()
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(
        new FocusTarget(),
        new DamageEffect(8,1,DamageType.BLUNT)
        ));
        actions.Add(new Action(
          new FocusTarget(),
          new ApplyBuffEffect(BuffLibrary.Vulnerable,new OnAttackReceivedTrigger())
        ));
        return new Attack("Bash", new FlatCost(2), "Deal 8 damage. Apply 2 Vulnerable.", ActionGroupIconLibrary.Empty(), actions, new CharacterDistanceToFocusPlayCondition(0,1));
    }
    public static Card Chomp()
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(
        new FocusTarget(),
        new DamageEffect(11,1,DamageType.PIERCE)
      ));
        return new Attack("Chomp(11d)", new FlatCost(1), "Deal 11 damage.", ActionGroupIconLibrary.Empty(), actions);
    }
    public static Card Thrash()
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(
        new FocusTarget(),
        new DamageEffect(7,1,DamageType.BLUNT)
      ));
        actions.Add(new Action(
        new SelfTarget(),
        new BlockEffect(5)
      ));
        return new Attack("Thrash(7d5b)", new FlatCost(1), "Deal 7 damage. Gain 5 block", ActionGroupIconLibrary.Empty(), actions);
    }
    public static Card Thunder()
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(
        new AllCharactersTarget(),
        new DamageEffect(4,1,DamageType.ELECTRIC)
      ));
        return new Attack("Thunder", new FlatCost(1), "Deal 4 damage.", ActionGroupIconLibrary.Empty(), actions);
    }
  public static Attack Charge()
  {
      List<Action> actions = new List<Action>();
      actions.Add(new Action(
        new SelfTarget(),
        new TeleportToCharacterFocusEffect()
      ));
      actions.Add(new Action(
        new FocusTarget(),
        new DamageEffect(7,1,DamageType.BLUNT)
      ));
      return new Attack(
        "Charge",
        new FlatCost(1),
        "Charge to selected character, violently.",
        ActionGroupIconLibrary.Empty(),
        actions, new CharacterDistanceToFocusPlayCondition(1,2)
      );
  }
}