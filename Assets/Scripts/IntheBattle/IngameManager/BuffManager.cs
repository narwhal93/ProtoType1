using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuffManager : SingletonMonoBehaviour<BuffManager> {


    public Stack<Buff> Buffs;
    public int m_MaxBuff;
    [SerializeField]
    GameObject m_BuffPrefab;


    void Start()
    {
        m_MaxBuff = 80;

        Buffs = new Stack<Buff>();
        MakeBuffs();

    }

    void MakeBuffs()
    {
        for (int i = 0; i < m_MaxBuff; i++)
        {
            GameObject obj = Instantiate(m_BuffPrefab) as GameObject;
            obj.transform.SetParent(this.transform);
            Buff tempBuff = obj.GetComponent<Buff>();
            obj.gameObject.SetActive(false);
            obj.gameObject.name = "Buff" + i.ToString();
            Buffs.Push(tempBuff);
        }
    }

}
