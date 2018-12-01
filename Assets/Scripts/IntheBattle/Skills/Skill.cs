using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour{

    public Character m_target;
    public Character m_userCharacter;
    public Animator m_animator;

    public virtual void Activating(Character target)
    {
        Debug.Log("Skill's skill");
    }
}
