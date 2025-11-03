using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    const string BESTSCORE_KEY = "BESTSCORE";
    const string BESTPLAYER_KEY = "BESTPLAYER";
    const string NONAME = "NoName";

    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TMP_InputField inputField;

    string userName;
    public string UserName
    {
        get => userName;
        set => userName = (value.Length == 0) ? NONAME : value;
    }
    public string BestPlayer
    {
        get => PlayerPrefs.GetString(BESTPLAYER_KEY, NONAME);
        set => PlayerPrefs.GetString(BESTPLAYER_KEY, value);
    }

    public int BestScore
    {
        get => PlayerPrefs.GetInt(BESTSCORE_KEY, 0);
        set
        {
            if(BestScore <= value)
            {
                PlayerPrefs.GetInt(BESTSCORE_KEY, value);
                BestPlayer = userName;
            }
        }
    }

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        bestScoreText.text = GetBestText();
    }
    public string GetBestText()
    {
        return $"Best Player - {BestPlayer} : {BestScore}";
    }

    public void UpdateName()
    {
        userName = inputField.text;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        UserName = inputField.text;
        SceneManager.LoadScene("main");
    }

    [MenuItem("BlockGame/Reset score")]
    public static void ResetBestScore()
    {
        PlayerPrefs.SetInt(BESTSCORE_KEY, 0);
        PlayerPrefs.SetString(BESTPLAYER_KEY, NONAME);
    }
}
