using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : DontDestroy<DataManager> {

    //Global Data
    [SerializeField]
    public int m_charNumPerTeam = 4;

    //Root Path
    string m_Path;

    //DataSets
    public Dictionary<int, int[]>[] m_teamData;
    public Dictionary<int, string> m_charBaseData;
    public Dictionary<int, string> m_stringData;

    //TempDataSets
    List<int[]>[] m_tempCharInfo;

    void Start()
    {
        m_Path = Application.dataPath;

        m_tempCharInfo = new List<int[]>[] { new List<int[]>() , new List<int[]>() };
        m_teamData = new Dictionary<int, int[]>[2]{ new Dictionary<int, int[]>(), new Dictionary<int, int[]>()};

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
        strReader.Close();
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
        m_tempCharInfo[0] = new List<int[]>() {
            new int[] { 1, 1 },
            new int[] { 1, 2 },
            new int[] { 1, 3 },
            new int[] { 1, 4 }
        };
        m_tempCharInfo[1] = new List<int[]>() {  
            new int[] { 1,1},
            new int[] { 1,2},
            new int[] { 1,3},
            new int[] { 1,4}
        };

        m_teamData[0].Add(0, m_tempCharInfo[0][0]);
        m_teamData[0].Add(1, m_tempCharInfo[0][1]);
        m_teamData[0].Add(2, m_tempCharInfo[0][2]);
        m_teamData[0].Add(3, m_tempCharInfo[0][3]);

        m_teamData[1].Add(0, m_tempCharInfo[1][0]);
        m_teamData[1].Add(1, m_tempCharInfo[1][1]);
        m_teamData[1].Add(2, m_tempCharInfo[1][2]);
        m_teamData[1].Add(3, m_tempCharInfo[1][3]);
    }                                       
}
