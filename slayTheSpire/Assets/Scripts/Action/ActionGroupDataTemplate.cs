using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenAnimation {
    public Tween tween;
    public bool disableGameObjectAfter;

    public TweenAnimation(Tween tween, bool disableGameObjectAfter){
        this.tween = tween;
        this.disableGameObjectAfter = disableGameObjectAfter;
    }
}
public class ActionGroupDataTemplate : MonoBehaviour
{
    public ActionGroup actionGroup;

    public Queue<TweenAnimation> animationQueue = new Queue<TweenAnimation>();
    public bool playingAnimation = false;

    public void Update(){
        if (!playingAnimation && animationQueue.Count > 0){
            StartCoroutine(PlayNextAnimation());
        }    
    }
    IEnumerator PlayNextAnimation(){
        GameObject templateInstance = HandManager.FindTemplateInstance(actionGroup);
        // templateInstance.SetActive(true);
        playingAnimation = true;
        TweenAnimation tweenAnimation = animationQueue.Dequeue();
        tweenAnimation.tween.Play();
        yield return tweenAnimation.tween.WaitForCompletion();
        playingAnimation = false;
        // templateInstance.SetActive(!tweenAnimation.disableGameObjectAfter);
    }
    public void AddAnimationToQueue(Tween animation,bool disableGameObjectAfter = false)
    {
        TweenAnimation tweenAnimation = new TweenAnimation(animation.Pause(),disableGameObjectAfter);
        animationQueue.Enqueue(tweenAnimation);
        // Debug.Log("add" + this.GetInstanceID().ToString());
    }
    public virtual void Set(ActionGroup actionGroup){
        this.actionGroup = actionGroup;
    }

    public virtual void Select(){
        GameManager.mainPlayer.Select(this.actionGroup);
        MarkTemplateAsSelected();
        // Debug.Log(GameManager.mainPlayer.selectedActionGroup.name);
    }

    public virtual void UpdateValues(){
        // this.transform.Find("name").gameObject.GetComponent<UnityEngine.UI.Text>().text = this.card.name;
        // this.transform.Find("type").gameObject.GetComponent<UnityEngine.UI.Text>().text = this.card.GetCardType();
        // this.transform.Find("mainResourceCost").gameObject.GetComponent<UnityEngine.UI.Text>().text = this.card.mainResourceCost.GetResourceCostDisplay(GameManager.mainPlayer);
        // this.transform.Find("description").gameObject.GetComponent<UnityEngine.UI.Text>().text = this.card.description;
    }
    public virtual void MarkTemplateAsSelected(){ 
        
        
    }
    public virtual void UnmarkTemplateAsUnselected(){ 
        
        
    }
}
