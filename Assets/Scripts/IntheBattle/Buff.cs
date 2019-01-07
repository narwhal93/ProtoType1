using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Buff : MonoBehaviour{

    [SerializeField]
    Character m_target;

    [SerializeField]
    Character m_user;

    public float m_extraParam;

    public int m_durationLeft;

    public enum ActionTiming
    {
        GiveDamage = 0,
        GetDamage,
        StartTurn,
        EndTrun
    }

    public ActionTiming m_tim;

    public enum BuffType
    { 
        Nothing = 0,
        Electrocuted,
        Stun,
        Sleep,
        Provoke,
        Bleed,
        ActionGage,
        Slow,
        Frozen,
        Weaponbreak,
        Armorbreak,
        Knockback,
        Fear,
        Mad,
        Fascinated,
        Cursed,
        Execution,
        Remove,
        Reflection
    }

    public BuffType m_type;

    public void SetTarget(Character target)
    {
        m_target = target;
    }

    public void SetUser(Character user)
    {
        m_user = user;
    }

    public void Action(ActionTiming timing)
    {
        if (m_tim == timing)
        {
            
        }
    }

    public void Move(int Count)
    {
        this.gameObject.transform.position = m_user.gameObject.transform.position + new Vector3(-50 + 15*(Count%5), +165 + 15f*(Count/5), 0);
    }


}
