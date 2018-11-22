using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : SingletonMonoBehaviour<DataManager> {

    //Global Data
    [SerializeField]
    public int m_charNumPerTeam = 4;
    //Root Path
    string m_Path;
    //DataSets
    public Dictionary<int, int[]> m_team1Data, m_team2Data;
    public Dictionary<string, string> m_charNumData;
    public Dictionary<int, string> m_stringData;


    //TempDataSets
    List<int[]> m_tempTeam1CharInfo, m_tempTeam2CharInfo;

    void Start()
    {
        m_Path = Application.dataPath;

        m_team1Data = new Dictionary<int, int[]>();
        m_team2Data = new Dictionary<int, int[]>();
        m_charNumData = new Dictionary<string, string>();
        m_stringData = new Dictionary<int, string>();

        ReadDataFile();
        ReadCharFile();

    }

    void ReadDataFile()
    {
        StreamReader strReader = new StreamReader(m_Path + "/Data/excel_char.csv");
        string pre_index = "index";
        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = strReader.ReadLine();

            if (data_String == null)
            {
                endOfFile = true;
                break;
            }
            
            var data_values = data_String.Split(',');
            
            if (!data_values[0].Equals("") && !data_values[0].Equals("index"))
            {
              //  Debug.Log("come");
                string m_tempCharData = data_values[7];
                for (int i = 8; i < data_values.Length; i++)
                {
                    m_tempCharData += ",";
                    m_tempCharData += data_values[i];
                }
                m_charNumData.Add(data_values[0] + "," + data_values[6], m_tempCharData);
            }
            if (!data_values[0].Equals(pre_index) && !data_values[0].Equals(""))
            {
                pre_index = data_values[0];
                m_stringData.Add(int.Parse(data_values[0]),data_values[1] + data_values[2] + data_values[3] + data_values[4] + data_values[5]);
            }
        }
    }

    void ReadCharFile()
    {
        m_tempTeam1CharInfo = new List<int[]>()
        {
            new int[] { 1,1},
            new int[] { 1,2},
            new int[] { 1,3},
            new int[] { 1,4}
        };
        m_team1Data.Add(0,m_tempTeam1CharInfo[0]);
        m_team1Data.Add(1,m_tempTeam1CharInfo[1]);
        m_team1Data.Add(2,m_tempTeam1CharInfo[2]);
        m_team1Data.Add(3,m_tempTeam1CharInfo[3]);

        m_tempTeam2CharInfo = new List<int[]>()
        {
            new int[] { 1,1},
            new int[] { 1,2},
            new int[] { 1,3},
            new int[] { 1,4}
        };
        m_team2Data.Add(0, m_tempTeam2CharInfo[0]);
        m_team2Data.Add(1, m_tempTeam2CharInfo[1]);
        m_team2Data.Add(2, m_tempTeam2CharInfo[2]);
        m_team2Data.Add(3, m_tempTeam2CharInfo[3]);
    }

}
