using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    Character m_target;
    Character m_user;

    [SerializeField]
    CharManager m_charManager;

    public Skill[] m_skills;

    enum BattleState {
        battleThinking,
        battleShowing,
        battleEnd
    }

    public enum SkillState {
        Skill1 = 0,
        Skill2,
        Skill3
    }

    BattleState m_battleSt;
    public SkillState m_SKillSt;

    public void ChangeTarget(string name)
    {
        if (name[0] == 1) m_target = m_charManager.m_team1[name[1]];
        else m_target = m_charManager.m_team2[name[2]];
    }

    public void SetSkillUser()
    {
        for (int i = 0; i<m_skills.Length; i++)
        {
            m_skills[i].m_userCharacter = m_user;
        }
    }

    public void CharacterClicked(string name)
    {
        ChangeTarget(name);
        SetSkillUser();
        UseSkill();
    }

    public void UseSkill()
    {
        switch (m_SKillSt)
        {
            case SkillState.Skill1:
                {
                    m_skills[0].Activating(m_target);
                    break;
                }
            case SkillState.Skill2:
                {
                    m_skills[1].Activating(m_target);
                    break;
                }
            case SkillState.Skill3:
                {
                    m_skills[2].Activating(m_target);
                    break;
                }
        }
    }

	// Use this for initialization
	void Start () {
        m_battleSt = BattleState.battleThinking;
        m_SKillSt = SkillState.Skill1;
        m_skills = new Skill[3]; 
        m_target = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
