using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target{
    public Target(){
  }
  public virtual List<Character> GetTargets(Character player) {
    List<Character> targets = new List<Character>();
    return targets;
  }
}

public class FocusTarget : Target{
    public FocusTarget(){
  }
  public override List<Character> GetTargets(Character player) {
    List<Character> targets = new List<Character>();
    targets.Add(player.GetFocus());
    return targets;
  }
}

public class SelfTarget : Target{
    public SelfTarget(){
  }
  public override List<Character> GetTargets(Character player) {
    List<Character> targets = new List<Character>() {player};
    return targets;
  }
}
public class FocusAllyOrSelfTarget : Target{
    public FocusAllyOrSelfTarget(){
  }
  public override List<Character> GetTargets(Character player) {
    
    if (true) {
      List<Character> targets = new List<Character>() {player.GetFocus()};
    return targets;
    }
    else{
      List<Character> targets = new List<Character>() {player};
    return targets;
    }
  }
}

public class TeamTarget : Target{
  public TeamTarget(){
  }
  public override List<Character> GetTargets(Character player) {
    List<Character> targets = new List<Character>();
    return targets;
    //return playerService.playerTeam
  }
}
public class AlliesTarget : Target{
  public AlliesTarget(){
  }
  public override List<Character> GetTargets(Character player) {
    List<Character> targets = new List<Character>();
    return targets;
  }
}
