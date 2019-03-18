using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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

    public enum BuffTiming
    {
        Start = 0,
        Attack,
        End,
        Wound,
        Passive
    }

    delegate void BuffActivating();
    delegate float BuffDmgActivating(float damage);

    BuffActivating Action;
    BuffDmgActivating DmgAction;

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
            case BuffType.Nothing:
                {
                    m_actInStart = true;
                    m_actInAttack = true;
                    m_actInEnd = true;
                    m_actInWound = true;
                    Action = new BuffActivating(Nothing);
                    DmgAction = new BuffDmgActivating(Nothing);
                    break;
                }

            case BuffType.Electrocuted:
                {
                    m_actInStart = false;
                    m_actInAttack = false;
                    m_actInEnd = false;
                    m_actInWound = true;
                    Action = new BuffActivating(ActElectrocuted);
                    DmgAction = new BuffDmgActivating(Nothing);
                    break;
                }

            case BuffType.Stun:
                {
                    m_actInStart = false;
                    m_actInAttack = false;
                    m_actInEnd = false;
                    m_actInWound = false;
                    Action = new BuffActivating(ActStun);
                    DmgAction = new BuffDmgActivating(Nothing);
                    break;
                }

            case BuffType.Sleep:
                {
                    m_actInStart = false;
                    m_actInAttack = false;
                    m_actInEnd = false;
                    m_actInWound = true;
                    Action = new BuffActivating(ActSleep);
                    DmgAction = new BuffDmgActivating(Nothing);
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

    public void RemoveBuff()
    {
        m_user = null;
        m_target = null;
        m_extraParam = 0;
        m_type = BuffType.Nothing;
        BuffManager.Instance.Buffs.Push(this);
        this.gameObject.SetActive(false);
    }

    public void Activator(BuffTiming timing)
    {
        switch (timing)
        {
            case BuffTiming.Start:
                {
                    if (m_actInStart)
                    {
                        Action();
                        m_durationLeft -= 1;
                    }
                    break;
                }
            case BuffTiming.Attack:
                {
                    if (m_actInAttack)
                    {
                        Action();
                        m_durationLeft -= 1;
                    }
                    break;
                }
            case BuffTiming.End:
                {
                    if (m_actInEnd)
                    {
                        Action();
                        m_durationLeft -= 1;
                    }
                    break;
                }
            case BuffTiming.Wound:
                {
                    if (m_actInWound)
                    {
                        Action();
                        m_durationLeft -= 1;
                    }
                    break;
                }
        }
        
    }

    public float DmgActivator(BuffTiming timing, float damage)
    {
        switch (timing)
        {
            case BuffTiming.Start:
                {
                    if (m_actInStart) return DmgAction(damage);
                    break;
                }
            case BuffTiming.Attack:
                {
                    if (m_actInAttack) return DmgAction(damage);
                    break;
                }
            case BuffTiming.End:
                {
                    if (m_actInEnd) return DmgAction(damage);
                    break;
                }
            case BuffTiming.Wound:
                {
                    if (m_actInWound) return DmgAction(damage);
                    break;
                }
        }
        return 0;
    }

    public void Nothing()
    {

    }

    public float Nothing(float damage)
    {
        return damage;
    }

    void ActElectrocuted()
    {
        m_user.m_hp -= m_extraParam;
        m_user.m_hpBar.Action();
        return;
    }

    void ActStun()
    {
        return;
    }

    void ActSleep()
    {
        return;
    }

}