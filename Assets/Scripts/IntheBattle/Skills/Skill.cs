using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Skill : MonoBehaviour{

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
    }

    public virtual void Init()
    {

    }

    public void MakeScript()
    {
        StreamWriter strWriter = new StreamWriter(Application.dataPath + "/Scripts/IntheBattle/Skills/" + m_skillName + ".cs");
        strWriter.WriteLine(@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Skill1 : Skill {

    object[] m_move;

    object m_attackTime;

    object[] m_strikeFrame;

    

    // Activating -> Jump -> attack -> strike or hit -> Jump back

    [HideInInspector]
    public string m_comboJumpAnimation;
    [HideInInspector]
    public string m_comboJumpBackAnimation;
    [HideInInspector]
    public string m_comboAttackAnimation;
    [HideInInspector]
    public string m_comboStrikeAnimation;

    public override void Init()
    {
        m_skillIndex = " + m_skillIndex + @";
        m_skillNumber = " + m_skillNumber + @";
        m_skillType = SkillManager.SkillType." + m_skillType + @";
        m_skillName =   "+"\"" + m_skillName + "\"" +@";
        m_targetNum = " + m_targetNum + @";

        m_IDamage = new Damage[] { new Damage(1), new Damage(2) };

        m_jumpFrame = " + m_jumpFrame + @";
        m_IAttackFrame = " + m_IAttackFrame 
        + @";
        m_IStrikeFrame = new int[2] { 35, 90 }; 

        m_jumpAnimation = " + "\""+ m_jumpAnimation +"\"" + @";
        m_jumpBackAnimation = "+ "\"" + m_jumpBackAnimation + "\"" + @";
");
        strWriter.Flush();
    }

    [System.Serializable]
    public class Damage : System.Object
    {
        public int damage;

        public Damage(int dmg)
        {
            damage = dmg;
        }

        public void GiveDamage(Character target, Character user)
        {
            target.m_hp -= user.m_attack * damage;
            target.m_hpBar.Action();
        }

    }

    [System.Serializable]
    public class Combo : System.Object
    {
        public int damage;

        public Combo(int dmg)
        {
            damage = dmg;
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

        if (GUILayout.Button("Refresh"))
        {
            myScript.m_IDamage = new Skill.Damage[myScript.m_IStrikeFrame.Length];
            for (int i = 0; i < myScript.m_IStrikeFrame.Length; i++)
            {
                myScript.m_IDamage[i] = new Skill.Damage(0);
            }
        }

        if (GUILayout.Button("Make Script"))
        {
            myScript.MakeScript();
        }
    }
}