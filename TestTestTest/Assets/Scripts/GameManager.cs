using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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

    private float timer = 0f;
    private float lastTickTime;
    private bool timerToggle;
    public TextMeshProUGUI timerUI;
    public GameObject title;
    public AudioSource tickSound;
    public AudioClip WinningSound;
    

    public TextMeshProUGUI timerScoreUI;
    private float timerScore;
    public float TimerScore
    {
        get
        {
            return timerScore;
        }
    }

    private string sceneState;
    public string SceneState
    {
        get
        {
            return sceneState;
        }
    }

    public string GetCurrentSceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name;
    }
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        lastTickTime = 0f;
        timerToggle = false;

        timerScoreUI.text = "";

        sceneState = GetCurrentSceneName();
    }

    // Update is called once per frame
    void Update()
    {
        switch (sceneState)
        {
            case "StartScene":
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    LoadPlayScene();
                }
                break;
            
            case "PlayScene":
                timerToggle = true;
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    LoadStartScene();
                }
                if (timerToggle && Time.time >= lastTickTime + 1)
                {
                    tickSound.Play();
                    lastTickTime = Time.time;
                }
                break;
            
            case "EndScene":
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    timerScoreUI.text = "";
                    LoadStartScene();
                }
                break;
        }

        if (timerToggle)
        {
            timer += Time.deltaTime;
            timerUI.text = timer.ToString("F1");
        }
        else if (!timerToggle)
        {
            timer = 0;
            timerUI.text = "";
        }
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene("StartScene");
        timerToggle = false;
        Debug.Log("timer has been reset, timer = " + timer);
        
        sceneState = "StartScene";
        title.SetActive(true);
    }

    public void LoadPlayScene()
    {
        SceneManager.LoadScene("PlayScene");
        
        sceneState = "PlayScene";
        title.SetActive(false);
    }

    public void LoadEndScene()
    {
        timerScore = timer;
        timerScoreUI.text = timerScore.ToString("F1");
        timerToggle = false;
        SceneManager.LoadScene("EndScene");
        tickSound.PlayOneShot(WinningSound);
        
        sceneState = "EndScene";
    }
}
