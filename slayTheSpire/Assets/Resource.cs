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
    public int CurrentResourceAmount() { return this.currentAmount; }
    public int MaxResourceAmount() { return this.maxAmount; }
    public virtual void StartCombat() { return; }
    public virtual void StartTurn() { return; }
    public virtual void EndCombat() { return; }
    public virtual void EndTurn() { return; }
    public virtual bool UseResource(int amount) { return false; }

}

public class Energy : Resource
{

    public Energy()
    {
        this.name = "Energy";
        this.currentAmount = 3;
        this.initialValue = 3;
        this.gain = 3;
        this.maxAmount = 3;
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
    public override bool UseResource(int amountToUse)
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

}
public class Faith : Resource
{

    public Faith()
    {
        this.name = "Faith";
        this.currentAmount = 50;
        this.initialValue = 50;
        this.gain = 0;

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
    public override bool UseResource(int amountToUse)
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

}