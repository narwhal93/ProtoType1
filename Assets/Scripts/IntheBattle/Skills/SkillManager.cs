using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : SingletonMonoBehaviour<SkillManager> {

    public enum SkillType
    {
        Rush = 0,
        Active,
        Passive,
        React,
        Revenge,
        AdditionalAttack
    }

    public List<Skill>[] m_reaction;
    public List<Skill>[] m_addtionalAttack;
    public List<Skill>[] m_revenge;

    public void Reaction(Character user, Character target)
    {
        for (int j = 0; j < m_reaction.Length; j++)
        {
            for (int i = 0; i < m_reaction[j].Count; i++)
            {
                m_reaction[j][i].Activating(target, SkillType.React);
            }
        }
    }
}
