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

    bool m_actInStart;
    bool m_actInAttack;
    bool m_actInEnd;
    bool m_actInWound;

    delegate void BuffActivating(float damage);

    BuffActivating Action;

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

    public void Init()
    {
        switch (m_type)
        {
            case BuffType.Electrocuted:
                {
                    m_actInStart = false;
                    m_actInAttack = false;
                    m_actInEnd = false;
                    m_actInWound = true;
                    Action = new BuffActivating(ActElectrocuted);
                    break;
                }

            case BuffType.Stun:
                {
                    m_actInStart = false;
                    m_actInAttack = false;
                    m_actInEnd = false;
                    m_actInWound = false;
                    Action = new BuffActivating(ActStun);
                    break;
                }

            case BuffType.Sleep:
                {
                    m_actInStart = false;
                    m_actInAttack = false;
                    m_actInEnd = false;
                    m_actInWound = true;
                    break;
                }
        }
    }

    public void SetTarget(Character target)
    {
        m_target = target;
    }

    public void SetUser(Character user)
    {
        m_user = user;
    }

    public void Move(int Count)
    {
        this.gameObject.transform.position = m_user.gameObject.transform.position + new Vector3(-50 + 15*(Count%5), +165 + 15f*(Count/5), 0);
    }


    public void Activator()
    {
        Action(0);
    }

    public void Activator(float damage)
    {
        Action(damage);
    }

    void ActElectrocuted(float damage)
    {
        return;
    }

    void ActStun(float damage)
    {
        return;
    }

    void ActSleep()
    {
        return;
    }

}
