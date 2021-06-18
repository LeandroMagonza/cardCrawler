using System.Collections;
using System.Collections.Generic;
 using UnityEngine.UI;
using UnityEngine;

public class AbilityDataTemplate : ActionGroupDataTemplate
{
    // public Ability ability;

    public override void UpdateValues(){
        // Debug.Log("updating ability values");
        this.transform.Find("Icon").gameObject.GetComponent<Image>().sprite = this.actionGroup.icon;
        // this.transform.GetComponent<Image>().sprite = this.actionGroup.icon;

        // this.transform.Find("name").gameObject.GetComponent<UnityEngine.UI.Text>().text = this.card.name;
        // this.transform.Find("type").gameObject.GetComponent<UnityEngine.UI.Text>().text = this.card.GetCardType();
        // this.transform.Find("resourceCost").gameObject.GetComponent<UnityEngine.UI.Text>().text = this.card.resourceCost.GetResourceCostDisplay(GameManager.mainPlayer);
        // this.transform.Find("description").gameObject.GetComponent<UnityEngine.UI.Text>().text = this.card.description;
    }
    public override void MarkTemplateAsSelected(){ 
        // Debug.Log("selected ability");
        GetComponent<Image>().color = Color.red;
        }
    public override void UnmarkTemplateAsUnselected(){ 
        // Debug.Log("Ability Deselected");
        GetComponent<Image>().color = Color.white;
        }
}
