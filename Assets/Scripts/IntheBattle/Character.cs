using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Character : MonoBehaviour {

    [SerializeField]
    public Skill[] m_skills;

    [SerializeField]
    public List<Buff> m_buff;

    public SkeletonAnimation m_animation;

    public BarIcons m_barIcon;
    public HpBar m_hpBar;

    public int m_index { get; set; }
    public string m_name { get; set; }
    public string m_portraitName { get; set; }
    public string m_elementSymbol { get; set; }
    public enum m_charClass
    {
        nothing = 0,
        warrior,
        supporter
    }
    public m_charClass m_class;
    public int m_star;
    public int m_maxLevel;
    public float m_maxHp;
    public float m_attack;
    public float m_defense;
    public float m_speed;
    public float m_critChance;
    public float m_critDmgRatio;
    public float m_ccChance;
    public float m_ccResist;
    public float m_coopChance;
    public float m_comboChance;

    public float m_action;
    public float m_hp;
    public bool m_side; // true = left, false = right

    public void MoveBuff()
    {
        for (int i = 0; i < m_buff.Count; i++)
        {
            m_buff[i].Move(i);
        }
    }

    public void ActivateBuff(Buff.BuffType buffType)
    {
        for (int i = 0; i < m_buff.Count; i++)
        {
            m_buff[i].Activator();
        }
    }

    public void ActivateBuff(Buff.BuffType buffType, float damage)
    {
        for (int i = 0; i < m_buff.Count; i++)
        {
            m_buff[i].Activator(damage);
        }
    }

    public void ResetData() {

    }

    void OnMouseDown()
    {
        Debug.Log("TargetClicked");
        BattleManager.Instance.CharacterClicked(this);
    }

   

}
