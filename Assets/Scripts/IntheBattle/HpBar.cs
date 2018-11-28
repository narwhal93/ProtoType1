﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {

    Character m_target;
    [SerializeField]
    Slider m_slider;

    public void Start()
    {
        m_target = null;
    }

    public void SetTarget(Character target)
    {
        m_target = target;
    }

    public void Action()
    {
        m_slider.value = (m_target.m_hp / m_target.m_maxHp);
    }

    public void Move()
    {
        this.gameObject.transform.position = m_target.gameObject.transform.position + new Vector3(0,70,0);
    }
}