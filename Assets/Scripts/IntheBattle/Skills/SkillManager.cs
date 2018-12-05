using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingletonMonoBehaviour<SkillManager> {

    public enum SkillType
    {
        Active = 0,
        AdditionalAttack,
        Counter
    }

    public List<Skill> m_addtionalAttack;
    public List<Skill> m_Counter;

}
