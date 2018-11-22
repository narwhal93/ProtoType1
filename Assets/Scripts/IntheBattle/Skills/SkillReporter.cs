using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillReporter : MonoBehaviour {

    [SerializeField]
    int skill;

    [SerializeField]
    BattleManager m_battleManager;

    [SerializeField]
    SpriteRenderer[] m_otherSkills;

    Sprite m_black, m_orange;

    void Start() {
        m_black = Resources.Load<Sprite>("how big");
        m_orange = Resources.Load<Sprite>("SkillSelected");
    }

    void OnMouseDown()
    {
        if (skill == 1) m_battleManager.m_SKillSt = BattleManager.SkillState.Skill1;
        if (skill == 2) m_battleManager.m_SKillSt = BattleManager.SkillState.Skill2;
        if (skill == 3) m_battleManager.m_SKillSt = BattleManager.SkillState.Skill3;
        m_otherSkills[0].sprite = m_black;
        m_otherSkills[1].sprite = m_black;
        this.GetComponent<SpriteRenderer>().sprite = m_orange;
    }
}
