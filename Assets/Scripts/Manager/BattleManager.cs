using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {


    [SerializeField]
    CharManager m_charManager;

    [SerializeField]
    TimeBarManager m_timeBarManager;

    [SerializeField]
    Character m_target;

    [SerializeField]
    Character m_myTurn;

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

    public void ChangeTarget(Character target)
    {
        m_target = target;
    }

    public void MyTurn(Character character)
    {
        m_myTurn = character;
        m_battleSt = BattleState.battleThinking;
    }

    public void CharacterClicked(Character target)
    {
        ChangeTarget(target);
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
                            int temp = Random.Range(0, m_charOnAction.Count - 1);
                            MyTurn(m_charOnAction[temp]);
                            m_charOnAction.Remove(m_charOnAction[temp]);
                        } 
                        break;
                    }
                    for (int j = 0; j < 2; j++) {
                        for (int i = 0; i < m_charManager.m_teamChar[j].Count; i++)
                        {
                            m_charManager.m_teamChar[j][i].m_action += m_charManager.m_teamChar[j][i].m_speed * Time.deltaTime * 0.1f;
                            if (m_charManager.m_teamChar[j][i].m_action >= 100f) m_charOnAction.Add(m_charManager.m_teamChar[j][i]);

                        }
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
