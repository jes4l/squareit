using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField]
    public GameObject Score;
    public Transform LeaderBoard;
    public static List<GameObject> players = new List<GameObject>();

    public static int[] currentScores = new int[4];


    // Start is called before the first frame update
    void Start()
    {
        foreach (Player otherPlayer in PhotonNetwork.PlayerList)
        {
            if (otherPlayer.CustomProperties.ContainsKey("playerAvatar"))
            {
                GameObject Player = Instantiate(Score, LeaderBoard);
                Player.GetComponent<Score>().setPlayerDetails(otherPlayer.NickName, (int)otherPlayer.CustomProperties["playerAvatar"]);

                players.Add(Player);
            }
        }

        for (int i = 0; i < currentScores.Length; i++)
        {
            currentScores[i] = 9;
        }
    }

    public void UpdateScores(int[] lastAnswers)
    {
        int index = 0;
        int numOfCorrectAnswers = 0;
        for (int i = 0; i < currentScores.Length; i++)
        {
            if (lastAnswers[i] == 1)
            {
                numOfCorrectAnswers++;
            }
        }

        for (int i = 0; i < currentScores.Length; i++)
        {
            if (lastAnswers[i] == 1)
            {
                currentScores[i] += (4 - numOfCorrectAnswers);
            }
            else
            {
                if (currentScores[i] >= numOfCorrectAnswers && currentScores[i] > 0)
                {
                    currentScores[i] -= numOfCorrectAnswers;
                }
            }
        }

        foreach (GameObject player in players)
        {
            while (index < 4)
            {
                if (player.GetComponent<Score>().colourIndex == index)
                {
                    player.GetComponent<Score>().playerScore.text = "" + currentScores[index];
                }
                index++;
            }
            index = 0;
        }
    }
}