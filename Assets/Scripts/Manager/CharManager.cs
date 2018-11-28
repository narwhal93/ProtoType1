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
    [SerializeField]
    HpBarManager m_hpBarManager;

    Vector3[] m_team1Location = new Vector3[] { new Vector3(-230, -25, -6), new Vector3(-320,60,-6), new Vector3(-360,-120,-6), new Vector3(-450,-25,-6)};
    Vector3[] m_team2Location = new Vector3[] { new Vector3(230, -25, -6), new Vector3(320, 60, -6), new Vector3(360, -120, -6), new Vector3(450, -25, -6)};

    GameObjectPool<Character> m_characterPool;
    public List<Character> m_team1, m_team2;

    public bool m_isLeftSide;

    // Use this for initialization
    void Start()
    {


        m_team1 = new List<Character>();
        m_team2 = new List<Character>();

        m_timeBarManager.m_barIconList = new List<BarIcons>();
        m_hpBarManager.m_hpBarIconList = new List<HpBar>();

        m_isLeftSide = true;

        MakeCharacter();
        m_timeBarManager.MakeIcons();
        m_hpBarManager.MakeIcons();
        SetCharacterData();
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
                var data_values = DataManager.Instance.m_charBaseData[DataManager.Instance.m_team1Data[i][0]].Split(',');

                m_team1[i].m_index = DataManager.Instance.m_team1Data[i][0];
                m_team1[i].m_name = data_values[0];
                m_team1[i].m_portraitName = data_values[1];
                m_team1[i].m_elementSymbol = data_values[2];
                switch (data_values[3])
                {
                    case "전사":
                        {
                            m_team1[i].m_class = Character.m_charClass.warrior;
                            break;
                        }
                    case "서포터":
                        {
                            m_team1[i].m_class = Character.m_charClass.supporter;
                            break;
                        }
                }
                m_team1[i].m_star = int.Parse(data_values[4]);
                m_team1[i].m_maxLevel = int.Parse(data_values[5]);
                m_team1[i].m_maxHp = int.Parse(data_values[6]);
                m_team1[i].m_attack = int.Parse(data_values[7]);
                m_team1[i].m_defense = int.Parse(data_values[8]);
                m_team1[i].m_speed = int.Parse(data_values[9]);
                m_team1[i].m_critChance = int.Parse(data_values[10]);
                m_team1[i].m_critDmgRatio = int.Parse(data_values[11]);
                m_team1[i].m_ccChance = int.Parse(data_values[12]);
                m_team1[i].m_ccResist = int.Parse(data_values[13]);
                m_team1[i].m_coopChance = int.Parse(data_values[14]);
                m_team1[i].m_comboChance = int.Parse(data_values[15]);

                m_team1[i].m_battleManager = m_battleManager;
                m_team1[i].m_hp = m_team1[i].m_maxHp;
                m_team1[i].m_action = 0f;

                //BarIcon
                BarIcons temp = m_timeBarManager.m_barIconList[i];
                temp.SetTarget(m_team1[i]);
                temp.name += m_team1[i].name;
                temp.gameObject.SetActive(true);
                temp.transform.position = new Vector3(-590, 325, -1);
                m_team1[i].m_barIcon = temp;

                //HpBar
                HpBar temp2 = m_hpBarManager.m_hpBarIconList[i];
                temp2.SetTarget(m_team1[i]);
                temp2.name += m_team1[i].name;
                temp2.gameObject.SetActive(true);
                temp2.Move();
                temp2.Action();

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
                var data_values = DataManager.Instance.m_charBaseData[DataManager.Instance.m_team2Data[i][0]].Split(',');

                m_team2[i].m_index = DataManager.Instance.m_team1Data[i][0];
                m_team2[i].m_name = data_values[0];
                m_team2[i].m_portraitName = data_values[1];
                m_team2[i].m_elementSymbol = data_values[2];
                switch (data_values[3])
                {
                    case "전사":
                        {
                            m_team2[i].m_class = Character.m_charClass.warrior;
                            break;
                        }
                    case "서포터":
                        {
                            m_team2[i].m_class = Character.m_charClass.supporter;
                            break;
                        }
                }
                m_team2[i].m_star = int.Parse(data_values[4]);
                m_team2[i].m_maxLevel = int.Parse(data_values[5]);
                m_team2[i].m_maxHp = int.Parse(data_values[6]);
                m_team2[i].m_attack = int.Parse(data_values[7]);
                m_team2[i].m_defense = int.Parse(data_values[8]);
                m_team2[i].m_speed = int.Parse(data_values[9]);
                m_team2[i].m_critChance = int.Parse(data_values[10]);
                m_team2[i].m_critDmgRatio = int.Parse(data_values[11]);
                m_team2[i].m_ccChance = int.Parse(data_values[12]);
                m_team2[i].m_ccResist = int.Parse(data_values[13]);
                m_team2[i].m_coopChance = int.Parse(data_values[14]);
                m_team2[i].m_comboChance = int.Parse(data_values[15]);

                m_team2[i].m_battleManager = m_battleManager;
                m_team2[i].m_hp = m_team2[i].m_maxHp;
                m_team2[i].m_action = 0f;

                //BarIcon
                BarIcons temp = m_timeBarManager.m_barIconList[DataManager.Instance.m_charNumPerTeam + i];
                temp.SetTarget(m_team2[i]);
                temp.name += m_team2[i].name;
                temp.gameObject.SetActive(true);
                temp.transform.position = new Vector3(-590, 325, -1);
                m_team2[i].m_barIcon = temp;

                //HpBar
                HpBar temp2 = m_hpBarManager.m_hpBarIconList[DataManager.Instance.m_charNumPerTeam + i];
                temp2.SetTarget(m_team2[i]);
                temp2.name += m_team2[i].name;
                temp2.gameObject.SetActive(true);
                temp2.Move();
                temp2.Action();
            }
            else
            {
                m_characterPool.push(m_team2[i]);
            }
        }
    }
}
