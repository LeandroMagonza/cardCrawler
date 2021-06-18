using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ResourceCost
{
   public virtual int GetResourceCost(Character character){
       return 0;
   }
   public virtual string GetResourceCostDisplay(Character character){
       return "0";
   }

}
public class FlatCost:ResourceCost
{
    int cost;
    public FlatCost(int cost){
        this.cost = cost;
    }
   public override int GetResourceCost(Character Character){
       return cost;
   }
   public override string GetResourceCostDisplay(Character character){
       return cost.ToString();
   }

}
public class XCost:ResourceCost
{
    public XCost(){
    }
   public override int GetResourceCost(Character character){
       return character.mainResource.CurrentResourceAmount();
   }
   public override string GetResourceCostDisplay(Character character){
       return "X";
   }

}

