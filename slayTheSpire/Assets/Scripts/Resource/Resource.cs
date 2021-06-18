using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Resource
{
    protected string name;
    protected int currentAmount;
    protected int maxAmount;
    protected int initialValue;
    protected int gain;
    // public ResourceType mainResourceType;
    public int CurrentResourceAmount() { return this.currentAmount; }
    public int MaxResourceAmount() { return this.maxAmount; }
    public virtual void StartCombat() { return; }
    public virtual void StartTurn() { return; }
    public virtual void EndCombat() { return; }
    public virtual void EndTurn() { return; }
    
    public event EventHandler OnResourceCurrentAmountModified; 

    protected virtual void CallOnResourceCurrentAmountModified(){
        OnResourceCurrentAmountModified?.Invoke(this,EventArgs.Empty);
    }
    public virtual bool UseResource(int amountToUse)
    {
        if (currentAmount >= amountToUse)
        {
            currentAmount -= amountToUse;
            return true;
        }
        else
        {
            return false;
        }
    }
     public virtual bool CheckEnoughResource(int amountToUse)
    {
        if (currentAmount >= amountToUse)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

public class Energy : Resource
{

    public Energy(int mainResourceAmount)
    {
        this.name = "Energy";
        this.currentAmount = mainResourceAmount;
        this.initialValue = mainResourceAmount;
        this.gain = mainResourceAmount;
        this.maxAmount = mainResourceAmount;
        // this.mainResourceType = ResourceType.ENERGY;
    }

    public override void StartCombat()
    {
    }
    public override void StartTurn()
    {
        this.currentAmount = this.gain;
    }
    public override void EndCombat()
    {
    }
    public override void EndTurn()
    {
    }
    

}
public class Faith : Resource
{

    public Faith()
    {
        this.name = "Faith";
        this.currentAmount = 50;
        this.initialValue = 50;
        this.gain = 0;
        // this.mainResourceType = ResourceType.FAITH;

    }
    public override void StartCombat()
    {
    }
    public override void StartTurn()
    {
    }
    public override void EndCombat()
    {
    }
    public override void EndTurn()
    {

    }


}