using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class EndOfExecutionProcess
{
    public virtual void ExecuteEndOfExecutionProcess(Character owner, ActionGroup actionGroupPlayed, int mainResourceCostUsed = 0)
    {
        return;
    }
}
public abstract class EndOfExecutionProcessCard : EndOfExecutionProcess
{
    public override void ExecuteEndOfExecutionProcess(Character owner, ActionGroup actionGroupPlayed, int mainResourceCostUsed = 0)
    {
        return;
    }
}

public class AddToDiscardEndOfExecutionProcess : EndOfExecutionProcessCard 
{
     public override void ExecuteEndOfExecutionProcess(Character owner, ActionGroup actionGroupPlayed, int mainResourceCostUsed = 0)
    {
        owner.DiscardCard((Card)actionGroupPlayed);
        return;
    }
}