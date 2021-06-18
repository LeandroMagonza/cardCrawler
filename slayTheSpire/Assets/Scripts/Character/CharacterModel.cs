using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel {
    public RuntimeAnimatorController characterAnimatorControler;

    public CharacterModel(RuntimeAnimatorController newController){
        characterAnimatorControler = newController;
    }
}