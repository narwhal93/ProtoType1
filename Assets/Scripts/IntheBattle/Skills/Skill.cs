using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[System.Serializable]
public class Skill : MonoBehaviour{

    string Json;

    object[] m_move;

    object m_attackTime;

    object[] m_strikeFrame;

    // Static
    public int m_skillIndex;
    public int m_skillNumber;
    public SkillManager.SkillType m_skillType;
    public string m_skillName;
    public string m_texture;
    public string m_skillText;
    public string m_skillTextAwaken;
    public int m_jumpFrame;
    public int m_IAttackFrame;
    public int[] m_IStrikeFrame;
    public int m_targetNum;
    public bool m_soulBurn;
    public string m_jumpAnimation;
    public string m_jumpBackAnimation;
    public string m_attackAnimation;
    public string m_strikeAnimation;

    //From Personal DB
    public bool m_skillAwakening;
    public int m_skillLevel;

    //Calculated
    public int m_coolDown;
    public int m_curCoolDown;
    public Damage[] m_IDamage;

    public Character m_target;
    public Character m_user;

    public bool m_cooperation = false;
    public bool m_combo = false;


    // Combo Only
    [HideInInspector]
    public int m_comboJumpFrame;
    [HideInInspector]
    public int m_comboIAttackFrame;
    [HideInInspector]
    public int[] m_comboIStrikeFrame;
    [HideInInspector]
    public string m_comboJumpAnimation;
    [HideInInspector]
    public string m_comboJumpBackAnimation;
    [HideInInspector]
    public string m_comboAttackAnimation;
    [HideInInspector]
    public string m_comboStrikeAnimation;
    // Combo Only

    public virtual void Activating(Character target, SkillManager.SkillType SkillType)
    {
        m_target = target;

        if (m_skillType == SkillType || SkillType == SkillManager.SkillType.Revenge)
        {

            m_attackTime = m_IAttackFrame;
            m_strikeFrame = new object[m_IStrikeFrame.Length];
            for (int i = 0; i < m_IStrikeFrame.Length; i++)
            {
                m_strikeFrame[i] = m_IStrikeFrame[i];
            }
            
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

    #region attack & Strike
    IEnumerator Attack(object attackFrame)
    {
        m_user.m_animation.AnimationName = m_attackAnimation;
        for (int i = 0; i < (int)attackFrame; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        if (m_user.m_animation.AnimationName == m_attackAnimation)
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
    #endregion

    #region Save & Load
    public void MakeScript()
    {
        Json = JsonUtility.ToJson(this);
        Debug.Log(Json);
        StreamWriter strWriter = new StreamWriter(Application.dataPath + "/Data/Skills/Skill" + m_skillIndex + ".Json");
        strWriter.WriteLine(Json);
        strWriter.Close();
    }

    public void GetScript()
    {
        StreamReader strReader = new StreamReader(Application.dataPath + "/Data/Skills/Skill" + m_skillIndex + ".Json");
        string strData = strReader.ReadLine();
        strReader.Close();
        Debug.Log(strData);
        Skill temp = this;
        JsonUtility.FromJsonOverwrite(strData, temp);
    }
    #endregion


    [System.Serializable]
    public class Damage : System.Object
    {
        public float _basicDamage;
        public float _damage;
        public List<GiveBuff> _buffs;

        public Damage(float dmg, GiveBuff[] buffs )
        {
            _damage = dmg;
            _buffs = new List<GiveBuff>();
            for (int i = 0; i < buffs.Length; i++)
            {
                _buffs.Add(buffs[i]);
            }
        }

        public void AttachBuff(Character target)
        {
            for (int i = 0; i < _buffs.Count; i++)
            {
                if (_buffs[i].m_IsTagetEnemy)
                {
                    target.m_buff.Add(BuffManager.Instance.Buffs.Pop());
                    target.m_buff[target.m_buff.Count - 1].gameObject.SetActive(true);
                    target.m_buff[target.m_buff.Count - 1].SetUser(target);
                    target.m_buff[target.m_buff.Count - 1].m_durationLeft = _buffs[i].m_buffturn;
                    target.m_buff[target.m_buff.Count - 1].m_tim = _buffs[i].m_timing;
                    target.m_buff[target.m_buff.Count - 1].m_type = _buffs[i].m_bufftype;
                    target.m_buff[target.m_buff.Count - 1].m_extraParam = _buffs[i].m_extraParam;
                }
            }
            target.MoveBuff();
        }

        public void GiveDamage(Character target, Character user)
        {
            target.m_hp -= user.m_attack * _damage + _basicDamage;
            target.m_hpBar.Action();
            AttachBuff(target);
        }
    }

    [System.Serializable]
    public class GiveBuff : System.Object
    {
        public bool m_IsTagetEnemy;
        public int m_targetNum;
        public Buff.BuffType m_bufftype;
        public Buff.ActionTiming m_timing;
        public int m_buffturn;
        public float m_extraParam;
        
        public GiveBuff(Buff.BuffType type, Buff.ActionTiming timing, int buffturn, bool target, int targetNum, float extraParam)
        {
            m_bufftype = type;
            m_timing = timing;
            m_buffturn = buffturn;
            m_IsTagetEnemy = target;
            m_targetNum = targetNum;
            m_extraParam = extraParam;
        }
    }
}

[CustomEditor(typeof(Skill), true)]
public class SkillEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Skill myScript = (Skill)target;

        if (myScript.m_combo)
        {
            myScript.m_comboJumpFrame = EditorGUILayout.IntField("comboJumpFrame",myScript.m_comboJumpFrame);
            myScript.m_comboIAttackFrame = EditorGUILayout.IntField("comboAttackFrame", myScript.m_comboIAttackFrame);
            int m_comboStrikeLength = EditorGUILayout.IntField("comboIStrikeFrame", myScript.m_comboIStrikeFrame.Length);
            if(myScript.m_comboIStrikeFrame.Length != m_comboStrikeLength) myScript.m_comboIStrikeFrame = new int[m_comboStrikeLength];

            for (int i = 0; i < m_comboStrikeLength; i++)
            {
                myScript.m_comboIStrikeFrame[i] = EditorGUILayout.IntField("comboIStrikeFrame" + i.ToString(), myScript.m_comboIStrikeFrame[i]);
            }
            
        }

        if (GUILayout.Button("Refresh"))
        {
            myScript.m_IDamage = new Skill.Damage[myScript.m_IStrikeFrame.Length];
            for (int i = 0; i < myScript.m_IStrikeFrame.Length; i++)
            {
                myScript.m_IDamage[i] = new Skill.Damage(0, new Skill.GiveBuff[] { });
            }
        }

        if (GUILayout.Button("Make Script"))
        {
            myScript.MakeScript();
        }

        if (GUILayout.Button("Get Script"))
        {
            myScript.GetScript();
        }
    }
}