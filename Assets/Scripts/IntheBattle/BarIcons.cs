using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarIcons : MonoBehaviour {

    Character m_target;

    public int m_id;

    public void SetTarget(Character target)
    {
        m_target = target;
    }

    public void Action()
    {
        this.transform.position = new Vector3(-145, 70 - 150*m_target.m_action/100, -1);
    }

	// Use this for initialization
	void Start () {
		
	}
}
