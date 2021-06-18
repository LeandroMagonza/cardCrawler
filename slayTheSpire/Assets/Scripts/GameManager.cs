using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public struct ActionQueueElement
{
    public ActionQueueElement(Character character,ActionGroup actionGroup, int resouceCost)
    {
        this.character = character;
        this.actionGroup = actionGroup;
        this.resouceCost = resouceCost;
    }
    public Character character { get; }
    public ActionGroup actionGroup { get; }
    public int resouceCost { get; }
}
public class ActionQueue{
    public Queue<ActionQueueElement> actionQueue = new Queue<ActionQueueElement>();
    public Boolean currentlyExecutingAction = false;

    public void ExecuteNextAction(){
        if (currentlyExecutingAction || actionQueue.Count == 0)
        {
            return;
        }
        currentlyExecutingAction = true;
        // Debug.Log("Begin executing action");
        ActionQueueElement elementToExecute = actionQueue.Dequeue();
        elementToExecute.actionGroup.ExecuteActionGroup(elementToExecute.character,elementToExecute.resouceCost);
        currentlyExecutingAction = false;
        // Debug.Log("End executing action");
    }
    
    public void AddActionToQueue(Character character, ActionGroup actionGroup,int resouceCost){
        ActionQueueElement elementToAdd = new ActionQueueElement(character,actionGroup,resouceCost);
        actionQueue.Enqueue(elementToAdd);
    }
}
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    public static Transform canvasTransform;
    public static Transform boardTransform;
    public static ActionQueue actionQueue = new ActionQueue();
    public int turnNumber = 0;
    public bool receivingPlayerInput = true;
    public static Character mainPlayer {get; private set;} = null;
    private List<List<Character>> teams = new List<List<Character>>();
    private SortedList<int,List<Character>> boardPositions = new SortedList<int,List<Character>>(); 
    public GameObject characterPrefab;
    public GameObject floorColumnImage;
    public List<GameObject> floor = new List<GameObject>();
    public List<GameObject> instancedCharacters;
    public List<GameObject> instancedFloorColumns;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            
            canvasTransform = GameObject.FindGameObjectWithTag("Canvas").transform;
            boardTransform = GameObject.FindGameObjectWithTag("Board").transform;
            mainPlayer = new PlayableCharacter(80,"Carlos",new Energy(3),CharacterModelLibrary.KnightCharacterModel());
            
            
            // mainPlayer.AddCardToDeckById(5);
            mainPlayer.OnCharacterDead += CheckCombatEndCondition;
            mainPlayer.AddCardToDeck(CardLibrary.Charge());
            mainPlayer.AddCardToDeck(CardLibrary.ShrugItOff());
            // mainPlayer.AddCardToDeck(CardLibrary.DeadlyPoison());
            mainPlayer.AddCardToDeck(CardLibrary.GetBehind());
            mainPlayer.AddCardToDeck(CardLibrary.TwinStrike());
            mainPlayer.AddCardToDeck(CardLibrary.TwinStrike());
            mainPlayer.AddCardToDeck(CardLibrary.TwinStrike());
            mainPlayer.AddCardToDeck(CardLibrary.TwinStrike());
            mainPlayer.AddCardToDeck(CardLibrary.TwinStrike());
            mainPlayer.AddCardToDeck(CardLibrary.TwinStrike());
            mainPlayer.AddCardToDeck(CardLibrary.TwinStrike());
            mainPlayer.AddCardToDeck(CardLibrary.Strike());
            // mainPlayer.AddCardToDeck(CardLibrary.Strike());
            // mainPlayer.AddCardToDeck(CardLibrary.Strike());
            // mainPlayer.AddCardToDeck(CardLibrary.Thunder());
            // mainPlayer.AddCardToDeck(CardLibrary.Bash());
            mainPlayer.abilities.Add(AbilityLibrary.MoveLeft());
            mainPlayer.abilities.Add(AbilityLibrary.MoveRight());
            mainPlayer.abilities.Add(AbilityLibrary.Strike());

            // Character ally = new NonPlayableCharacter(80,"Roberto",new Energy(2),CharacterModelLibrary.KnightCharacterModel());
            // ally.AddCardToDeck(CardLibrary.GetBehind());
            // Character ally2 = new Character(80,"Mario",new Energy(3));
            List<Character> allyTeam = new List<Character>();
            MoveCharacterToBoardColumn(mainPlayer,0);
            // MoveCharacterToBoardColumn(ally,1);
            // MoveCharacterToBoardColumn(mainPlayer,0);
            // MoveCharacterToBoardColumn(mainPlayer,0);
            allyTeam.Add(mainPlayer);
            // allyTeam.Add(mainPlayer);
            // allyTeam.Add(mainPlayer);
            // ally.TurnRight();
            // allyTeam.Add(ally);
            // allyTeam.Add(ally2);
            teams.Add(allyTeam);

            List<Character> NpcTeam = new List<Character>();
            teams.Add(NpcTeam);


            Character Npc = new NonPlayableCharacter(80,"bicho",new Energy(3),CharacterModelLibrary.GoblinCharacterModel());
            Npc.AddCardToDeck(CardLibrary.Thrash());
            NpcTeam.Add(Npc);
            MoveCharacterToBoardColumn(Npc,1);
            Npc.OnCharacterDead += CheckCombatEndCondition;

            Character Npc2 = new NonPlayableCharacter(80,"bicho2",new Energy(3),CharacterModelLibrary.GoblinCharacterModel());
            Npc2.AddCardToDeck(CardLibrary.Bellow());
            NpcTeam.Add(Npc2);
            MoveCharacterToBoardColumn(Npc2,1);
            Npc2.OnCharacterDead += CheckCombatEndCondition;

            

            // NpcTeam.Add(Npc2);
            // MoveCharacterToBoardColumn(Npc2,1);
            // MoveCharacterToBoardColumn(Npc2,3);
            // MoveCharacterToBoardColumn(Npc2,4);
            // MoveCharacterToBoardColumn(Npc2,5);
            // MoveCharacterToBoardColumn(Npc,3);
            // MoveCharacterToBoardColumn(Npc,4);
            // MoveCharacterToBoardColumn(Npc,4);
            // MoveCharacterToBoardColumn(Npc,4);
            // MoveCharacterToBoardColumn(Npc,1);
            InstanceCharacters();
    
        }
    }
    public void Start(){
        StartTurn();
    }

    private void InstanceCharacters(){
        
        int firstColumnPositionX = 85;
        int lastColumnPositionX = 1515;
        int amountColumns = boardPositions.Keys.Max()+1;
        // Debug.Log(amountColumns);
        int columnWidth = (lastColumnPositionX-firstColumnPositionX)/(amountColumns);
        // int firstRowPositionY = 325;
        // int lastRowPositionY = 725;
        float minScaleY = 0;
        int firstRowPositionY = 225;
        int lastRowPositionY = 615;
        int columnHeight = lastRowPositionY - firstRowPositionY;
        //vacio el piso
        floor.Clear();
        for (int columnNumber = 0; columnNumber < amountColumns; columnNumber++)
        {
            
            if (boardPositions.ContainsKey(columnNumber))
            {
                
            List<Character> charactersInColumn = boardPositions[columnNumber];
            int columnPositionX = firstColumnPositionX + columnWidth*columnNumber + columnWidth/2 ;
            // int columnPositionX = firstColumnPositionX + columnWidth*column.Key + halfColumnWidth;
            int amountCharactersInColumn = charactersInColumn.Count;
            int characterRowSeparation = columnHeight/(amountCharactersInColumn+1);
            
            
            float prefabHeight = characterPrefab.GetComponent<RectTransform>().sizeDelta.y;
            float prefabScaleY = characterPrefab.transform.localScale.y;
            float characterHeight = characterPrefab.GetComponent<RectTransform>().sizeDelta.y*characterPrefab.transform.localScale.y;

            if ((amountCharactersInColumn-1)*characterHeight>columnHeight)
            {
                //scale down characters
                // Debug.Log("characters scaled down");
                // Debug.Log("("+columnHeight+" / "+amountCharactersInColumn+"-1)"+"/("+characterHeight+")");

                prefabScaleY = ((columnHeight)/(amountCharactersInColumn-1))/(characterHeight);
                // characterHeight = characterPrefab.GetComponent<RectTransform>().sizeDelta.y*prefabScaleY;
                // Debug.Log("character new size" + characterHeight);
                // characterRowSeparation = (int)characterHeight/2;
            }
            if (minScaleY == 0)
            {
                minScaleY = prefabScaleY;
            }
            else if (minScaleY>prefabScaleY)
            {
                minScaleY = prefabScaleY;
            }

            // if (amountCharactersInColumn*characterHeight>columnHeight)
            // {
                // rowPositionY = firstRowPositionY;
                // Debug.Log("no first separation"+characterRowSeparation);
            // }
            // else
            // {
                //remove lower bound
                // rowPositionY = firstRowPositionY+characterRowSeparation;
                // Debug.Log("characterRowSeparation"+characterRowSeparation);
            // }

            GameObject instancedFloorColumnImage;
            instancedFloorColumnImage = Instantiate(floorColumnImage, new Vector3(0, 0, 0), Quaternion.identity);
            instancedFloorColumnImage.transform.SetParent(boardTransform, false);
            instancedFloorColumnImage.GetComponent<FloorColumn>().columnNumber = columnNumber;
            instancedFloorColumns.Add(instancedFloorColumnImage);
            
            foreach (Character character in charactersInColumn)
            {

                GameObject instancedCharacter;
                instancedCharacter = Instantiate(characterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                // instancedCharacter = Instantiate(characterPrefab, new Vector3(columnPositionX, rowPositionY, 0), Quaternion.identity);
                // instancedCharacter.transform.SetParent(canvasTransform, false);
                instancedCharacter.transform.SetParent(instancedFloorColumnImage.transform, false);
                instancedCharacter.AddComponent<CharacterDataTemplate>();
                instancedCharacter.GetComponent<CharacterDataTemplate>().setCharacter(character);
                // RuntimeAnimatorController characterAnimatorControler = (RuntimeAnimatorController)Resources.Load("Prefabs/Characters/Knight/KnightController");
                
                
                
                instancedCharacter.transform.Find("characterSprite").gameObject.GetComponent<Animator>().runtimeAnimatorController = character.characterModel.characterAnimatorControler;
                instancedCharacter.GetComponent<Button>().onClick.AddListener ( delegate { FocusCharacter (character,instancedCharacter); });
                // Debug.Log(instancedCharacter.GetComponent<RectTransform>().sizeDelta.y*instancedCharacter.transform.localScale.y);
                // instancedCharacter.transform.localScale = new Vector3(prefabScaleY,prefabScaleY,1);
                // Debug.Log(instancedCharacter.GetComponent<RectTransform>().sizeDelta.y*instancedCharacter.transform.localScale.y);
                instancedCharacters.Add(instancedCharacter);

                // rowPositionY += characterRowSeparation+(int)characterHeight/2;
            }

            }

            
        }
        // foreach (GameObject instancedCharacter in instancedCharacters)
        // {
        //     instancedCharacter.transform.localScale = new Vector3(minScaleY,minScaleY,1);
        // }

    }


    public void Update(){

        actionQueue.ExecuteNextAction();

        if (mainPlayer.status != CharacterStatus.DEAD && receivingPlayerInput)
        {
            if(Input.GetKeyUp("e")) {
                StartCoroutine(EndTurn());
            }
            if(Input.GetKeyUp("space")) {
                if (mainPlayer.selectedActionGroup == null)
                {
                    Debug.Log("No card selected");
                }
                else if (mainPlayer.PlayActionGroup())
                {
                    
                }
            }
        }

    }

    
    public void FocusCharacter(Character characterToSelect, GameObject instancedCharacter){

        GameObject previouslyFocusedCharacter = FindCharacterInstance(mainPlayer.focus);
        if (previouslyFocusedCharacter != null)
        {
            previouslyFocusedCharacter.transform.Find("selectionMarker").GetComponent<Image>().enabled = false;
            // previouslyFocusedCharacter.transform.position = new Vector2(previouslyFocusedCharacter.transform.position.x,previouslyFocusedCharacter.transform.position.y-25);
        }
        mainPlayer.SetFocus(characterToSelect);
        instancedCharacter.transform.Find("selectionMarker").GetComponent<Image>().enabled = true;
        // instancedCharacter.transform.position = new Vector2(instancedCharacter.transform.position.x,instancedCharacter.transform.position.y+25);
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

    public GameObject FindFloorColumnInstance(int columnNumber){

            foreach (GameObject instancedFloorColumn in instancedFloorColumns)
            {
                if (instancedFloorColumn.GetComponent<FloorColumn>().columnNumber == columnNumber)
                {
                    return instancedFloorColumn;
                }
            }
            return null;
    }


    public IEnumerator EndTurn(){
        receivingPlayerInput = false;
        // Debug.Log("receivingPlayerInput = false");


        // Debug.Log("Loops: "+loops);
        EventManager.BeforeNpcTurn();

        EventManager.NpcTurn();

        while (actionQueue.actionQueue.Count != 0 ){
            yield return null;
        }

        EventManager.AfterNpcTurn();
        // Debug.Log("actionsInQueue " + actionQueue.actionQueue.Count);
        StartTurn();
        receivingPlayerInput = true;
        // Debug.Log("receivingPlayerInput = true");
        
    }
    public void StartTurn(){
        turnNumber += 1;
        EventManager.BeforePlayerTurn();
        // HandManager.Instance.InstanceHand();
        // turnEnded?.Invoke(this,EventArgs.Empty);
        
    }

    public void StartCombat(){
        EventManager.BeforeStartCombat();
        StartTurn();
    }


    public List<Character> GetEnemies(Character character){
        List<Character> enemies = new List<Character>();
        foreach (List<Character> team in teams)
            {
                if (!team.Contains(character))
                {
                    enemies.AddRange(team);
                }
            }
        return enemies;
    }

    public List<Character> GetAllCharacters(Character character){
        List<Character> allCharacters = new List<Character>();
        foreach (List<Character> team in teams)
            {
                allCharacters.AddRange(team);
            }
        return allCharacters;
    }

    public List<Character> GetAllies(Character character){
        List<Character> allies = new List<Character>();
        foreach (List<Character> team in teams)
            {
                if (team.Contains(character))
                {
                    allies.AddRange(team);
                }
            }
        return allies;
    }
    public List<Character> GetOtherAllies(Character character){
        List<Character> allies = new List<Character>();
        foreach (List<Character> team in teams)
            {
                if (team.Contains(character))
                {
                    foreach (Character teamCharacter in team)
                    {
                        if (character != teamCharacter)
                        {
                            allies.Add(teamCharacter);
                        }
                    }
                }
            }
        return allies;
    }
    public Boolean AreOnSameTeam(Character character1,Character character2){
        List<Character> allies = new List<Character>();
        foreach (List<Character> team in teams)
            {
                if (team.Contains(character1) && team.Contains(character2))
                {
                    return true;
                }
            }
        return false;
    }
    public void RemoveCharacterFromBoard(Character character){
        // Debug.Log("Remove Character Called " + character.name);
        if (!boardPositions.Any())
        {
            return;
        }

        int amountColumns = boardPositions.Keys.Max()+1;

        for (int columnNumber = 0; columnNumber < amountColumns; columnNumber++)
        {   
        // Debug.Log("columnNumber "+columnNumber);
            if (boardPositions.Keys.Contains(columnNumber))
            {
                if (boardPositions[columnNumber].Contains(character))
                {
                    boardPositions[columnNumber].Remove(character);
                }
            }
            else
            {
                // Debug.Log("added column "+columnNumber);
                List<Character> newColumnList = new List<Character>();
                boardPositions.Add(columnNumber,newColumnList);
            }

        }

    }
    public void AddCharacterToBoard(Character character, int columnNumberToAddCharacter){
        // Debug.Log("Add Character Called " + character.name);

        int startingColumn;

        if (!boardPositions.Any())
        {
            startingColumn = 0;
        }
        else
        {
            startingColumn = boardPositions.Keys.Max()+1;
        }

        for (int columnNumber = startingColumn; columnNumber < columnNumberToAddCharacter+1; columnNumber++)
        {   
        // Debug.Log("columnNumber "+columnNumber);
            if (!boardPositions.Keys.Contains(columnNumber))
            {
                List<Character> newColumnList = new List<Character>();
                boardPositions.Add(columnNumber,newColumnList);
            }

        }

        boardPositions[columnNumberToAddCharacter].Add(character);
    }

    public void MoveCharacterToBoardColumn(Character character, int columnNumberToAddCharacter){
        // Debug.Log("Move Character Called " + character.name);

        int currentCharacterColumnNumber = GetCharacterColumnNumber(character);
        if (columnNumberToAddCharacter < 0)
        {
            return;
        }

        RemoveCharacterFromBoard(character);
        AddCharacterToBoard(character, columnNumberToAddCharacter);

        GameObject instancedCharacter = FindCharacterInstance(character);

        if (instancedCharacter != null)
        {   
            if (currentCharacterColumnNumber >=0 )
            {
                if (currentCharacterColumnNumber < columnNumberToAddCharacter)
                {
                    character.TurnRight();
                    // Debug.Log("character turned Right from"+columnNumberToAddCharacter+" to "+columnNumberToAddCharacter);
                }
                else
                {
                    character.TurnLeft();
                    // Debug.Log("character turned Left from"+currentCharacterColumnNumber+" to "+columnNumberToAddCharacter);
                }
            }

            instancedCharacter.transform.SetParent(canvasTransform,true);
            GameObject floorColumnInstance = FindFloorColumnInstance(columnNumberToAddCharacter);
            // instancedCharacter.transform.SetParent(floorColumnInstance.transform,false);
            // Debug.Log("move instanced character" + character.name);
            
            StartCoroutine(PlaceInstancedCharacterInBoardColumn(instancedCharacter, floorColumnInstance));
            
        }
    }

    
    IEnumerator PlaceInstancedCharacterInBoardColumn(GameObject instancedCharacter, GameObject floorColumnInstance)
    {
        
        Tween characterTween = instancedCharacter.transform.DOMoveX(floorColumnInstance.transform.position.x, 1);
        // Debug.Log("set walking animation");
        instancedCharacter.transform.Find("characterSprite").GetComponent<Animator>().SetBool("walking",true);

        yield return characterTween.WaitForCompletion();
        // This log will happen after the tween has completed
        instancedCharacter.transform.SetParent(floorColumnInstance.transform,false);
        // Debug.Log("unset walking animation");
        instancedCharacter.transform.Find("characterSprite").GetComponent<Animator>().SetBool("walking",false);
        // Debug.Log("Tween completed!");
    }

    public int GetCharacterColumnNumber(Character character){
        if (!boardPositions.Any())
        {
            return -1;
        }
         int amountColumns = boardPositions.Keys.Max()+1;
        for (int columnNumber = 0; columnNumber < amountColumns; columnNumber++)
        {
            if (boardPositions[columnNumber].Contains(character))
            {
                return columnNumber;
            }
        }
        return -1;
    }

    public int GetDistanceBetweenIntancedCharacters(Character character1,Character character2){
        if (character1 == null || character2 == null)
        {
            return -1;
        }
        int character1ColumnNumber = GetCharacterColumnNumber(character1);
        int character2ColumnNumber = GetCharacterColumnNumber(character2);
        return Math.Abs(character1ColumnNumber-character2ColumnNumber);
    }

    public void CheckCombatEndCondition(Character deadCharacter){
        bool allTeamDead;
        // bool anyTeamDead = false;
        foreach (List<Character> team in teams)
            {
                allTeamDead = true;
                foreach (Character character in team){
                    if (character.status != CharacterStatus.DEAD)
                    {
                        allTeamDead = false;
                    }
                }
                if (allTeamDead)
                {
                    // anyTeamDead = true;
                    Debug.Log("Combat ended");
                }
            }



    }
    
}

