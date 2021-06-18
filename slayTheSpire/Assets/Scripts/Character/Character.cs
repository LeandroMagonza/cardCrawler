using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Character
{

    public string name;
    public CharacterModel characterModel;
    public FacingDirection facingDirection;
    public CharacterStatus status
    {
        get { return _status; }
        set
        {
            _status = value;
            Debug.Log(value);
            if (value == CharacterStatus.DEAD)
            {
                CallOnCharacterDead();
            }
        }
    }
    protected CharacterStatus _status;
    protected List<Card> startingDeck = new List<Card>();
    protected List<Card> deck = new List<Card>();
    protected List<Card> discard = new List<Card>();
    public List<Card> hand { get; private set; } = new List<Card>();
    public List<Ability> abilities { get; private set; } = new List<Ability>();
    protected List<Card> exhaust = new List<Card>();
    public List<Buff> onAttackReceivedBuffs = new List<Buff>();
    public List<Buff> onAttackPlayedBuffs = new List<Buff>();
    public List<Buff> beforeNpcTurnBuffs = new List<Buff>();
    public List<Buff> afterNpcTurnBuffs = new List<Buff>();
    public List<Buff> beforePlayerTurnBuffs = new List<Buff>();
    public List<Buff> buffsToRemove = new List<Buff>();
    public List<List<Buff>> buffs = new List<List<Buff>>();
    public List<Buff> garbageBuffList = new List<Buff>();

    protected int handRefill = 5;
    // maxHandSize:number;
    // public Dictionary<ResourceType, Resource> mainResources = 
    // new Dictionary<ResourceType, Resource>();

    public Dictionary<DamageType, DefenceModificator> defences =
    new Dictionary<DamageType, DefenceModificator>();
    // maxHandSize:number;
    public Dictionary<DamageType, OffenseModificator> offences =
    new Dictionary<DamageType, OffenseModificator>();
    // public ResourceType mainResourceType;
    public Resource mainResource;
    public int actionPoints;
    public int abilityPoints;

    protected int _currentHp;
    public int currentHp
    {
        get { return _currentHp; }
        private set
        {
            _currentHp = value;
            if (_currentHp <= 0)
            {
                _currentHp = 0;
                status = CharacterStatus.DEAD;
            }
        }
    }
    public int maxHp;
    public int block;
    protected Character _focus;
    public Character focus
    {
        get { return _focus; }
        protected set
        {
            _focus = value;
            CallOnDisplayValuesModified();
        }
    }
    public ActionGroup selectedActionGroup = null;
    public event EventHandler OnDisplayValuesModified;
    public event EventHandler OnHandDisplayModified;
    public event CharacterDeadDelegate OnCharacterDead;
    public delegate void CharacterDeadDelegate(Character character);
    public event DiscardShuffledDelegate OnDiscardShuffled;
    public delegate void DiscardShuffledDelegate(List<Card> discard);
    public event DiscardCardDelegate OnCardDiscarded;
    public delegate void DiscardCardDelegate(Card cardToDiscard);

    public Character(
     int maxHp, string name,
     Resource mainResource,
     CharacterModel characterModel
    )
    {
        this.currentHp = maxHp;
        this.maxHp = maxHp;
        this.block = 0;
        this.name = name;
        EventManager.beforeNpcTurn += BeforeNpcTurn;
        EventManager.afterNpcTurn += AfterNpcTurn;
        EventManager.beforePlayerTurn += StartTurn;
        this.mainResource = mainResource;
        this.characterModel = characterModel;
        buffs.Add(onAttackReceivedBuffs);
        buffs.Add(onAttackPlayedBuffs);
        buffs.Add(beforeNpcTurnBuffs);
        buffs.Add(afterNpcTurnBuffs);
        buffs.Add(beforePlayerTurnBuffs);
        // DefenceModificator defenceModificator = new DefenceModificator();
        // defenceModificator.ChangeFlatModificator(0,3);
        // defences.Add(DamageType.FIRE,defenceModificator);
    }
    public virtual int DealDamage(int damage, DamageType damageType)
    {
        if (!offences.ContainsKey(damageType))
        {
            offences.Add(damageType, new OffenseModificator());
        }
        damage = offences[damageType].DamageAfterOffenses(damage);

        GameObject characterInstance = GameManager.Instance.FindCharacterInstance(this);
        if (characterInstance != null)
        {
            Animator animation = characterInstance.transform.Find("characterSprite").GetComponent<Animator>();
            // Debug.Log("triggered attack animation");
            animation.SetTrigger("attack");
        }
        else
        {
            Debug.Log("WTF");
        }
        return damage;
    }

    public virtual void ReciveDamage(int damage, DamageType damageType)
    {
        foreach (Buff buff in onAttackReceivedBuffs)
        {
            buff.ExecuteBuff(this);
        }
        EmptyGarbageBuffList();
        if (!defences.ContainsKey(damageType))
        {
            defences.Add(damageType, new DefenceModificator());
        }
        damage = defences[damageType].DamageAfterDefences(damage);
        if (this.block > damage)
        {
            this.block -= damage;
        }
        else
        {
            Animator animator = GameManager.Instance.FindCharacterInstance(this).transform.Find("characterSprite").GetComponent<Animator>();
            animator.SetTrigger("takeHit");
            int remainingDamage = damage - this.block;
            this.block = 0;
            this.currentHp -= remainingDamage;

        }
        CallOnDisplayValuesModified();
    }
    public virtual void LoseFlatHealth(int damage)
    {

        this.currentHp -= damage;
        CallOnDisplayValuesModified();
    }

    public virtual void AddBlock(int block)
    {
        this.block += block;
        CallOnDisplayValuesModified();
    }

    public virtual void DiscardHand()
    {
        foreach (Card card in hand.ToList())
        {
            DiscardCard(card);
        }
        selectedActionGroup = null;
    }


    public virtual void BeforeNpcTurn(object sender, EventArgs e)
    {
        foreach (Buff buff in beforeNpcTurnBuffs)
        {
            buff.ExecuteBuff(this);
        }
        EmptyGarbageBuffList();
        // CleanGarbageBuffList();
    }

    public virtual void AfterNpcTurn(object sender, EventArgs e)
    {
        // Debug.Log("After Npc turn cAlled");
        this.DiscardHand();
        foreach (Buff buff in afterNpcTurnBuffs)
        {
            // buff (this)
        }
        EmptyGarbageBuffList();

    }
    public virtual void StartTurn(object sender, EventArgs e)
    {
        this.block = 0;
        DrawCard(handRefill);
        if (this.hand.Count != 0)
        {
            this.selectedActionGroup = this.hand[0];
        }
        this.mainResource.StartTurn();
        if (focus == null)
        {
            focus = GameManager.Instance.GetEnemies(this)[0];
        }
        CallOnDisplayValuesModified();
        // CallOnHandDisplayModified();
    }

    public virtual void DrawCard(int amount)
    {
        for (int index = 0; index < amount; index++)
        {
            if (this.deck.Count == 0 && this.discard.Count > 0)
            {
                this.RefillDeck();
            }
            if (this.deck.Count != 0)
            {
                this.hand.Add(this.deck[0]);
                this.deck.RemoveAt(0);
            }

            // Debug.Log("Called from draw card");
        }
        CallOnHandDisplayModified();
    }

    public virtual void RefillDeck()
    {
        CallOnDiscardShuffled(discard);
        this.deck.AddRange(Shuffle(discard));
        this.discard.Clear();
    }

    public virtual void ShuffleDeck()
    {
        this.deck = Shuffle(this.deck);
    }

    public virtual void SetFocus(Character newFocus)
    {
        this.focus = newFocus;
        CallOnDisplayValuesModified();
    }

    public virtual int GetCurrentDeckSize()
    {
        return this.deck.Count;
    }
    public virtual int GetCurrentDiscardSize()
    {
        return this.discard.Count;
    }
    public Boolean IsActionGroupPlayable(ActionGroup actionGroup, int mainResourceCost)
    {
        if (actionGroup == null)
        {
            return false;
        }

        if (
          actionGroup.playCondition != null
          &&
          !actionGroup.playCondition.CheckIfPlayable(this, actionGroup)
          )
        {
            return false;
        }
        if (!mainResource.UseResource(mainResourceCost))
        {
            return false;
        }

        return true;
    }
    public virtual Boolean PlayActionGroup()
    {

        int mainResourceCost = selectedActionGroup.mainResourceCost.GetResourceCost(this);
        if (!IsActionGroupPlayable(selectedActionGroup, mainResourceCost))
        {
            return false;
        }

        GameManager.actionQueue.AddActionToQueue(this, selectedActionGroup, mainResourceCost);
        // this.selectedActionGroup.ExecuteActionGroup(this,mainResourceCost);

        selectedActionGroup.ExecuteEndOfExecutionProcess(this, mainResourceCost);

        CallOnDisplayValuesModified();
        CallOnHandDisplayModified();
        // Debug.Log("Called from play actionGroup card");
        return true;

        // Debug.Log("Card played");
    }
    // public void DiscardCard(ActionGroup actionGroup){
    //   Debug.Log("Display DiscardCard actionGroup called");
    //   Card card = actionGroup as Card;
    //   DiscardCard(card);

    // }
    public void DiscardCard(Card cardToDiscard)
    {
        // Debug.Log("Display DiscardCard called");
        this.hand.Remove(cardToDiscard);
        this.deck.Remove(cardToDiscard);
        this.discard.Add(cardToDiscard);
        CallOnCardDiscarded(cardToDiscard);
        if (cardToDiscard == selectedActionGroup)
        {
            selectedActionGroup = null;
        }
    }
    // public void DiscardCard(Ability ability){
    //   Debug.Log("Display DiscardCard ability called");
    // }
    public virtual void Select(ActionGroup actionGroup)
    {
        HandManager.UnmarkTemplateAsUnselected(selectedActionGroup);
        selectedActionGroup = actionGroup;
        CallOnDisplayValuesModified();
    }
    public virtual void AddCardToHand(Card card)
    {
        this.hand.Add(card);
        CallOnHandDisplayModified();
    }
    public virtual void AddCardToStartingDeck(Card card)
    {
        this.startingDeck.Add(card);
    }
    public virtual void AddCardToDeck(Card card)
    {
        this.deck.Add(card);
    }

    public virtual void StartCombat(object sender, EventArgs e)
    {
        // this.block = 0;
        // this.DrawCard(this.handRefill);
        // // this.selectedActionGroup = this.hand[0];
        // this.mainResource.StartTurn();
    }

    protected virtual void CallOnDisplayValuesModified()
    {
        OnDisplayValuesModified?.Invoke(this, EventArgs.Empty);
    }
    protected virtual void CallOnCharacterDead()
    {
        OnCharacterDead?.Invoke(this);
    }
    protected virtual void CallOnDiscardShuffled(List<Card> discard)
    {
        OnDiscardShuffled?.Invoke(discard);
    }

    protected virtual void CallOnHandDisplayModified()
    {
        // Debug.Log("Hand Display modified event Called by "+name);
        OnHandDisplayModified?.Invoke(this, EventArgs.Empty);
    }
    protected virtual void CallOnCardDiscarded(Card CardToDiscard)
    {

        // Debug.Log("Hand Display modified event Called by "+name);
        OnCardDiscarded?.Invoke(CardToDiscard);
    }

    public virtual void RemoveBuff(Buff buffToRemove)
    {
        // Debug.Log("buff remover called on "+this.name);
        foreach (List<Buff> buffList in buffs)
        {
            if (buffList.Contains(buffToRemove))
            {
                buffList.Remove(buffToRemove);
                // Debug.Log("buff Removed");
            }
        }
    }
    public virtual void RemoveBuffs(List<Buff> buffsToRemove)
    {
        // Debug.Log("buff remover called on "+this.name);
        List<List<Buff>> newBuffs = new List<List<Buff>>();
        foreach (List<Buff> buffList in buffs)
        {
            newBuffs.Add(buffList.Except(buffsToRemove).ToList());
        }
        buffs = newBuffs;
    }
    public virtual void AddToGarbageBuffList(Buff buffToAddToGarbage)
    {
        // Debug.Log("add to garbage list called on "+this.name);
        garbageBuffList.Add(buffToAddToGarbage);
    }

    public virtual void EmptyGarbageBuffList()
    {
        // RemoveBuffs(garbageBuffList);
        // Debug.Log("empty garbage buff list");
        foreach (Buff buffToRemove in garbageBuffList)
        {
            RemoveBuff(buffToRemove);
        }
        garbageBuffList.Clear();
    }

    public virtual void Move(int columnNumber)
    {

    }

    public void ModifyFlatOffenses(int level, int value, DamageType damageType)
    {
        if (!offences.ContainsKey(damageType))
        {
            offences.Add(damageType, new OffenseModificator());
        }
        offences[damageType].ChangeFlatModificator(level, value);
    }

    public void TurnLeft()
    {
        facingDirection = FacingDirection.LEFT;
        CallOnDisplayValuesModified();
    }
    public void TurnRight()
    {
        facingDirection = FacingDirection.RIGHT;
        CallOnDisplayValuesModified();

    }
    public List<T> Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

}





public class NonPlayableCharacter : Character
{

    public NonPlayableCharacter(int maxHp,
       string name,
       Resource mainResource, CharacterModel characterModel) : base(maxHp, name, mainResource, characterModel)
    {
        EventManager.npcTurn += NpcTurn;
        TurnLeft();
    }

    public void NpcTurn(object sender, EventArgs e)
    {
        if (status != CharacterStatus.DEAD)
        {
            PlayActionGroup();
        }
    }

    public override void StartTurn(object sender, EventArgs e)
    {
        DrawCard(handRefill);
        if (this.hand.Count != 0)
        {
            this.selectedActionGroup = this.hand[0];
        }
        mainResource.StartTurn();
        focus = GameManager.mainPlayer;
        CallOnDisplayValuesModified();
    }

    public override void BeforeNpcTurn(object sender, EventArgs e)
    {
        this.block = 0;
        base.BeforeNpcTurn(sender, e);
    }

}

public class PlayableCharacter : Character
{

    public PlayableCharacter(int maxHp,
       string name,
       Resource mainResource, CharacterModel characterModel) : base(maxHp, name, mainResource, characterModel)
    {
        TurnRight();
    }

}