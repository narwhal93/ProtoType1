﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBarManager : MonoBehaviour {

    [SerializeField]
    CharManager m_charManager;

    [SerializeField]
    GameObject m_iconPrefab;

    public List<BarIcons> m_barIconList;

    public void MakeIcons()
    {
        for (int i = 0; i < DataManager.Instance.m_charNumPerTeam*2; i++)
        {
            GameObject obj = Instantiate(m_iconPrefab) as GameObject;
            obj.transform.SetParent(this.transform);
            BarIcons Bar = obj.GetComponent<BarIcons>();
            obj.name = "BarIcons";
            obj.SetActive(false);
            m_barIconList.Add(Bar);
        }
    }

	// Use this for initialization
	void Start () {
        
	}

    
	
	// Update is called once per frame
	void Update () {
		
	}
}
