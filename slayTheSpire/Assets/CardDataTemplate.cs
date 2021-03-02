using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataTemplate : MonoBehaviour
{
    GameManager gameManager = GameManager.Instance;
    public Card card;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCard(Card card){
        this.card = card;
    }

    public void selectCard(){
        GameManager.mainPlayer.SelectCard(this.card);
        // Debug.Log(GameManager.mainPlayer.selectedCard.name);
    }
}
