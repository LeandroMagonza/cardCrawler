using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect{
  public virtual void ExecuteEffect(List<Character> targets) {
  }
}

public class DamageEffect: Effect{
  int damage;
  public DamageEffect(int damage){this.damage=damage;}
  public override void ExecuteEffect(List<Character> targets) {
    // targets.forEach(target=> target.reciveDamage(this.damage));
    foreach(Character target in targets){
        target.ReciveDamage(this.damage);
        }
  }
}
public class TrueDamageEffect: Effect{
  int damage;
  public TrueDamageEffect(int damage){this.damage=damage;}
  public override void ExecuteEffect(List<Character> targets) {
    // targets.forEach(target=> target.reciveDamage(this.damage));
    foreach(Character target in targets){
        target.ReciveTrueDamage(this.damage);
        }
  }
}
public class BlockEffect : Effect{
  int block;
  public BlockEffect(int block){this.block=block;}
  public override void ExecuteEffect(List<Character> targets) {
    foreach(Character target in targets){
        target.AddBlock(this.block);
        }
  }
}
public class DrawEffect : Effect{
  int amountOfCardsToDraw;
  public DrawEffect(int amountOfCardsToDraw){this.amountOfCardsToDraw = amountOfCardsToDraw;}
  public override void ExecuteEffect(List<Character> targets) {
    foreach(Character target in targets){
        target.DrawCard(this.amountOfCardsToDraw);
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


  public override void ExecuteEffect(List<Character> targets) {
     foreach(Character target in targets){
          Buff buff = instanciateBuff();
          Debug.Log(buff.description);
          this.trigger.AddBuffToCharacter(buff,target);
          buff.SubcribeToEndTurn();
        }
  }
}

