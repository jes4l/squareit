using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningCondition : MonoBehaviour
{

    public TMP_Text FirstScore;
    public TMP_Text SecondScore;
    public TMP_Text ThirdScore;
    public TMP_Text FirstName;
    public TMP_Text SecondName;
    public TMP_Text ThirdName;
    private int[] scores = new int[4];
    [SerializeField]
    public ScoreSaver scoreSaver;
    [SerializeField]
    public string[] playerNames = new string[4];
    private List<KeyValuePair<string, int>> playerScores;
    public SpriteRenderer background;
    void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scores = scoreSaver.savedScores;
        playerNames = scoreSaver.players;

        // Create a list of KeyValuePair<int, KeyValuePair<string, int>> to hold index, player names, and scores
        List<KeyValuePair<int, KeyValuePair<string, int>>> playerScoresWithIndex = new List<KeyValuePair<int, KeyValuePair<string, int>>>();

        // Populate the playerScoresWithIndex list
        for (int i = 0; i < scores.Length; i++)
        {
            playerScoresWithIndex.Add(new KeyValuePair<int, KeyValuePair<string, int>>(i, new KeyValuePair<string, int>(playerNames[i], scores[i])));
        }

        // Sort the playerScoresWithIndex list based on scores in descending order
        playerScoresWithIndex.Sort((x, y) => y.Value.Value.CompareTo(x.Value.Value));

        // Find the index of the top player before sorting
        int topPlayerIndex = playerScoresWithIndex[0].Key;
        ChangeBackground(topPlayerIndex);

        // Display top three scores
        if (playerScoresWithIndex.Count >= 1)
        {
            FirstScore.text = playerScoresWithIndex[0].Value.Value + "pts";
            FirstName.text = playerScoresWithIndex[0].Value.Key;
        }
        if (playerScoresWithIndex.Count >= 2)
        {
            SecondScore.text = playerScoresWithIndex[1].Value.Value + "pts";
            SecondName.text = playerScoresWithIndex[1].Value.Key;
        }
        if (playerScoresWithIndex.Count >= 3)
        {
            ThirdScore.text = playerScoresWithIndex[2].Value.Value + "pts";
            ThirdName.text = playerScoresWithIndex[2].Value.Key;
        }

        // Schedule returning to the lobby scene after 10 seconds
        Invoke("ReturnToLobby", 10f);
    }

    private void ReturnToLobby()
    {
        PhotonNetwork.LoadLevel("Lobby");
        Destroy(gameObject);
    }

    private void ChangeBackground(int colour)
    {
        switch (colour)
        {
            case 0:
                background.color = Color.red;
                break;
            case 1:
                background.color = Color.blue;
                break;
            case 2:
                background.color = Color.green;
                break;

            case 3:
                background.color = Color.yellow;
                break;

            default:
                Debug.LogError("Unknown color");
                break;
        }
    }
}