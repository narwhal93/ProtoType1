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
}
