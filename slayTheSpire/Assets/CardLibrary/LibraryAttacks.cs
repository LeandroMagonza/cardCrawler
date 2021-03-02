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
        new DamageEffect(6)
      ));
        return new Attack(1, "Strike", 1, "Deal 6 damage.", actions);
    }
    public static Card Bash()
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(
        new FocusTarget(),
        new DamageEffect(8)
        ));
        actions.Add(new Action(
          new FocusTarget(),
          new ApplyBuffEffect(BuffLibrary.Vulnerable,new OnAttackReceivedTrigger())
        ));
        return new Attack(3, "Bash", 0, "Deal 8 damage. Apply 2 Vulnerable.", actions);
    }
    public static Card Chomp()
    {
        List<Action> actions = new List<Action>();
        actions.Add(new Action(
        new FocusTarget(),
        new DamageEffect(11)
      ));
        return new Attack(4, "Chomp", 1, "Deal 11 damage.", actions);
    }

}