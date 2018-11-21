using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharManager : MonoBehaviour {

    [SerializeField]
    GameObject m_team1Parent;
    [SerializeField]
    GameObject m_team2Parent;

    [SerializeField]
    GameObject m_defaultCharacter;

    GameObjectPool<Character> m_characterPool;
    List<Character> m_team1, m_team2;


	// Use this for initialization
	void Start () {
        m_team1 = new List<Character>();
        m_team2 = new List<Character>();

        MakeCharacter();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void MakeCharacter()
    {
        for (int i = 0; i < DataManager.Instance.m_charNumPerTeam; i++)
        {
            GameObject obj = Instantiate(m_defaultCharacter, new Vector3(0, 0, 0), Quaternion.identity);
            obj.name = "character" + i.ToString();
            m_team1.Add(obj.GetComponent<Character>());
            obj.transform.SetParent(m_team1Parent.transform);
        }

        for (int i = 0; i < DataManager.Instance.m_charNumPerTeam; i++)
        {
            GameObject obj = Instantiate(m_defaultCharacter, new Vector3(0, 0, 0), Quaternion.identity);
            obj.name = "character" + i.ToString();
            m_team2.Add(obj.GetComponent<Character>());
            obj.transform.SetParent(m_team2Parent.transform);
        }
    }

    void SetCharacterData()
    {
        for (int i = 0; i < DataManager.Instance.m_charNumPerTeam; i++)
        {
            if (DataManager.Instance.m_team1Data.ContainsKey(i) == true)
            {
                var data_values = DataManager.Instance.m_charNumData[DataManager.Instance.m_team1Data[i][0].ToString() + "," + DataManager.Instance.m_team1Data[i][1].ToString()].Split(',');

                m_team1[i].m_level = DataManager.Instance.m_team1Data[i][1];
                m_team1[i].m_hp = int.Parse(data_values[0]);
                m_team1[i].m_attack = int.Parse(data_values[1]);
                m_team1[i].m_defense = int.Parse(data_values[2]);
                m_team1[i].m_critChance = int.Parse(data_values[3]);
                m_team1[i].m_critDmg = int.Parse(data_values[4]);
                m_team1[i].m_ccChance = int.Parse(data_values[5]);
                m_team1[i].m_ccResist = int.Parse(data_values[6]);
                m_team1[i].m_coopChance = int.Parse(data_values[7]);
                m_team1[i].m_comboChance = int.Parse(data_values[8]);
            }
        }
    }
}
