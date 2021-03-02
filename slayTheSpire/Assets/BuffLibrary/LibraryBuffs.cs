using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class BuffLibrary
{

    public static TemporaryBuff Vulnerable()
    {   
        Action action = new Action(new SelfTarget(),new TrueDamageEffect(6));
        return new TemporaryBuff("Vulnerable", action, 2);
    }
}