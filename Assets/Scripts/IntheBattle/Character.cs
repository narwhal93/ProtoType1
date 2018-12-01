using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField]
    public Skill[] m_skills;

    public BattleManager m_battleManager;

    public BarIcons m_barIcon;
    public HpBar m_hpBar;

    public int m_index { get; set; }
    public string m_name { get; set; }
    public string m_portraitName { get; set; }
    public string m_elementSymbol {get; set; }
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

    public void ResetData() {

    }

    void OnMouseDown()
    {
        m_battleManager.CharacterClicked(this);
        Debug.Log("?");
    }

}
