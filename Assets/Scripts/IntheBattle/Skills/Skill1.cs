using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1 : Skill {

    [SerializeField]
    SkillManager.SkillType m_SkillType = SkillManager.SkillType.Active;

    [SerializeField]
    int m_skillPlace = 0;
    [SerializeField]
    bool m_activeORPassive = true;
    [SerializeField]
    bool m_attackORAssist = true; 
    public bool m_cooperation = false;
    public bool m_compbo = false;
    [SerializeField]
    bool m_IsTargeting = true;
    [SerializeField]
    int m_targetNum = 1;
    [SerializeField]
    int m_jumpFrame = 40;
    object[] m_move;
    [SerializeField]
    int m_IAttackTime = 40;  // For Designer
    object m_attackTime;
    [SerializeField]
    int[] m_IStrikeFrame = { 35, 90 };  // For Designer
    object[] m_strikeFrame;


  //  int[] m_skrikeIndex = { 1, 0 };
   // [SerializeField]
  //  DoSomething[] m_StrikeIndex = { };

    // Activating -> Jump -> attack -> strike or hit -> Jump back

    public override void Activating(Character target, bool inputStyle_AorP)
    {
        // For Designer. 
        m_attackTime = m_IAttackTime;
        m_strikeFrame = new object[m_IStrikeFrame.Length];
        for (int i = 0; i < m_IStrikeFrame.Length; i++)
        {
            m_strikeFrame[i] = m_IStrikeFrame[i];
        }

        base.Activating(target, inputStyle_AorP);
        if (m_activeORPassive == inputStyle_AorP)
        {
            if (m_attackORAssist ^ (m_userCharacter.m_side == m_target.m_side))
            {
                BattleManager.Instance.m_battleSt = BattleManager.BattleState.battleShowing;
                m_userCharacter.m_animation.loop = false;
                m_userCharacter.m_animation.AnimationName = "jump";
                Vector3 temp = new Vector3();
                if (target.m_side == true) temp = ((m_target.transform.position + new Vector3(150f, 0f, 0f)) - this.gameObject.transform.position) / 40;
                else temp = ((m_target.transform.position + new Vector3(-150f, 0f, 0f)) - this.gameObject.transform.position) / 40;
                m_move = new object[] { temp.x, temp.y, temp.z, 0 };
                StartCoroutine("Jump", m_move);
            }
        }
    }

    #region jump & backjump

    IEnumerator Jump(object[] Move)
    {
        if ((int)Move[3] < m_jumpFrame)
        {
            Vector3 temp = new Vector3((float)Move[0], (float)Move[1], (float)Move[2]);
            this.gameObject.transform.position += temp;
            m_userCharacter.m_hpBar.Move();
            yield return new WaitForEndOfFrame();
            int temp2 = (int)Move[3] + 1;
            Move[3] = (object)temp2;
            StartCoroutine("Jump", Move);
        }
        else
        {
            m_move[3] = 0;
            StartCoroutine("Attack", m_attackTime);
        }

        yield return null;
    }

    IEnumerator Jump_back(object[] Move)
    {
        if ((int)Move[3] < m_jumpFrame)
        {
            Vector3 temp = new Vector3((float)Move[0], (float)Move[1], (float)Move[2]);
            this.gameObject.transform.position -= temp;
            m_userCharacter.m_hpBar.Move();
            yield return new WaitForEndOfFrame();
            int temp2 = (int)Move[3] + 1;
            Move[3] = (object)temp2;
            StartCoroutine("Jump_back", Move);
        }
        else
        {
            m_userCharacter.m_animation.loop = true;
            m_userCharacter.m_animation.AnimationName = "stand";
            BattleManager.Instance.SkillFinished();
            m_userCharacter.m_action = 0;
            m_userCharacter.m_barIcon.Action();
        }
        yield return null;
    }
#endregion

    IEnumerator Attack(object attackFrame)
    {
        m_userCharacter.m_animation.AnimationName = "skill_1";
        for (int i = 0; i < (int)attackFrame; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine("Strike", m_strikeFrame);
    }

    IEnumerator Strike(object[] strikeFrame)
    {
        m_userCharacter.m_animation.AnimationName = "attack";
        for (int j = 0; j < strikeFrame.Length; j++)
        {
            for (int i = 0; i < (int)strikeFrame[j]; i++)
            {
                yield return new WaitForEndOfFrame();
            }
            Damage();
        }
        m_userCharacter.m_animation.AnimationName = "jump_back";
        StartCoroutine("Jump_back", m_move);
        m_move[3] = 0;
        yield return null;
    }

    void Damage()
    {
        m_target.m_hp -= m_userCharacter.m_attack;
        m_target.m_hpBar.Action();
    }
}


