using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Skill : MonoBehaviour{

    public Character m_target;
    public Character m_userCharacter;

    public virtual void Activating(Character target, bool inputStyle_AorP)
    {
        m_target = target;
    }
}
