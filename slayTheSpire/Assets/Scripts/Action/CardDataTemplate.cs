using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataTemplate : ActionGroupDataTemplate
{
    public override void UpdateValues(){
        // Debug.Log(this.actionGroup.name);
        this.transform.Find("name").gameObject.GetComponent<UnityEngine.UI.Text>().text = this.actionGroup.name;
        this.transform.Find("type").gameObject.GetComponent<UnityEngine.UI.Text>().text = this.actionGroup.GetType();
        this.transform.Find("mainResourceCost").gameObject.GetComponent<UnityEngine.UI.Text>().text = this.actionGroup.mainResourceCost.GetResourceCostDisplay(GameManager.mainPlayer);
        this.transform.Find("description").gameObject.GetComponent<UnityEngine.UI.Text>().text = this.actionGroup.description;
    }
    public override void MarkTemplateAsSelected(){ 
        transform.DOLocalMoveY(-225, 0.5f);
        transform.SetAsLastSibling();
    }
    public override void UnmarkTemplateAsUnselected(){ 
        transform.DOLocalMoveY(-350, 0.5f);
    }
    
}
