using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HandManager : MonoBehaviour
{
    private static HandManager _instance;
    public static HandManager Instance { get { return _instance; } }
    private Character mainPlayer;
    public GameObject cardPrefab;
    public GameObject abilityPrefab;
    public GameObject abilitiesContainer;
    int unselectedHandCardPosY = -350;
    // int selectedHandCardPosY = -225;
    private int cardWidth = 150;
    private int maxSpacing = 15;
    private int handCardStartingPosX = -500;
    private int handCardFinalPosX = 500;
    [SerializeField]
    private GameObject deckIcon;
    [SerializeField]
    private GameObject discardIcon;
    private float minTwist = 25f;
    private float maxTwist = -25f;
    private float scalingFactor = 2f;
    public static List<GameObject> instancedTemplates = new List<GameObject>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void Start()
    {
        mainPlayer = GameManager.mainPlayer;
        mainPlayer.OnHandDisplayModified += InstanceHand;
        mainPlayer.OnCardDiscarded += DisplayDiscardCard;
        mainPlayer.OnDiscardShuffled += DisplayDiscardShuffled;
        InstanceAbilities();
    }
    public void InstanceAbilities()
    {
        GameObject firstAbilityRow = abilitiesContainer.transform.GetChild(0).gameObject;
        foreach (Ability ability in mainPlayer.abilities)
        {
            // Debug.Log(ability.name);
            GameObject instancedAbility = Instantiate(abilityPrefab, deckIcon.transform.position, Quaternion.Euler(0, 0, 0));
            instancedAbility.transform.SetParent(firstAbilityRow.transform, false);
            AbilityDataTemplate abilityDataTemplate = instancedAbility.GetComponent<AbilityDataTemplate>();
            abilityDataTemplate.Set(ability);
            abilityDataTemplate.UpdateValues();
            instancedAbility.GetComponent<Button>().onClick.AddListener(delegate { abilityDataTemplate.Select(); });
            instancedTemplates.Add(instancedAbility);
        }


        // GameObject instancedCard = FindTemplateInstance(card);
        // if (instancedCard == null)
        // {
        // }
    }

    public void InstanceHand(object sender, EventArgs e)
    {
        // Debug.Log("Instance Hand Called");

        int handCardAmount = mainPlayer.hand.Count;
        int handCardSpacingX = (handCardFinalPosX - handCardStartingPosX) / (handCardAmount + 1);
        if (handCardSpacingX < cardWidth + maxSpacing)
        {
            handCardSpacingX = cardWidth + maxSpacing;
        }
        int handCardPosX = handCardStartingPosX + handCardSpacingX;
        int handCardPosY = unselectedHandCardPosY;
        // 20f for example, try various values
        float twistPerCard = (maxTwist - minTwist) / (handCardAmount + 1);
        float startTwist = twistPerCard + minTwist;

        // that should be roughly one-tenth the height of one
        // of your cards, just experiment until it works well

        foreach (Card card in mainPlayer.hand)
        {
            GameObject instancedCard = FindTemplateInstance(card);

            if (instancedCard == null)
            {
                instancedCard = InstantiateCard(card);
            }

            instancedCard.SetActive(true);
            float nudgeThisCard = Math.Abs(startTwist);
            nudgeThisCard *= scalingFactor;
            // nudgeThisCard = 0;  
            // Debug.Log(card.name);

            MoveInstancedCardToHand(instancedCard, new Vector3(handCardPosX, handCardPosY, 0));
            //         .OnComplete(GameManager.SetPlayingAnimationFalse);
            // GameManager.WaitForAnimation();
            // instancedCard.transform.localPosition = new Vector2(handCardPosX, -175f-nudgeThisCard);
            instancedCard.transform.rotation = Quaternion.Euler(0, 0, startTwist);

            handCardPosX += handCardSpacingX;
            startTwist += twistPerCard;
        }

        // if (mainPlayer.selectedActionGroup != null)
        // {
        //     mainPlayer.selectedActionGroup.MarkTemplateAsSelected();
        // }
    }
    public void MoveInstancedCardToHand(GameObject instancedCard, Vector3 position)
    {
        CardDataTemplate cardDataTemplate = instancedCard.GetComponent<CardDataTemplate>();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(instancedCard.transform.DOLocalMove(position, 1));
        sequence.Insert(0, instancedCard.transform.DOScale(new Vector3(1f, 1f, 1f), 1));
        cardDataTemplate.AddAnimationToQueue(sequence);
    }
    public void DisplayDiscardCard(Card cardToDiscard)
    {
        // Debug.Break();
        GameObject instancedCard = FindTemplateInstance(cardToDiscard);
        if (instancedCard != null)
        {
            MoveInstancedCardToDiscard(instancedCard);
        }
    }

    public void MoveInstancedCardToDiscard(GameObject instancedCard)
    {
        CardDataTemplate cardDataTemplate = instancedCard.GetComponent<CardDataTemplate>();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(instancedCard.transform.DOMove(discardIcon.transform.position, 1));
        sequence.Insert(0, instancedCard.transform.DOScale(new Vector3(0f, 0f, 0f), 1));
        cardDataTemplate.AddAnimationToQueue(sequence, true);

    }
    public void DisplayDiscardShuffled(List<Card> cardsToShuffleInDeck)
    {
        foreach (Card card in cardsToShuffleInDeck)
        {
            GameObject instancedCard = FindTemplateInstance(card);
            MoveInstancedCardToDeck(instancedCard);
        }
    }
    public void MoveInstancedCardToDeck(GameObject instancedCard)
    {
        CardDataTemplate cardDataTemplate = instancedCard.GetComponent<CardDataTemplate>();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(instancedCard.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f));
        sequence.Append(instancedCard.transform.DOScale(new Vector3(0f, 0f, 0f), 0.5f));
        sequence.Insert(0, instancedCard.transform.DOMove(deckIcon.transform.position, 1));
        cardDataTemplate.AddAnimationToQueue(sequence, true);
        // sequence.Insert(0,instancedCard.transform.DOScale(new Vector3(0f, 0f, 0f),0.5f));
        // This log will happen after the tween has completed
        // instancedCard.SetActive(false);
        // Debug.Log("Tween completed!");
    }
    public GameObject InstantiateCard(Card card)
    {
        GameObject instancedCard = FindTemplateInstance(card);
        if (instancedCard == null)
        {
            instancedCard = Instantiate(cardPrefab, deckIcon.transform.position, Quaternion.Euler(0, 0, 0));
            instancedCard.transform.SetParent(GameManager.canvasTransform, false);
            // instancedCard.transform.position = new Vector2(handCardPosX, handCardPosY-nudgeThisCard);
            instancedCard.AddComponent<CardDataTemplate>();
            CardDataTemplate cardDataTemplate = instancedCard.GetComponent<CardDataTemplate>();
            cardDataTemplate.Set(card);
            // Debug.Log("Set card called");
            instancedCard.tag = "HandCard";
            // Debug.Log(instancedCard.transform.Find("name").gameObject.GetComponent<UnityEngine.UI.Text>().text);
            cardDataTemplate.UpdateValues();
            instancedCard.GetComponent<Button>().onClick.AddListener(delegate { cardDataTemplate.Select(); });
            instancedTemplates.Add(instancedCard);
        }
        return instancedCard;
    }
    public static GameObject FindTemplateInstance(ActionGroup actionGroup)
    {
        if (actionGroup != null)
        {
            foreach (GameObject instancedTemplate in instancedTemplates)
            {
                if (instancedTemplate.GetComponent<ActionGroupDataTemplate>().actionGroup == actionGroup)
                {
                    return instancedTemplate;
                }
            }
        }
        return null;
    }

    public static void UnmarkTemplateAsUnselected(ActionGroup actionGroup)
    {
        GameObject instancedTemplate = FindTemplateInstance(actionGroup);
        if (instancedTemplate != null)
        {
            instancedTemplate.GetComponent<ActionGroupDataTemplate>().UnmarkTemplateAsUnselected();
        }
    }

}

