using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff {
  public Character originator{get;set;}
  public Character appliedTo{get;set;}
  public string description;
  protected Action action;


  public Buff(string description,Action action){
    this.description = description;
    this.action = action;
  }
    public void ExecuteBuff(Character player){
      action.ExecuteAction(player,0);
  }
   public virtual void SubcribeToEndTurn(){
    // GameManager.turnEnded += ReduceDuration;
  }
   public void AddThisBuffToGarbageBuffList(object sender, EventArgs e){
    appliedTo.AddToGarbageBuffList(this);
  }
}

public class TemporaryBuff : Buff {
  int duration;

  public TemporaryBuff(string description,Action action,int duration):
    base(description,action){
      this.duration = duration;
    }

  public void ReduceDuration(object sender, EventArgs e){
    duration -= 1;
    // Debug.Log("Duration Reduced");
    if (duration == 0)
    {
      // Debug.Log("duration = 0");
      EventManager.afterNpcTurn -= ReduceDuration;
      AddThisBuffToGarbageBuffList(this,EventArgs.Empty);
    }
  }

  public override void SubcribeToEndTurn(){
    // Debug.Log("subscribed to endturn");
    EventManager.afterNpcTurn += ReduceDuration;
  }

}

public class StackableBuff : Buff {
  
  public StackableBuff(string description,Action action):
    base(description,action){
      this.action.effect.OnRemoveBuff += AddThisBuffToGarbageBuffList;
    }
}

