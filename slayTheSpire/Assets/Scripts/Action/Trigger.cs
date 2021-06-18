using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger {
  public virtual void AddBuffToCharacter(Buff buff,Character character){
  }
}

public class OnAttackReceivedTrigger : Trigger{
  public override void AddBuffToCharacter(Buff buff,Character character){
    character.onAttackReceivedBuffs.Add(buff);
  }
}

public class OnBeforeNpcTurnTrigger : Trigger{
  public override void AddBuffToCharacter(Buff buff,Character character){
    character.beforeNpcTurnBuffs.Add(buff);
  }
}

public class OnAttackPlayed : Trigger{
  public override void AddBuffToCharacter(Buff buff,Character character){
    character.onAttackPlayedBuffs.Add(buff);
  }
}