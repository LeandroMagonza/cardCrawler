using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class ActionGroupIconLibrary
{

    public static Sprite ArrowLeft()
    {   
        // File.Exists("Assets/Resources/Characters/1/1.obj)"
        return (Sprite)Resources.Load("Prefabs/Cards_Abilities/Sprites/ArrowLeft",typeof(Sprite));
    }
    public static Sprite ArrowRight()
    {   
        return (Sprite)Resources.Load("Prefabs/Cards_Abilities/Sprites/ArrowRight",typeof(Sprite));
    }
    public static Sprite Empty()
    {   
        return (Sprite)Resources.Load("Prefabs/Cards_Abilities/Sprites/Empty",typeof(Sprite));
    }
        

}