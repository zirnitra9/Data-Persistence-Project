using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Memorius : MonoBehaviour
{
    //private variables
    static Memorius instance;
    static string playerName;
    static string[] highScoreNames = new string[5];
    static int[] highScoreNumbers = new int[5];

    //get and set
    public static Memorius Instance => instance;

    public string[] HighScoreNames { get => highScoreNames; set => highScoreNames = value; }
    public int[] HighScoreNumbers { get => highScoreNumbers; set => highScoreNumbers = value; }
    public string PlayerName { get => playerName; set => playerName = value; }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SaveHighScores()
    {
        SavedData data = new SavedData();
        data.Names = highScoreNames;
        data.Scores = highScoreNumbers;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/highscores.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavedData data = JsonUtility.FromJson<SavedData>(json);
            highScoreNames = data.Names;
            highScoreNumbers = data.Scores;
        }
        else
        {
            CreateNewHighScores();
        }
    }

    public void UpdateHighScores(string namevar, int scorevar)
    {
        for (int i = 0; i < 5; i++)
        {
            if(scorevar > highScoreNumbers[i])
            {
                InjectHighScore(namevar, scorevar, i);
                Debug.Log("inserted new high score at position " + i);
                break;
            }
        }
    }

    public int StealScoreFromMainManager()
    {
        MainManager mng = GameObject.Find("MainManager").GetComponent<MainManager>();
        return mng.GetScore();
    }

    private void InjectHighScore(string namevar, int scorevar, int positionvar)
    {
        var oldNames = new string[5];
        var oldValues = new int[5];

        oldNames = highScoreNames;
        oldValues = highScoreNumbers;

        for (int i = 4; i > -1; i--)
        {
            if(positionvar == i)
            {
                highScoreNames[i] = namevar;
                highScoreNumbers[i] = scorevar;
                
            } else if (positionvar < i)
            {
                highScoreNames[i] = oldNames[i-1];
                highScoreNumbers[i] = oldValues[i-1];
            }
        }
    }

    void CreateNewHighScores()
    {
        for (int i = 0; i < 5; i++)
        {
            highScoreNames[i] = "n/a";
        }

        for (int i = 0; i < 5; i++)
        {
            highScoreNumbers[i] = 0;
        }
    }

    [System.Serializable]
    class SavedData
    {
        public string[] Names;
        public int[] Scores;
    }

}
