using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class EventManager : MonoBehaviour{
    
    public static event EventHandler beforeEnemyTurn;  
    public static event EventHandler onEnemyTurn;  
    public static event EventHandler afterEnemyTurn;  
    public static event EventHandler beforePlayerTurn;  
    public static event EventHandler beforeStartCombat;  

    public static void BeforeEnemyTurn(){
        beforeEnemyTurn?.Invoke(beforeEnemyTurn,EventArgs.Empty);
    }
    public static void OnEnemyTurn(){
        onEnemyTurn?.Invoke(onEnemyTurn,EventArgs.Empty);
    }
    public static void AfterEnemyTurn(){
        afterEnemyTurn?.Invoke(afterEnemyTurn,EventArgs.Empty);
    }
    public static void BeforePlayerTurn(){
        beforePlayerTurn?.Invoke(beforePlayerTurn,EventArgs.Empty);
    }
    public static void BeforeStartCombat(){
        beforeStartCombat?.Invoke(beforeStartCombat,EventArgs.Empty);
    }

    



}