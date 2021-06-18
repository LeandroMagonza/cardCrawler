using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect{
  public event EventHandler OnRemoveBuff; 
  public virtual void ExecuteEffect(Character executer,List<Character> targets,int mainResourceCost) {
  }
  public virtual void RemoveBuff(Character executer,List<Character> targets) {
  }
  protected virtual void CallOnRemoveBuff(){
    OnRemoveBuff?.Invoke(this,EventArgs.Empty);
  }
}

public class DamageEffect: Effect{
  int damage;
  int times;
  DamageType damageType;
  public DamageEffect(int damage,int times,DamageType damageType){
    this.damage=damage;
    this.times = times;
    this.damageType = damageType;
    }
  public override void ExecuteEffect(Character executer,List<Character> targets,int mainResourceCost) {
    // targets.forEach(target=> target.reciveDamage(this.damage));
    foreach(Character target in targets){
          for (var i = 0; i < times; i++)
          {
            target.ReciveDamage(executer.DealDamage(damage,damageType),damageType);
          }
        }
  }
}
public class XDamageEffect: Effect{
  int damage;
  int times;
  DamageType damageType;
  public XDamageEffect(int damage,int times,DamageType damageType){
    this.damage = damage;
    this.times = times;
    this.damageType = damageType;
    }
  public override void ExecuteEffect(Character executer,List<Character> targets,int mainResourceCost) {
    // targets.forEach(target=> target.reciveDamage(this.damage));
    foreach(Character target in targets){
          for (var i = 0; i < times; i++)
          {
            target.ReciveDamage(executer.DealDamage(damage*mainResourceCost,damageType),damageType);
          }
        }
  }
}

public class LoseFlatHealthEffect: Effect{
  int damage;
  public LoseFlatHealthEffect(int damage){this.damage=damage;}
  public override void ExecuteEffect(Character executer,List<Character> targets,int mainResourceCost) {
    // targets.forEach(target=> target.reciveDamage(this.damage));
    foreach(Character target in targets){
        target.LoseFlatHealth(this.damage);
        }
  }
}
public class PoisonEffect: Effect{
  int poisonAmount;
  public PoisonEffect(int poisonAmount){this.poisonAmount=poisonAmount;}
  public override void ExecuteEffect(Character executer,List<Character> targets,int mainResourceCost) {
    foreach(Character target in targets){
        target.LoseFlatHealth(this.poisonAmount);
        }
        this.poisonAmount --;
        if (this.poisonAmount<1)
        {
          CallOnRemoveBuff();
        }
  }
}
public class BlockEffect : Effect{
  int block;
  public BlockEffect(int block){this.block=block;}
  public override void ExecuteEffect(Character executer,List<Character> targets,int mainResourceCost) {
    foreach(Character target in targets){
        target.AddBlock(this.block);
        }
  }
}
public class ModifyFlatOffensesEffect : Effect{
  int gain;
  int level;
  DamageType damageType;
  public ModifyFlatOffensesEffect(int level,int gain,DamageType damageType){
    this.gain=gain;
    this.level=level;
    this.damageType = damageType;
  }

  public override void ExecuteEffect(Character executer,List<Character> targets,int mainResourceCost) {
    foreach(Character target in targets){
        target.ModifyFlatOffenses(level,gain,damageType);
        }
  }
}
public class DrawEffect : Effect{
  int amountOfCardsToDraw;
  public DrawEffect(int amountOfCardsToDraw){this.amountOfCardsToDraw = amountOfCardsToDraw;}
  public override void ExecuteEffect(Character executer,List<Character> targets,int mainResourceCost) {
    foreach(Character target in targets){
        target.DrawCard(this.amountOfCardsToDraw);
        }
  }
}
public class TeleportToCharacterFocusEffect : Effect{
  public TeleportToCharacterFocusEffect(){}
  public override void ExecuteEffect(Character executer,List<Character> targets,int mainResourceCost) {
    foreach(Character target in targets){
      if (target.focus != null)
      {
        int columnToMoveTo = GameManager.Instance.GetCharacterColumnNumber(target.focus);
        if (columnToMoveTo >= 0)
        {
          GameManager.Instance.MoveCharacterToBoardColumn(target,columnToMoveTo);
        }
        else
        {
          // Debug.Log("negative column");
        }
        
      }
      else
      {
        // Debug.Log("No focus");
      }
        
    }
  }
}
public class MoveTargetAmountOfColumnsEffect : Effect{
  int amountOfColumns;
  FacingDirection direction;
  public MoveTargetAmountOfColumnsEffect(int amountOfColumns,FacingDirection direction){
    this.amountOfColumns = amountOfColumns;
    this.direction = direction;
    }
  public override void ExecuteEffect(Character executer,List<Character> targets,int mainResourceCost) {
    foreach(Character target in targets){

        int currentColumn = GameManager.Instance.GetCharacterColumnNumber(target);

        if (direction == FacingDirection.RIGHT && currentColumn+amountOfColumns >=0)
        {
          GameManager.Instance.MoveCharacterToBoardColumn(target,currentColumn+amountOfColumns);
        }
        else if (direction == FacingDirection.LEFT && currentColumn-amountOfColumns >=0)
        {
          GameManager.Instance.MoveCharacterToBoardColumn(target,currentColumn-amountOfColumns);
        }
        else
        {
          // Debug.Log("negative column");
        }
    }
  }
}
public class ApplyBuffEffect : Effect{

  public delegate Buff InstanciateBuff();
  public InstanciateBuff instanciateBuff;

  Trigger trigger;

  public ApplyBuffEffect(InstanciateBuff instanciateBuff, Trigger trigger){
    this.instanciateBuff = instanciateBuff;
    this.trigger = trigger;
  }

  public override void ExecuteEffect(Character executer,List<Character> targets,int mainResourceCost) {
     foreach(Character target in targets){
          Buff buff = instanciateBuff();
          // Debug.Log(buff.description);
          this.trigger.AddBuffToCharacter(buff,target);
          buff.SubcribeToEndTurn();
          buff.appliedTo = target;
        }
  }
}
public class ApplyStackableBuffEffect : Effect{

  public delegate Buff InstanciateBuff();
  public InstanciateBuff instanciateBuff;

  Trigger trigger;

  public ApplyStackableBuffEffect(InstanciateBuff instanciateBuff, Trigger trigger){
    this.instanciateBuff = instanciateBuff;
    this.trigger = trigger;
  }

  public override void ExecuteEffect(Character executer,List<Character> targets,int mainResourceCost) {
     foreach(Character target in targets){
          Buff buff = instanciateBuff();
          // Debug.Log(buff.description);
          this.trigger.AddBuffToCharacter(buff,target);
          buff.SubcribeToEndTurn();
          buff.appliedTo = target;
        }
  }
}

