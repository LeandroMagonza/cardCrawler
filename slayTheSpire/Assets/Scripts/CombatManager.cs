// using DG.Tweening;
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;


// public class CombatManager : MonoBehaviour
// {
//     private static CombatManager _instance;
//     public static CombatManager Instance { get { return _instance; } }


//     public int turnNumber = 0;
//     public static Character mainPlayer {get; private set;} = null;
//     private List<List<Character>> teams = new List<List<Character>>(); 
//     public GameObject characterPrefab;
//     public List<GameObject> instancedCharacters;


//     private void Awake()
//     {
//         if (_instance != null && _instance != this)
//         {
//             Destroy(this.gameObject);
//         }
//         else
//         {
//             _instance = this;
            
//             mainPlayer = new Character(80,"Carlos",new Energy(3));
//             mainPlayer.AddCardToDeck(CardLibrary.Defend());
//             mainPlayer.AddCardToDeck(CardLibrary.ShrugItOff());
//             mainPlayer.AddCardToDeck(CardLibrary.Defend());
//             mainPlayer.AddCardToDeck(CardLibrary.TwinStrike());
//             mainPlayer.AddCardToDeck(CardLibrary.GetBehind());
//             mainPlayer.AddCardToDeck(CardLibrary.Strike());
//             mainPlayer.AddCardToDeck(CardLibrary.Strike());
//             mainPlayer.AddCardToDeck(CardLibrary.Strike());
//             mainPlayer.AddCardToDeck(CardLibrary.Strike());
//             mainPlayer.AddCardToDeck(CardLibrary.Bash());
//             // Character ally = new NonPlayableCharacter(80,"Roberto",new Energy(1));
//             // ally.AddCardToDeck(CardLibrary.GetBehind());
//             // Character ally2 = new Character(80,"Mario",new Energy(3));
//             List<Character> allyTeam = new List<Character>();
//             allyTeam.Add(mainPlayer);
//             // allyTeam.Add(ally);
//             // allyTeam.Add(ally2);
//             teams.Add(allyTeam);

//             Character Npc = new NonPlayableCharacter(80,"bicho",new Energy(3));
//                        // Npc.AddCardToDeck(CardLibrary.Bellow());
//             Npc.AddCardToDeck(CardLibrary.Thrash());
//             // Character Npc2 = new NonPlayableCharacter(80,"goblin",new Energy(3));
//             List<Character> NpcTeam = new List<Character>();
//             NpcTeam.Add(Npc);
//             // NpcTeam.Add(Npc2);
//             teams.Add(NpcTeam);
//             InstanceCharacters();
            
//         }
//     }
//     public void Start(){
//         StartTurn();
//     }

//     private void InstanceCharacters(){
        
//         int firstRowPositionX = 0;
//         int lastRowPositionX = 830;
//         int startingTeamZonePositionX = 0;
//         int firstRowPositionY = 120;
//         int characterZoneWidth = 370;
//         int characterSeparation = 50;
//         int characterSize = 100;
//         int teamZoneSeparation = 200; 
//         foreach (List<Character> Team in this.teams)
//         {   
//             int characterWidth = startingTeamZonePositionX;

//             foreach (Character character in Team)
//             {
//                 GameObject instancedCharacter;
//                 instancedCharacter = Instantiate(characterPrefab, new Vector3(characterWidth, firstRowPositionY, 2), Quaternion.identity);
//                 instancedCharacter.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
//                 // instancedCharacter.transform.localScale = new Vector3(1, 1, 1);
//                 characterWidth += characterSize + characterSeparation;
//                 instancedCharacter.AddComponent<CharacterDataTemplate>();
//                 instancedCharacter.GetComponent<CharacterDataTemplate>().setCharacter(character);
//                 instancedCharacter.GetComponent<Button>().onClick.AddListener ( delegate { FocusCharacter (character,instancedCharacter); });

//                 instancedCharacters.Add(instancedCharacter);
//             }

//             startingTeamZonePositionX += characterZoneWidth + teamZoneSeparation;
//         }
//     }


//     public void Update(){
//         if (mainPlayer.status != CharacterStatus.DEAD)
//         {
//             if(Input.GetKeyUp("e")) {
//                 EndTurn();
//             }
//             if(Input.GetKeyUp("space")) {
//                 if (mainPlayer.selectedActionGroup == null)
//                 {
//                     Debug.Log("No card selected");
//                 }
//                 else if (mainPlayer.PlayActionGroup())
//                 {
//                     // HandManager.Instance.InstanceHand();
//                     // animation["Attacking"].wrapMode = WrapMode.Once;
//                     // FindCharacterInstance(mainPlayer).transform.Find("characterSprite").GetComponent<Animator>().SetBool("attack",false);
                    
//                 }
//             }
//         }

//     }

    
//     public void FocusCharacter(Character characterToSelect, GameObject instancedCharacter){

//         GameObject previouslyFocusedCharacter = FindCharacterInstance(mainPlayer.focus);
//         if (previouslyFocusedCharacter != null)
//         {
//             previouslyFocusedCharacter.transform.position = new Vector2(previouslyFocusedCharacter.transform.position.x,previouslyFocusedCharacter.transform.position.y-25);
//         }
//         mainPlayer.SetFocus(characterToSelect);
//         instancedCharacter.transform.position = new Vector2(instancedCharacter.transform.position.x,instancedCharacter.transform.position.y+25);
//     }

 
//     public GameObject FindCharacterInstance(Character character){
//         if (character != null){
//             foreach (GameObject instancedCharacter in instancedCharacters)
//             {
//                 if (instancedCharacter.GetComponent<CharacterDataTemplate>().character == character)
//                 {
//                     return instancedCharacter;
//                 }
//             }
//         }
//         return null;
//     }

//     public void EndTurn(){
//         // Debug.Log("End Turn Called");
//         EventManager.BeforeNpcTurn();

//         EventManager.NpcTurn();

//         EventManager.AfterNpcTurn();
        
//         StartTurn();
        
//     }
//     public void StartTurn(){
//         turnNumber += 1;
//         EventManager.BeforePlayerTurn();
//         // HandManager.Instance.InstanceHand();
//         // turnEnded?.Invoke(this,EventArgs.Empty);
        
//     }

//     public void StartCombat(){
//         EventManager.BeforeStartCombat();
//         StartTurn();
//     }


//     public List<Character> GetEnemies(Character character){
//         List<Character> enemies = new List<Character>();
//         foreach (List<Character> team in teams)
//             {
//                 if (!team.Contains(character))
//                 {
//                     enemies.AddRange(team);
//                 }
//             }
//         return enemies;
//     }

//     public List<Character> GetAllies(Character character){
//         List<Character> allies = new List<Character>();
//         foreach (List<Character> team in teams)
//             {
//                 if (team.Contains(character))
//                 {
//                     allies.AddRange(team);
//                 }
//             }
//         return allies;
//     }
//     public List<Character> GetOtherAllies(Character character){
//         List<Character> allies = new List<Character>();
//         foreach (List<Character> team in teams)
//             {
//                 if (team.Contains(character))
//                 {
//                     foreach (Character teamCharacter in team)
//                     {
//                         if (character != teamCharacter)
//                         {
//                             allies.Add(teamCharacter);
//                         }
//                     }
//                 }
//             }
//         return allies;
//     }
//     public Boolean AreOnSameTeam(Character character1,Character character2){
//         List<Character> allies = new List<Character>();
//         foreach (List<Character> team in teams)
//             {
//                 if (team.Contains(character1) && team.Contains(character2))
//                 {
//                     return true;
//                 }
//             }
//         return false;
//     }
// }

