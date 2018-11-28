using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarIcons : MonoBehaviour {

    Character m_target;

    public void SetTarget(Character target)
    {
        m_target = target;
    }

    public void Action()
    {
        this.transform.position = new Vector3(-590, 325 - 695*m_target.m_action/100, -1);
    }
}
