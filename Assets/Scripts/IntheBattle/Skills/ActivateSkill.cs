using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSkill : MonoBehaviour {
    [SerializeField]
    BattleManager m_battleManager;

    void OnMouseDown()
    {
        m_battleManager.ActivateSkill();
    }

}
