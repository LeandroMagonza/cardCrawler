using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class EventManager : MonoBehaviour{
    
    public static event EventHandler beforeNpcTurn;  
    public static event EventHandler npcTurn;  
    public static event EventHandler afterNpcTurn;  
    public static event EventHandler beforePlayerTurn;  
    public static event EventHandler beforeStartCombat;  

    public static void BeforeNpcTurn(){
        beforeNpcTurn?.Invoke(beforeNpcTurn,EventArgs.Empty);
    }
    public static void NpcTurn(){
        npcTurn?.Invoke(npcTurn,EventArgs.Empty);
    }
    public static void AfterNpcTurn(){
        afterNpcTurn?.Invoke(afterNpcTurn,EventArgs.Empty);
    }
    public static void BeforePlayerTurn(){
        beforePlayerTurn?.Invoke(beforePlayerTurn,EventArgs.Empty);
    }
    public static void BeforeStartCombat(){
        beforeStartCombat?.Invoke(beforeStartCombat,EventArgs.Empty);
    }
 
    



}