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
        this.transform.Find("currentAmountResource").gameObject.GetComponent<UnityEngine.UI.Text>().text = character.resource.CurrentResourceAmount().ToString();
        this.transform.Find("maxAmountResource").gameObject.GetComponent<UnityEngine.UI.Text>().text = character.resource.MaxResourceAmount().ToString();
    }

    public void focusCharacter(){
        GameManager.mainPlayer.SetFocus(this.character);
        // Debug.Log(GameManager.mainPlayer.focus.name);
    }

    public void CharacterDisplayValuesModified(object sender, EventArgs e){
        // Debug.Log(character.name);
        UpdateText();
    }
}
