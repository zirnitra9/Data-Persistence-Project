using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    [SerializeField] Text nameText;
    [SerializeField] Text highScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        ConstructTheBricks();

        SetPlayerName();
        if(Memorius.Instance != null)
        {
            Memorius.Instance.LoadHighScores();
        }
        WriteTheHighScores();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    
    public int GetScore()
    {
        return m_Points;
    }

    private void WriteTheHighScores()
    {
        string str = "";
        str += NewLine("HIGH SCORES :");

        if (Memorius.Instance != null)
        {
            string[] names = Memorius.Instance.HighScoreNames;
            int[] scores = Memorius.Instance.HighScoreNumbers;
            for (int i = 0; i < names.Length; i++)
            {
                str += NewLine(names[i] + " " + scores[i]);
            }
        }
        else
        {
            str += NewLine("Error");
            str += NewLine("Menu was skipped");
        }

        highScoreText.text = str;
    }

    private string NewLine(string str)
    {
        return str + System.Environment.NewLine;
    }

    private void ConstructTheBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    void SetPlayerName()
    {
        string str;

        if (Memorius.Instance == null)
        {
            str = "Error";
        }
        else
        {
            str = Memorius.Instance.PlayerName;
        }

        nameText.text = "Name : " + str;
    }
}
