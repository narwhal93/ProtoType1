using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharManager : MonoBehaviour {

    [SerializeField]
    BattleManager m_battleManager;
    [SerializeField]
    GameObject m_LeftParent;
    [SerializeField]
    GameObject m_RightParent;
    [SerializeField]
    GameObject m_defaultCharacter;
    [SerializeField]
    TimeBarManager m_timeBarManager;

    Vector3[] m_team1Location = new Vector3[] { new Vector3(-60, -20, -6), new Vector3(-80,-10,-6), new Vector3(-80,-30,-6), new Vector3(-100,-20,-6)};
    Vector3[] m_team2Location = new Vector3[] { new Vector3(80, -20, -6), new Vector3(100, -10, -6), new Vector3(100, -30, -6), new Vector3(120, -20, -6)};

    GameObjectPool<Character> m_characterPool;
    public List<Character> m_team1, m_team2;

    public bool m_isLeftSide;

	// Use this for initialization
	void Start () {


        m_team1 = new List<Character>();
        m_team2 = new List<Character>();

        m_timeBarManager.m_barIconList = new List<BarIcons>();

        m_isLeftSide = true;

        MakeCharacter();
        m_timeBarManager.MakeIcons();
        SetCharacterData();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void MakeCharacter()
    {
        for (int i = 0; i < DataManager.Instance.m_charNumPerTeam; i++)
        {
            GameObject obj = Instantiate(m_defaultCharacter) as GameObject;
            m_team1.Add(obj.GetComponent<Character>());
            obj.transform.SetParent(m_LeftParent.transform);
            if (m_isLeftSide)
            {
                obj.name = 1.ToString() + i.ToString();
                obj.transform.position = m_team1Location[i];
            }
            else
            {
                obj.name = 2.ToString() + i.ToString();
                obj.transform.position = m_team2Location[i];
            } 
        }

        for (int i = 0; i < DataManager.Instance.m_charNumPerTeam; i++)
        {
            GameObject obj = Instantiate(m_defaultCharacter, new Vector3(0, 0, 0), Quaternion.identity);
            m_team2.Add(obj.GetComponent<Character>());
            obj.transform.SetParent(m_RightParent.transform);
            if (m_isLeftSide)
            {
                obj.name = 2.ToString() + i.ToString();
                obj.transform.position = m_team2Location[i];
            }
            else
            {
                obj.name = 1.ToString() + i.ToString();
                obj.transform.position = m_team1Location[i];
            } 
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
                m_team1[i].m_speed = int.Parse(data_values[3]);
                m_team1[i].m_critChance = int.Parse(data_values[4]);
                m_team1[i].m_critDmg = int.Parse(data_values[5]);
                m_team1[i].m_ccChance = int.Parse(data_values[6]);
                m_team1[i].m_ccResist = int.Parse(data_values[7]);
                m_team1[i].m_coopChance = int.Parse(data_values[8]);
                m_team1[i].m_comboChance = int.Parse(data_values[9]);
                m_team1[i].m_battleManager = m_battleManager;

                BarIcons temp = m_timeBarManager.m_barIconList[i];
                temp.SetTarget(m_team1[i]);
                temp.m_id = int.Parse(m_team1[i].name);
                temp.name += m_team1[i].name;
                temp.gameObject.SetActive(true);
                temp.transform.position = new Vector3(-145, 70, -1);
            }
            else
            {
                m_characterPool.push(m_team1[i]);
            }
        }

        for (int i = 0; i < DataManager.Instance.m_charNumPerTeam; i++)
        {
            if (DataManager.Instance.m_team2Data.ContainsKey(i) == true)
            {
                var data_values = DataManager.Instance.m_charNumData[DataManager.Instance.m_team2Data[i][0].ToString() + "," + DataManager.Instance.m_team2Data[i][1].ToString()].Split(',');

                m_team2[i].m_level = DataManager.Instance.m_team2Data[i][1];
                m_team2[i].m_hp = int.Parse(data_values[0]);
                m_team2[i].m_attack = int.Parse(data_values[1]);
                m_team2[i].m_defense = int.Parse(data_values[2]);
                m_team2[i].m_speed = int.Parse(data_values[3]);
                m_team2[i].m_critChance = int.Parse(data_values[4]);
                m_team2[i].m_critDmg = int.Parse(data_values[5]);
                m_team2[i].m_ccChance = int.Parse(data_values[6]);
                m_team2[i].m_ccResist = int.Parse(data_values[7]);
                m_team2[i].m_coopChance = int.Parse(data_values[8]);
                m_team2[i].m_comboChance = int.Parse(data_values[9]);
                m_team2[i].m_battleManager = m_battleManager;

                BarIcons temp = m_timeBarManager.m_barIconList[DataManager.Instance.m_charNumPerTeam+i];
                temp.SetTarget(m_team2[i]);
                temp.m_id = int.Parse(m_team2[i].name);
                temp.name += m_team2[i].name;
                temp.gameObject.SetActive(true);
                temp.transform.position = new Vector3(-145, 70, -1);
            }
            else
            {
                m_characterPool.push(m_team2[i]);
            }
        }
    }
}
