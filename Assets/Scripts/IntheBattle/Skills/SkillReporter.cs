using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillReporter : MonoBehaviour {

    [SerializeField]
    int m_skill;
    

    [SerializeField]
    BattleManager m_battleManager;

    [SerializeField]
    SpriteRenderer[] m_otherSkills;

    SpriteRenderer m_thisSkill;

    void Start() {
        m_thisSkill = this.GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        if (m_skill == 1) m_battleManager.m_SKillSt = BattleManager.SkillState.Skill1;
        if (m_skill == 2) m_battleManager.m_SKillSt = BattleManager.SkillState.Skill2;
        if (m_skill == 3) m_battleManager.m_SKillSt = BattleManager.SkillState.Skill3;

        m_otherSkills[0].color = Color.black;
        m_otherSkills[1].color = Color.black;
        m_thisSkill.color = new Color(0.9f,0.5f, 0,1);

    }
}
