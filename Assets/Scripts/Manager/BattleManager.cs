using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {


    [SerializeField]
    CharManager m_charManager;

    [SerializeField]
    TimeBarManager m_timeBarManager;

    Character m_target;
    Character m_user;
    public Skill[] m_skills;

    List<Character> m_charOnAction;

    enum BattleState {
        battleWaiting = 0,
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
        for (int i = 0; i < m_skills.Length; i++)
        {
            m_skills[i].m_userCharacter = m_user;
        }
    }

    public void MyTurn(Character character)
    {
        m_user = character;
        m_battleSt = BattleState.battleThinking;
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

    public void CharacterClicked(string name)
    {
        if (m_battleSt == BattleState.battleThinking)
        {
            ChangeTarget(name);
            SetSkillUser();
            UseSkill();
        }
    }

    public void OnActionList(Character character)
    {
        m_charOnAction.Add(character);
    }

	// Use this for initialization
	void Start () {
        m_charOnAction = new List<Character>();
        m_battleSt = BattleState.battleWaiting;
        m_SKillSt = SkillState.Skill1;
        m_skills = new Skill[3]; 
        m_target = null;
	}
	
	// Update is called once per frame
	void Update () {
		switch(m_battleSt)
        {
            case BattleState.battleWaiting:
                {
                    if (m_charOnAction.Count >= 1)
                    {
                        {
                            MyTurn(m_charOnAction[m_charOnAction.Count-1]);
                        } 
                        break;
                    }
                    for (int i = 0; i < m_charManager.m_team1.Count; i++)
                    {
                        m_charManager.m_team1[i].m_action += m_charManager.m_team1[i].m_speed * Time.deltaTime * 0.1f;
                        if (m_charManager.m_team1[i].m_action >= 100f) m_charOnAction.Add(m_charManager.m_team1[i]);
                        
                    }
                    for (int i = 0; i < m_charManager.m_team2.Count; i++)
                    {
                        m_charManager.m_team2[i].m_action += m_charManager.m_team2[i].m_speed * Time.deltaTime * 0.1f;
                        if (m_charManager.m_team2[i].m_action >= 100f) m_charOnAction.Add(m_charManager.m_team2[i]);
                    }
                    for (int i = 0; i < m_timeBarManager.m_barIconList.Count; i++)
                    {
                        m_timeBarManager.m_barIconList[i].Action();
                    }
                    break;
                }
            case BattleState.battleThinking:
                {
                    break;
                }
            case BattleState.battleShowing:
                {
                    break;
                }
            case BattleState.battleEnd:
                {
                    break;
                }
        }
	}
}
