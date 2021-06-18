using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class CharacterModelLibrary
{

    public static CharacterModel KnightCharacterModel()
    {   
        RuntimeAnimatorController characterAnimatorControler = (RuntimeAnimatorController)Resources.Load("Prefabs/Characters/Knight/KnightController");
        CharacterModel model = new CharacterModel(characterAnimatorControler);
        return model;
    }
    public static CharacterModel GoblinCharacterModel()
    {   
        RuntimeAnimatorController characterAnimatorControler = (RuntimeAnimatorController)Resources.Load("Prefabs/Characters/Goblin/GoblinController");
        CharacterModel model = new CharacterModel(characterAnimatorControler);
        return model;
    }
    
        

}