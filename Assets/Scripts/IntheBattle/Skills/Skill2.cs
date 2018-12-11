using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2 : Skill {

    [SerializeField]
    SkillManager.SkillType m_SkillType = SkillManager.SkillType.Revenge;

    [SerializeField]
    int m_skillPlace = 0;
    [SerializeField]
    bool m_attackORAssist = true;
    [SerializeField]
    bool m_IsTargeting = true;
    [SerializeField]
    int m_targetNum = 1;



}
