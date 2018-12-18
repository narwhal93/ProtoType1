using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : SingletonMonoBehaviour<BattleManager> {


    [SerializeField]
    Character m_target;
    [SerializeField]
    Character m_myTurn;

    List<Character> m_charOnAction;

    public enum BattleState {
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

    public BattleState m_battleSt;
    public SkillState m_SKillSt;



    //response
    public void CharacterClicked(Character target)
    {
        Debug.Log("??");
        ChangeTarget(target);
    }

    public void ChangeTarget(Character target)
    {
        m_target = target;
    }

    //Related to turn
    public void MyTurn(Character character)
    {
        m_myTurn = character;
        m_battleSt = BattleState.battleThinking;
        m_myTurn.m_animation.AnimationName = "stand_ready";
    }

    public void OnActionList(Character character)
    {
        m_charOnAction.Add(character);
    }



    // ActiveSKill
    public void ActivateSkill() 
    {
        m_myTurn.m_skills[(int)m_SKillSt].Activating(m_target, SkillManager.SkillType.Active);
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
                        for (int i = 0; i < CharManager.Instance.m_teamChar[j].Count; i++)
                        {
                            CharManager.Instance.m_teamChar[j][i].m_action += CharManager.Instance.m_teamChar[j][i].m_speed * Time.deltaTime * 0.3f;
                            if (CharManager.Instance.m_teamChar[j][i].m_action >= 100f) m_charOnAction.Add(CharManager.Instance.m_teamChar[j][i]);

                        }
                    }
                    for (int i = 0; i < TimeBarManager.Instance.m_barIconList.Count; i++)
                    {
                        TimeBarManager.Instance.m_barIconList[i].Action();
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
