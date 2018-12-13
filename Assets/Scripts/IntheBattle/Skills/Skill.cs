using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour{


    int m_skillPlace = 0;
    bool m_attackORAssist;
    bool m_IsTargeting;
    int m_targetNum;
    int m_jumpFrame;
    int m_IAttackTime;
    int[] m_IStrikeFrame;

    public Character m_target;
    public Character m_user;

    public bool m_cooperation = false;
    public bool m_combo = false;

    public virtual void Activating(Character target, SkillManager.SkillType inputStyle_AorP)
    {
        m_target = target;
    }

    public virtual void Init()
    {

    }

    [System.Serializable]
    public class Damage : System.Object
    {
        public int damage;

        public void GiveDamage(Character target, Character user)
        {
            target.m_hp -= user.m_attack * damage;
            target.m_hpBar.Action();
        }
    }

}