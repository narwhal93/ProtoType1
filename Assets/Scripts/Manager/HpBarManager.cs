using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarManager : SingletonMonoBehaviour<HpBarManager> {

    [SerializeField]
    GameObject m_hpBarPrefab;

    public List<HpBar> m_hpBarIconList;

    public void MakeIcons()
    {
        for (int i = 0; i < DataManager.Instance.m_charNumPerTeam * 2; i++)
        {
            GameObject obj = Instantiate(m_hpBarPrefab) as GameObject;
            obj.transform.SetParent(this.transform);
            HpBar tempHpBar = obj.GetComponent<HpBar>();
            obj.name = "HPBarIcons";
            obj.SetActive(false);
            m_hpBarIconList.Add(tempHpBar);
        }
    }
}
