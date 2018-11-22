using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour{

    public Character target;
    public Character m_userCharacter;


    public virtual void Activating(Character target)
    {
        Debug.Log("Skill's skill");
    }
}
