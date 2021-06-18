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
    targets.Add(player.focus);
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
    
    if (GameManager.Instance.AreOnSameTeam(player,player.focus)) {
      List<Character> targets = new List<Character>() {player.focus};
    return targets;
    }
    else{
      List<Character> targets = new List<Character>() {player};
    return targets;
    }
  }
}

public class OtherAlliesTarget : Target{
  public OtherAlliesTarget(){
  }
  public override List<Character> GetTargets(Character player) {
    return GameManager.Instance.GetOtherAllies(player);
  }
}
public class AlliesTarget : Target{
  public AlliesTarget(){
  }
  public override List<Character> GetTargets(Character player) {
    return GameManager.Instance.GetAllies(player);
  }
}
public class AllCharactersTarget : Target{
  public AllCharactersTarget(){
  }
  public override List<Character> GetTargets(Character player) {
    return GameManager.Instance.GetAllCharacters(player);
  }
}
