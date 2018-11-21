using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class LoadSceneManager : MonoBehaviour {
    public enum eState
    {
        Start = 0,
        BattlePhaze
    }
    eState m_state;

    public eState m_State { get { return m_state; } }

    eState m_loadState;

    private static LoadSceneManager m_loadSceneManager;

    public static LoadSceneManager Instance { get { return m_loadSceneManager; } }

    AsyncOperation m_sceneLoadTask = null;
    string m_loadSceneName = null;    
    GUIText m_progressText;
    void Awake()
    {
        if (m_loadSceneManager == null)
        {
            m_loadSceneManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (m_loadSceneManager != this)
        {
            Destroy(this.gameObject);
            return;
        }
        m_progressText = GetComponent<GUIText>();
    }
	// Use this for initialization
	void Start () {
        m_loadState = eState.Start;
        SetState(eState.Start);
    }
	
	// Update is called once per frame
	void Update () {
        if(m_sceneLoadTask != null && m_loadSceneName != null)
        {
            if(m_sceneLoadTask.isDone == true)
            {
                Resources.UnloadUnusedAssets();
                m_sceneLoadTask = null;
                m_loadSceneName = null;
                //m_progressText.text = string.Empty;
            }
            else
            {
                float p = m_sceneLoadTask.progress * 100;
                //m_progressText.text = Mathf.RoundToInt(p).ToString() + "%";              
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Key Pressed");
        }
		
	}

    #region Public Method
    public void SetState(eState state)
    {
        m_state = state;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevel(string levelName, eState state)
    {
        if (!string.IsNullOrEmpty(m_loadSceneName))
        {
            return;
        }
        m_loadState = state;
        SceneManager.LoadScene(levelName);
    }
    public void LoadLevelMerge(string levelName, eState state)
    {
        if (!string.IsNullOrEmpty(m_loadSceneName))
        {
            return;
        }
        m_loadState = state;
        m_loadSceneName = levelName;
        m_sceneLoadTask = SceneManager.LoadSceneAsync(m_loadSceneName, LoadSceneMode.Additive);
    }
    public void LoadLevelAsync(string levelName, eState state)
    {
        if (!string.IsNullOrEmpty(m_loadSceneName))
        {
            return;
        }
        m_loadState = state;
        m_loadSceneName = levelName;
        m_sceneLoadTask = SceneManager.LoadSceneAsync(m_loadSceneName);
    }
    #endregion
}
