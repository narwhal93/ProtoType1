using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;


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

    Vector3[] m_leftPosition = new Vector3[] { new Vector3(-230, -25, -6), new Vector3(-320,60,-6), new Vector3(-360,-120,-6), new Vector3(-450,-25,-6)};
    Vector3[] m_rightPosition = new Vector3[] { new Vector3(230, -25, -6), new Vector3(320, 60, -6), new Vector3(360, -120, -6), new Vector3(450, -25, -6)};

    public List<Character>[] m_teamChar;
    public List<MeshRenderer>[] m_teamMesh;
    public List<Animator>[] m_teamAnim;
    public List<SkeletonAnimator>[] m_teamSkeleton;

    public bool m_isLeftSide;

    // Use this for initialization
    void Start()
    {
        m_teamChar = new List<Character>[] { new List<Character>(), new List<Character>() };

        m_teamMesh = new List<MeshRenderer>[] { new List<MeshRenderer>(), new List<MeshRenderer>() };

        m_teamAnim = new List<Animator>[] { new List<Animator>(), new List<Animator>()};

        m_teamSkeleton = new List<SkeletonAnimator>[] { new List<SkeletonAnimator>(), new List<SkeletonAnimator>() };
        

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
        for (int j = 0; j < 2; j++) {
            for (int i = 0; i < DataManager.Instance.m_charNumPerTeam; i++)
            {
                GameObject obj = Instantiate(m_defaultCharacter) as GameObject;
                m_teamChar[j].Add(obj.GetComponent<Character>());
                m_teamMesh[j].Add(obj.GetComponent<MeshRenderer>());
                m_teamAnim[j].Add(obj.GetComponent<Animator>());
                m_teamSkeleton[j].Add(obj.GetComponent<SkeletonAnimator>());


                obj.name = j.ToString() + i.ToString();
                if (m_isLeftSide ^ j ==0)
                {
                    obj.transform.position = m_rightPosition[i];
                    obj.transform.SetParent(m_RightParent.transform);
                    obj.transform.localScale = new Vector3(-1f,1f,1f);
                    }
                else
                {
                    obj.transform.position = m_leftPosition[i];
                    obj.transform.SetParent(m_LeftParent.transform);
                    obj.transform.localScale = new Vector3(1f, 1f, 1f);
                }
            }
        }
    }

    void SetCharacterData()
    {
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < DataManager.Instance.m_charNumPerTeam; i++)
            {
                if (DataManager.Instance.m_teamData[j].ContainsKey(i) == true)
                {
                    var data_values = DataManager.Instance.m_charBaseData[DataManager.Instance.m_teamData[j][i][0]].Split(',');

                    //character
                    m_teamChar[j][i].m_index = DataManager.Instance.m_teamData[j][i][0];
                    m_teamChar[j][i].m_name = data_values[0];
                    m_teamChar[j][i].m_portraitName = data_values[1];
                    m_teamChar[j][i].m_elementSymbol = data_values[2];
                    switch (data_values[3])
                    {
                        case "전사":
                            {
                                m_teamChar[j][i].m_class = Character.m_charClass.warrior;
                                break;
                            }
                        case "서포터":
                            {
                                m_teamChar[j][i].m_class = Character.m_charClass.supporter;
                                break;
                            }
                    }
                    m_teamChar[j][i].m_star = int.Parse(data_values[4]);
                    m_teamChar[j][i].m_maxLevel = int.Parse(data_values[5]);
                    m_teamChar[j][i].m_maxHp = int.Parse(data_values[6]);
                    m_teamChar[j][i].m_attack = int.Parse(data_values[7]);
                    m_teamChar[j][i].m_defense = int.Parse(data_values[8]);
                    m_teamChar[j][i].m_speed = int.Parse(data_values[9]);
                    m_teamChar[j][i].m_critChance = int.Parse(data_values[10]);
                    m_teamChar[j][i].m_critDmgRatio = int.Parse(data_values[11]);
                    m_teamChar[j][i].m_ccChance = int.Parse(data_values[12]);
                    m_teamChar[j][i].m_ccResist = int.Parse(data_values[13]);
                    m_teamChar[j][i].m_coopChance = int.Parse(data_values[14]);
                    m_teamChar[j][i].m_comboChance = int.Parse(data_values[15]);

                    m_teamChar[j][i].m_battleManager = m_battleManager;
                    m_teamChar[j][i].m_hp = m_teamChar[j][i].m_maxHp;
                    m_teamChar[j][i].m_action = 0f;

                    //BarIcon
                    BarIcons temp = m_timeBarManager.m_barIconList[i+j*DataManager.Instance.m_charNumPerTeam];
                    temp.SetTarget(m_teamChar[j][i]);
                    temp.name += m_teamChar[j][i].name;
                    temp.gameObject.SetActive(true);
                    temp.transform.position = new Vector3(-590, 325, -1);
                    m_teamChar[j][i].m_barIcon = temp;

                    //HpBar
                    HpBar temp2 = m_hpBarManager.m_hpBarIconList[i + j * DataManager.Instance.m_charNumPerTeam];
                    temp2.SetTarget(m_teamChar[j][i]);
                    temp2.name += m_teamChar[j][i].name;
                    temp2.gameObject.SetActive(true);
                    m_teamChar[j][i].m_hpBar = temp2;
                    temp2.Move();
                    temp2.Action();

                    //MeshRenderer
                   // m_teamMesh[j][i].material = Resources.Load<Material>("SpineData/" + DataManager.Instance.m_teamData[j][i][0] + "_Material");
     //               m_teamMesh[j][i].sortingLayerName = "Character";
     //               m_teamMesh[j][i].sortingOrder = 1;

                    //Animator
                    m_teamAnim[j][i].runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("SpineData/" + DataManager.Instance.m_teamData[j][i][0] + "_Controller");

                    //Skeleton Animator
                    m_teamSkeleton[j][i].skeletonDataAsset = Resources.Load<SkeletonDataAsset>("SpineData/" + DataManager.Instance.m_teamData[j][i][0] + "_SkeletonData");
                    m_teamSkeleton[j][i].Initialize(true);

                    //Skill
                    m_teamChar[j][i].m_skills = new Skill[3];
                    for (int k = 0; k < 3; k++)
                    {
                        string tempstring = "Skill" + int.Parse(data_values[16 + k]);
                        Skill tempSkill = (Skill)m_teamChar[j][i].gameObject.AddComponent(Type.GetType(tempstring));
                        m_teamChar[j][i].m_skills[k] = tempSkill;
                        tempSkill.m_userCharacter = m_teamChar[j][i];
                        tempSkill.m_animator = this.gameObject.GetComponent<Animator>();
                    }
                }
            }
        }
    }
}
