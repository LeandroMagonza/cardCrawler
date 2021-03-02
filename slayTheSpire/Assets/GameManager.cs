using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static Character mainPlayer {get; private set;}
    private List<List<Character>> teams = new List<List<Character>>(); 
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public int turnNumber = 0;

    public GameObject cardPrefab;
    public GameObject characterPrefab;
    public List<GameObject> instancedCardsHand;
    public List<GameObject> instancedCharacters;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            
            mainPlayer = new Character(80,"Carlos",new Energy());
            mainPlayer.AddCardToDeck(CardLibrary.Defend());
            mainPlayer.AddCardToDeck(CardLibrary.Defend());
            mainPlayer.AddCardToDeck(CardLibrary.Defend());
            mainPlayer.AddCardToDeck(CardLibrary.Defend());
            mainPlayer.AddCardToDeck(CardLibrary.Strike());
            mainPlayer.AddCardToDeck(CardLibrary.Strike());
            mainPlayer.AddCardToDeck(CardLibrary.Strike());
            mainPlayer.AddCardToDeck(CardLibrary.Strike());
            mainPlayer.AddCardToDeck(CardLibrary.Strike());
            mainPlayer.AddCardToDeck(CardLibrary.Bash());
            Character ally = new Character(80,"Roberto",new Energy());
            Character ally2 = new Character(80,"Mario",new Energy());
            List<Character> allyTeam = new List<Character>();
            allyTeam.Add(mainPlayer);
            allyTeam.Add(ally);
            allyTeam.Add(ally2);
            teams.Add(allyTeam);

            Character enemy = new Character(80,"bicho",new Energy());
            Character enemy2 = new Character(80,"goblin",new Energy());
            List<Character> enemyTeam = new List<Character>();
            enemyTeam.Add(enemy);
            enemyTeam.Add(enemy2);
            teams.Add(enemyTeam);
            InstanceCharacters();
            StartTurn();
        }
    }

    private void InstanceCharacters(){
        
        int startingTeamZoneWidth = -460;
        int characterZoneWidth = 370;
        int characterSeparation = 50;
        int characterSize = 100;
        int teamZoneSeparation = 200; 
        foreach (List<Character> Team in this.teams)
        {   
            int characterWidth = startingTeamZoneWidth;

            foreach (Character character in Team)
            {
                GameObject instancedCharacter;
                instancedCharacter = Instantiate(characterPrefab, new Vector3(characterWidth, 0, 2), Quaternion.identity);
                instancedCharacter.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
                // instancedCharacter.transform.localScale = new Vector3(1, 1, 1);
                characterWidth += characterSize + characterSeparation;
                instancedCharacter.AddComponent<CharacterDataTemplate>();
                instancedCharacter.GetComponent<CharacterDataTemplate>().setCharacter(character);
                instancedCharacter.GetComponent<Button>().onClick.AddListener ( delegate { FocusCharacter (character,instancedCharacter); });

                instancedCharacters.Add(instancedCharacter);
            }

            startingTeamZoneWidth += characterZoneWidth + teamZoneSeparation;
        }
    }


    public void Update(){
          if(Input.GetKeyUp("e")) {
              EndTurn();
     }
          if(Input.GetKeyUp("space")) {
              if (mainPlayer.selectedCard == null)
              {
                  Debug.Log("No card selected");
              }
              else
              {
                mainPlayer.PlayCard();
                // Debug.Log("Block:"+mainPlayer.block);
                InstanceHand();
              }
     }
    }

    public void InstanceHand(){
            foreach (var instancedCard in instancedCardsHand)
            {
                Destroy(instancedCard);
            }
            instancedCardsHand.Clear();

            int cardWidth = 150;
            int minSpacing = 25;
            int handCardAmount = mainPlayer.hand.Count;
            int handCardSpacingX = 800/(handCardAmount-1);
            if (handCardSpacingX > cardWidth + minSpacing)
            {
                handCardSpacingX = cardWidth + minSpacing;
            }
            int handCardPosX = (800-(handCardSpacingX*(handCardAmount-1)-minSpacing))/2-400;

            float minTwist = 30f;
            float maxTwist = -30f;
            // 20f for example, try various values
            float twistPerCard = (maxTwist-minTwist) / (handCardAmount+1);
            float startTwist = twistPerCard+minTwist;

            float scalingFactor = 4f;
            // that should be roughly one-tenth the height of one
            // of your cards, just experiment until it works well
             
            foreach (Card card in mainPlayer.hand)
            {
                GameObject instancedCard;
                float nudgeThisCard = Mathf.Abs(startTwist);
                nudgeThisCard *= scalingFactor;   
                instancedCard = Instantiate(cardPrefab, new Vector3(handCardPosX, -175f-nudgeThisCard, 0), Quaternion.Euler(0, 0, startTwist));
                instancedCard.transform.SetParent (GameObject.FindGameObjectWithTag("Canvas").transform, false);
                handCardPosX+=handCardSpacingX;
                startTwist += twistPerCard;
                instancedCard.AddComponent<CardDataTemplate>();
                instancedCard.GetComponent<CardDataTemplate>().setCard(card);
                instancedCard.tag = "HandCard";
            // Debug.Log(instancedCard.transform.Find("name").gameObject.GetComponent<UnityEngine.UI.Text>().text);
                instancedCard.transform.Find("name").gameObject.GetComponent<UnityEngine.UI.Text>().text = card.name;
                instancedCard.transform.Find("type").gameObject.GetComponent<UnityEngine.UI.Text>().text = card.GetCardType();
                instancedCard.transform.Find("resourceCost").gameObject.GetComponent<UnityEngine.UI.Text>().text = card.cost.ToString();
                instancedCard.transform.Find("description").gameObject.GetComponent<UnityEngine.UI.Text>().text = card.description;

                instancedCard.GetComponent<Button>().onClick.AddListener ( delegate { SelectCard (card,instancedCard); });

                instancedCardsHand.Add(instancedCard);
            }

    }

    public void SelectCard(Card cardToSelect, GameObject instancedCard){

        GameObject previouslySelectedCard = FindCardInstance(mainPlayer.selectedCard);
        if (previouslySelectedCard != null)
        {
            previouslySelectedCard.transform.position = new Vector2(previouslySelectedCard.transform.position.x,previouslySelectedCard.transform.position.y-25);
        }
        mainPlayer.SelectCard(cardToSelect);
        instancedCard.transform.position = new Vector2(instancedCard.transform.position.x,instancedCard.transform.position.y+25);
    }

    public void FocusCharacter(Character characterToSelect, GameObject instancedCharacter){

        GameObject previouslyFocusedCharacter = FindCharacterInstance(mainPlayer.GetFocus());
        if (previouslyFocusedCharacter != null)
        {
            previouslyFocusedCharacter.transform.position = new Vector2(previouslyFocusedCharacter.transform.position.x,previouslyFocusedCharacter.transform.position.y-25);
        }
        mainPlayer.SetFocus(characterToSelect);
        instancedCharacter.transform.position = new Vector2(instancedCharacter.transform.position.x,instancedCharacter.transform.position.y+25);
    }

    public GameObject FindCardInstance(Card card){
        if (card != null){
            foreach (GameObject instancedCard in instancedCardsHand)
            {
                if (instancedCard.GetComponent<CardDataTemplate>().card == card)
                {
                    return instancedCard;
                }
            }
        }
        return null;
    }
    public GameObject FindCharacterInstance(Character character){
        if (character != null){
            foreach (GameObject instancedCharacter in instancedCharacters)
            {
                if (instancedCharacter.GetComponent<CharacterDataTemplate>().character == character)
                {
                    return instancedCharacter;
                }
            }
        }
        return null;
    }

    public void EndTurn(){
        // Debug.Log("End Turn Called");
        EventManager.BeforeEnemyTurn();

        EventManager.OnEnemyTurn();

        EventManager.AfterEnemyTurn();
        
        StartTurn();
        
    }
    public void StartTurn(){
        turnNumber += 1;
        EventManager.BeforePlayerTurn();
        InstanceHand();
        // turnEnded?.Invoke(this,EventArgs.Empty);
        
    }

    public void StartCombat(){
        EventManager.BeforeStartCombat();
        StartTurn();
    }
}
