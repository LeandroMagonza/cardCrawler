using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card{
    public int id;
    public string name;
    public int cost;
    public string description;
    public List<Action> actions = new List<Action>();

    public Card(int id,
                string name,
                int cost,
                string description,
                List<Action> actions){
        this.id = id;
        this.name = name;
        this.cost = cost;
        this.description = description;
        this.actions = actions;
    }

    public void ExecuteCard(Character player){
        foreach(Action action in this.actions){
        action.ExecuteAction(player);
        }
    }
    public virtual string GetCardType(){
        return "";
    }
}   

public class Skill : Card{
    public Skill(int id, string name, int cost, string description, List<Action> actions):
    base( id,name,cost,description,actions){
    }

    public override string GetCardType(){
        return "Skill";
    }
    
}
public class Attack : Card{
    public Attack(int id, string name, int cost, string description, List<Action> actions):
    base( id,name,cost,description,actions){
    }
     public override string GetCardType(){
        return "Attack";
    }
}
// public class Power : Card{
//        public int id;
//     public string name;
//     public int cost;
//     public string description;
//     public List<Action> actions = new List<Action>();
//     public Power(int id,
//               string name,
//               int cost,
//               string description,
//               List<Action> actions)
//     {
//         // this.id = id;
//         // this.name = name;
//         // this.cost = cost;
//         // this.description = description;
//         // this.actions = actions;
//     }
// *//