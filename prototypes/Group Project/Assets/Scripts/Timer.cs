using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    // 3 mins
    private float countdownTime = 180f;
    private bool timerIsRunning = false;
    public ScoreLogic CalcWinner;
    public string[] playersNames = new string[4];

    [SerializeField]
    public ScoreSaver scoreSaver;

    void Start()
    {
        StartTimer();
    }

    // Timer for game
    void Update()
    {
        if (timerIsRunning)
        {
            float elapsedTime = Time.time - startTime;
            float timeRemaining = Mathf.Clamp(countdownTime - elapsedTime, 0f, countdownTime);
            //Debug.Log("Time remaining should be " + timeRemaining + "s");
            UpdateTimerText(timeRemaining);
            if (timeRemaining <= 0f)
            {
                StopTimer();
            }
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        timerIsRunning = true;
    }

    public void StopTimer()
    {
        timerIsRunning = false;
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            scoreSaver.savedScores = ScoreBoard.currentScores;
            foreach (GameObject players in ScoreBoard.players)
            {
                playersNames[players.GetComponent<Score>().colourIndex] = players.GetComponent<Score>().playerName.text;
            }
            scoreSaver.players = playersNames;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    // Format timer
    void UpdateTimerText(float timeRemaining)
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}