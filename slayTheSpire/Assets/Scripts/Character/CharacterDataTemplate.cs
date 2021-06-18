using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataTemplate : MonoBehaviour
{
    // Start is called before the first frame update
    public Character character;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCharacter(Character character){
        this.character = character;
        character.OnDisplayValuesModified += CharacterDisplayValuesModified;
        UpdateText();
    }

    public void UpdateText(){
        this.transform.Find("name").gameObject.GetComponent<UnityEngine.UI.Text>().text = character.name;
        this.transform.Find("currentHp").gameObject.GetComponent<UnityEngine.UI.Text>().text = character.currentHp.ToString();
        this.transform.Find("maxHp").gameObject.GetComponent<UnityEngine.UI.Text>().text = character.maxHp.ToString();
        this.transform.Find("block").gameObject.GetComponent<UnityEngine.UI.Text>().text = "("+character.block.ToString()+")";
        this.transform.Find("currentAmountResource").gameObject.GetComponent<UnityEngine.UI.Text>().text = character.mainResource.CurrentResourceAmount().ToString();
        this.transform.Find("maxAmountResource").gameObject.GetComponent<UnityEngine.UI.Text>().text = character.mainResource.MaxResourceAmount().ToString();
        if (character.selectedActionGroup != null)
        {
            this.transform.Find("cardName").gameObject.GetComponent<UnityEngine.UI.Text>().text = character.selectedActionGroup.name;
        }
        else
        {
            this.transform.Find("cardName").gameObject.GetComponent<UnityEngine.UI.Text>().text = "No card selected";
        }
        if (character.focus != null)
        {
            this.transform.Find("focusName").gameObject.GetComponent<UnityEngine.UI.Text>().text = character.focus.name;
        }
        else
        {
            this.transform.Find("focusName").gameObject.GetComponent<UnityEngine.UI.Text>().text = "No focus";
        }

        Vector3 localScale = this.transform.Find("characterSprite").transform.localScale;
        if (character.facingDirection == FacingDirection.LEFT)
        {
            localScale = new Vector3(-Math.Abs(localScale.x),
            localScale.y,
            localScale.z
            );
        }else
        {
            localScale = new Vector3(Math.Abs(localScale.x),
            localScale.y,
            localScale.z
            );
        }
        this.transform.Find("characterSprite").transform.localScale = localScale;
    }

    public void focusCharacter(){
        GameManager.mainPlayer.SetFocus(this.character);
        // Debug.Log(GameManager.mainPlayer.focus.name);
    }

    public void CharacterDisplayValuesModified(object sender, EventArgs e){
        if (character.status == CharacterStatus.DEAD)
        {
            Animator animator = this.transform.Find("characterSprite").GetComponent<Animator>();
            animator.SetBool("dead",true);
            
        }
        UpdateText();
    }
}
