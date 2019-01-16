using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Skill1 : Skill {

    object[] m_move;

    object m_attackTime;

    object[] m_strikeFrame;

    // Activating -> Jump -> attack -> strike or hit -> Jump back

    public override void Init()
    {
        m_skillIndex = 1;
        m_skillNumber = 1;
        m_skillType = SkillManager.SkillType.Active;
        m_skillName = "ttt";
        m_targetNum = 1;

        m_jumpFrame = 40;
        m_IAttackFrame = 40;
        m_IStrikeFrame = new int[2] { 35, 90 };
        m_IDamage = new Damage[] { new Damage(1f, new GiveBuff[1]{new GiveBuff(Buff.BuffType.Nothing, Buff.ActionTiming.GiveDamage, 0, true, 1, 0)}), 
                                   new Damage(1f, new GiveBuff[0])};

        m_jumpAnimation = "jump";
        m_jumpBackAnimation = "jump_back";
        m_attackAnimation = "skill_1";
        m_strikeAnimation = "attack";

        m_comboJumpAnimation = "jump";
        m_comboJumpBackAnimation = "jump_back";
        m_comboAttackAnimation  = "skill_1";
        m_comboStrikeAnimation  = "attack";
}

    public override void Activating(Character target, SkillManager.SkillType SkillType)
    {
        if (m_skillType == SkillType || SkillType == SkillManager.SkillType.Revenge)
        {
 
            m_attackTime = m_IAttackFrame;
            m_strikeFrame = new object[m_IStrikeFrame.Length];
            for (int i = 0; i < m_IStrikeFrame.Length; i++)
            {
                m_strikeFrame[i] = m_IStrikeFrame[i];
            }

            base.Activating(target, SkillType);

            if ((m_skillType == SkillManager.SkillType.Active) ^ (m_user.m_side == m_target.m_side))
            {
                BattleManager.Instance.m_battleSt = BattleManager.BattleState.battleShowing;
                m_user.m_animation.loop = false;
                m_user.m_animation.AnimationName = m_jumpAnimation;
                Vector3 temp = new Vector3();
                if (target.m_side == true) temp = ((m_target.transform.position + new Vector3(150f, 0f, 0f)) - this.gameObject.transform.position) / m_jumpFrame;
                else temp = ((m_target.transform.position + new Vector3(-150f, 0f, 0f)) - this.gameObject.transform.position) / m_jumpFrame;
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
            m_user.m_hpBar.Move();
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
            m_user.m_hpBar.Move();
            yield return new WaitForEndOfFrame();
            int temp2 = (int)Move[3] + 1;
            Move[3] = (object)temp2;
            StartCoroutine("Jump_back", Move);
        }
        else
        {
            m_user.m_animation.loop = true;
            m_user.m_animation.AnimationName = "stand";
            
            m_user.m_action = 0;
            m_user.m_barIcon.Action();
        }
        yield return null;
    }
#endregion

    IEnumerator Attack(object attackFrame)
    {
        m_user.m_animation.AnimationName = m_attackAnimation;
        for (int i = 0; i < (int)attackFrame; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        if(m_user.m_animation.AnimationName == m_attackAnimation)
        StartCoroutine("Strike", m_strikeFrame);
    }

    IEnumerator Strike(object[] strikeFrame)
    {
        m_user.m_animation.AnimationName = "attack";
        for (int j = 0; j < strikeFrame.Length; j++)
        {
            for (int i = 0; i < (int)strikeFrame[j]; i++)
            {
                yield return new WaitForEndOfFrame();
            }
            m_IDamage[j].GiveDamage(m_target, m_user);
        }
        m_user.m_animation.AnimationName = m_jumpBackAnimation;
        StartCoroutine("Jump_back", m_move);
        m_move[3] = 0;
        yield return null;
    }

}

