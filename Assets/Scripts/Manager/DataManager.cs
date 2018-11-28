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
    public Dictionary<int, string> m_charBaseData;
    public Dictionary<int, string> m_stringData;
    //TempDataSets
    List<int[]> m_tempTeam1CharInfo, m_tempTeam2CharInfo;

    void Start()
    {
        m_Path = Application.dataPath;

        m_team1Data = new Dictionary<int, int[]>();
        m_team2Data = new Dictionary<int, int[]>();
        m_charBaseData = new Dictionary<int, string>();
        m_stringData = new Dictionary<int, string>();

 
        ReadDataFile();
        ReadLevelData();
        ReadCharFile();
    }

    void ReadDataFile()
    {
        StreamReader strReader = new StreamReader(m_Path + "/Data/excel_base.csv");
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

            if (data_values[0] == "")
            {
                endOfFile = true;
                break;
            }

            if (!data_values[0].Equals("index") && !data_values[0].Equals("int"))
            {
                string temp = data_values[1];
                for (int i = 2; i < data_values.Length-1; i++)
                {
                    temp += "," + data_values[i];
                }
                m_charBaseData.Add(int.Parse(data_values[0]),temp);
            }
        }
    }

    void ReadLevelData()
    {
        //ReadFiles
        /*
        StreamReader strReader = new StreamReader(m_Path + "/Data/excel_Level.csv");
        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = strReader.ReadLine();

            if (data_String == null)
            {
                endOfFile = true;
                break;
            }
        }
        */

    }

    void ReadCharFile()
    {

        // ReadFiles
        /*
        StreamReader strReader = new StreamReader(m_Path + "/Data/excel_char.csv");
        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = strReader.ReadLine();

            if (data_String == null)
            {
                endOfFile = true;
                break;
            }
        }
        */
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
