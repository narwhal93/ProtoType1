using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public string m_index { get; set; }
    public string m_name { get; set; }
    public string m_portraitName { get; set; }
    public string m_icon { get; set; }
    public enum m_charClass
    {
        nothing = 0,
        warrior,
        supporter
    }

    public string m_star;
    public int m_level;
    public float m_hp;
    public float m_attack;
    public float m_defense;
    public float m_speed;
    public float m_critChance;
    public float m_critDmg;
    public float m_ccChance;
    public float m_ccResist;
    public float m_coopChance;
    public float m_comboChance;


    public void SetData() {


    }

    private void Start()
    {
        m_level = 10;
    }
}
