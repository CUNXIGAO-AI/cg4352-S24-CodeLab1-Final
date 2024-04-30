using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public TMP_InputField playerNameInputField;
    public Button playerNameBotton;
    public TextMeshProUGUI scoreListDisplay;
    public List<ScorePlayer> scores = new List<ScorePlayer>();

    public Canvas canvasEndScene;
    public GameObject playerInputObject;

    private string FILE_PATH;
    
    
    // Start is called before the first frame update
    void Start()
    {
        FILE_PATH = Application.dataPath + "/Data/Scores.txt";
        
        scoreListDisplay.text = "";
        playerNameBotton.onClick.AddListener(SubmitScore);
        
        LoadScores();
    }

    void LoadScores()
    {
        if (File.Exists(FILE_PATH))
        {
            string content = File.ReadAllText(FILE_PATH);
            string[] lines = content.Split('\n');
            scores.Clear();

            foreach (var line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        string name = parts[0];
                        float score = float.Parse(parts[1]);
                        scores.Add(new ScorePlayer(name, score));
                    }
                }
            }
        }
    }

    void SaveScores()
    {
        string content = "";
        foreach (var score in scores)
        {
            content += score.name + "," + score.score + "\n";
        }
        File.WriteAllText(FILE_PATH, content);
    }

    void SubmitScore()
    {
        string name = playerNameInputField.text;
        float score = GameManager.instance.TimerScore;
        
        scores.Add(new ScorePlayer(name , score));
        SortAndDisplayScores(score);
        playerNameInputField.text = "";
        SaveScores();
        
        playerInputObject.SetActive(false);
    }

    void SortAndDisplayScores(float currentScore)
    {
        scores.Sort((a, b) => a.score.CompareTo(b.score));
        int currentIndex = scores.FindIndex(x => x.score == currentScore);
        
        string displayText = "YOUR SCORE\n" + "\n";
        int displayStart = Mathf.Max(currentIndex - 3, 0);
        int displayEnd = Mathf.Min(currentIndex + 3, scores.Count - 1);
        
        for (int i = displayStart; i <= displayEnd; i++)
        {
            string rank = (i + 1) + ". ";
            
            if (i == currentIndex)
            {
                displayText += ">>> " + rank + scores[i].name + ": " + scores[i].score.ToString("F3") + " <<<\n";
            }
            else
            {
                displayText += rank + scores[i].name + ": " + scores[i].score.ToString("F3") + "\n";
            }
        }
        
        scoreListDisplay.text = displayText;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.SceneState == "EndScene")
        {
            canvasEndScene.enabled = true;
        }
        else
        {
            scoreListDisplay.text = "";
            playerInputObject.SetActive(true);
            canvasEndScene.enabled = false;
        }
    }
}
