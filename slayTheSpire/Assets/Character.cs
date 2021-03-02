using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {

  public string name;
  List<Card> startingDeck = new List<Card>();
  List<Card> deck = new List<Card>();
  List<Card> discard = new List<Card>();
  public List<Card> hand {get ; private set;} = new List<Card>();
  List<Card> exhaust = new List<Card>();
  public List<Buff> onAttackReceivedBuffs = new List<Buff>();
  public List<Buff> onAttackPlayed = new List<Buff>();
  public List<Buff> beforeEnemyTurn = new List<Buff>();
  public List<Buff> afterEnemyTurn = new List<Buff>();
  public List<Buff> beforePlayerTurn = new List<Buff>();
  int handRefill =  5;
  // maxHandSize:number;
  public Resource resource;
  public int currentHp;
  public int maxHp;
  public int block;
  public Character focus ;
  public Card selectedCard = null;

  public event EventHandler OnDisplayValuesModified; 
    public Character(
     int maxHp,
     string name,
     Resource resource
    ){
      this.currentHp = maxHp;
      this.maxHp = maxHp;
      this.block = 0;
      this.name = name;
      EventManager.beforeEnemyTurn += BeforeEnemyTurn;
      EventManager.afterEnemyTurn += AfterEnemyTurn;
      EventManager.beforePlayerTurn += StartTurn;
      this.resource = resource;
  }

  public void ReciveDamage(int damage){
    foreach (Buff buff in onAttackReceivedBuffs)
    {
        buff.ExecuteBuff(this);
    }
    if (this.block>damage) {
      this.block -= damage;
    }
    else{
      int remainingDamage = damage-this.block;
      this.block = 0;
      this.currentHp -= remainingDamage;
    }
    OnDisplayValuesModified?.Invoke(this,EventArgs.Empty);
  }
  public void ReciveTrueDamage(int damage){
    if (this.block>damage) {
      this.block -= damage;
    }
    else{
      int remainingDamage = damage-this.block;
      this.block = 0;
      this.currentHp -= remainingDamage;
    }
    OnDisplayValuesModified?.Invoke(this,EventArgs.Empty);
  }

  public void AddBlock(int block){
    this.block += block;
    OnDisplayValuesModified?.Invoke(this,EventArgs.Empty);
  }

  public void DiscardHand(){
    this.discard.AddRange(this.hand);
    this.hand.Clear();
  }

  public void BeforeEnemyTurn(object sender, EventArgs e){
    // this.DiscardHand();
  }
    public void AfterEnemyTurn(object sender, EventArgs e){
    // Debug.Log("After enemy turn cAlled");
    this.DiscardHand();
    foreach (Buff buff in afterEnemyTurn)
    {
        // buff (this)
    }
  }

  public void StartTurn(object sender, EventArgs e){
    this.block = 0;
    this.DrawCard(this.handRefill);
    // this.selectedCard = this.hand[0];
    this.resource.StartTurn();
    OnDisplayValuesModified?.Invoke(this,EventArgs.Empty);
  }

  public void DrawCard(int amount){
    for (int index = 0; index < amount; index++) {
      if (this.deck.Count == 0 && this.discard.Count > 0) {
        this.RefillDeck();
      }
      if (this.deck.Count != 0)
      {
        this.hand.Add(this.deck[0]);
        this.deck.RemoveAt(0);
      }
    }
  }

  public void RefillDeck(){
    this.deck.AddRange(Shuffle(this.discard));
    this.discard.Clear();
  }

  public void ShuffleDeck(){
    this.deck = Shuffle(this.deck);
  }
 
  public void SetFocus(Character newFocus ){
    this.focus = newFocus;
    OnDisplayValuesModified?.Invoke(this,EventArgs.Empty);
  }
  public Character GetFocus(){
    return this.focus;
  }

  public int GetCurrentDeckSize(){
    return this.deck.Count;
  }
  public int GetCurrentDiscardSize(){
    return this.discard.Count;
  }

  public void PlayCard() {
    if (resource.UseResource(this.selectedCard.cost)) {
      this.selectedCard.ExecuteCard(this);
      this.discard.Add(this.selectedCard); 
      this.hand.Remove(this.selectedCard); 
      this.selectedCard = null; 
      OnDisplayValuesModified?.Invoke(this,EventArgs.Empty);
    }
    // Debug.Log("Card played");
  }

  public void SelectCard(Card card){
    this.selectedCard = card;
  }
  public void AddCardToHand(Card card){
    this.hand.Add(card);
  }
  public void AddCardToStartingDeck(Card card){
    this.startingDeck.Add(card);
  }
  public void AddCardToDeck(Card card){
    this.deck.Add(card);
  }

  public void StartCombat(object sender, EventArgs e){
    // this.block = 0;
    // this.DrawCard(this.handRefill);
    // // this.selectedCard = this.hand[0];
    // this.resource.StartTurn();
  }


  public List<T> Shuffle<T>(List<T> list)  
  {  
      System.Random rng = new System.Random();
      int n = list.Count;  
      while (n > 1) {  
          n--;  
          int k = rng.Next(n + 1);  
          T value = list[k];  
          list[k] = list[n];  
          list[n] = value;  
      }  
      return list;
  }

}





