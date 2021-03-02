using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action{

    public Target target;
    public Effect effect;

    public Action(
      Target target,
      Effect effect
      ){
        this.target = target;
        this.effect = effect;
    }
    public void ExecuteAction(Character player) {
      this.effect.ExecuteEffect(this.target.GetTargets(player));
    }

  }
