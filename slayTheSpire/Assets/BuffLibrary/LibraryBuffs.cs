using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class BuffLibrary
{

    public static TemporaryBuff Vulnerable()
    {   
        Action action = new Action(new SelfTarget(),new LoseFlatHealthEffect(6));
        return new TemporaryBuff("Vulnerable", action, 2);
    }

    public static StackableBuff Poison()
    {   
        Action action = new Action(new SelfTarget(),new PoisonEffect(7));
        return new StackableBuff("Poison", action);
    }
}