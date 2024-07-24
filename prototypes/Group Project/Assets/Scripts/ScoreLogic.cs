using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ScoreLogic : MonoBehaviour
{
    private int playerColour = 0;
    private int playerPos = -1;
    [SerializeField] public Color red;
    private List<Vector2> targetPositions = new List<Vector2>();
    private int[] colourAnswers = new int[4];
    public ScoreBoard sB;

    private int width, height = 6;
    public GameBoardManager gameManager;
    [SerializeField]
    public SpriteRenderer squareRender;

    [SerializeField]
    public Image[] playerIcons = new Image[4];
    public Image iconHighlighter;

    public ParticleSystem streakParticle;
    private bool currentAnswer = false;
    public Dictionary<int, int> playerInfo;


    private bool playerStreak = true;
    private List<List<ParticleSystem>> streakArrays;
    private List<ParticleSystem> streakList;
    private bool[] playersOnStreak = new bool[4];
    private int[] streakValues = new int[4];

    private void Start()
    {
        playerInfo = new Dictionary<int, int>();
        targetPositions.Add(new Vector2(2, 3));
        targetPositions.Add(new Vector2(3, 3));
        targetPositions.Add(new Vector2(2, 2));
        targetPositions.Add(new Vector2(3, 2));

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            // Check if the player has the "playerAvatar" custom property
            if (player.Value.CustomProperties.ContainsKey("playerAvatar"))
            {
                // Get the value of the "playerAvatar" custom property for the player
                int playerAvatarColour = (int)player.Value.CustomProperties["playerAvatar"];
                playerInfo.Add(player.Key, playerAvatarColour);
            }
        }

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("playerAvatar"))
        {
            // Get the value of the "playerAvatar" custom property for the local player
            playerColour = (int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"];
            playerPos = playerColour;
            RectTransform rectTransformToMove = playerIcons[playerColour].rectTransform;
            RectTransform rectTransformReference = iconHighlighter.rectTransform;

            // Set the position of the image to match the position of the reference image
            rectTransformReference.anchoredPosition = rectTransformToMove.anchoredPosition;
        }

        streakList = new List<ParticleSystem>();
        streakArrays = new List<List<ParticleSystem>>();

        for (int i = 0; i < 4; i++)
        {
            streakArrays.Add(new List<ParticleSystem>());
            streakValues[i] = 0;
        }
    }

    public void checkAnswer()
    {
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            if (player.Value.CustomProperties.ContainsKey("Answer"))
            {
                if ((int)player.Value.CustomProperties["Answer"] == 1)
                {
                    playerInfo.TryGetValue(player.Key, out int val);
                    colourAnswers[val] = 1;
                    streakValues[val] += 1;
                    Debug.Log("Correct player colour" + val);
                    Debug.Log("PLAYER" + player.Value.NickName + " ANSWER IS " + colourAnswers[val]);
                    /*if ((player.Key-1) == playerColour)
                    {
                        currentAnswer = true;
                    }*/
                }
                else
                {
                    playerInfo.TryGetValue(player.Key, out int val);
                    colourAnswers[val] = -1;
                    streakValues[val] = 0;
                    /*if ((player.Key - 1) == playerColour)
                    {
                        currentAnswer = false;
                    }*/
                }
            }
        }

        sB.UpdateScores(colourAnswers);
        int temp = playerColour;
        for (int i = 0; i < colourAnswers.Length; i++)
        {
            if (colourAnswers[i] == 1)
            {
                playerColour = i;
                Debug.Log("PLAYER COLOUR IS" + playerColour);

                playerPos = playerColour;
                ChangeCenterMostSquare();
            }
        }

        playerColour = temp;
        playerPos = temp;
        // Apply streak
        for (int i = 0; i < 4; i++)
        {
            if (streakValues[i] >= 3)
            {
                playersOnStreak[i] = true;
                applyStreak(i);
            }
            else
            {
                playersOnStreak[i] = false;
                if (streakArrays[i].Count > 0)
                {
                    removeStreak(i);
                }
            }
        }
    }

    // Sub-grids
    private List<Vector2Int[]> subgrids = new List<Vector2Int[]>()
    {
        new Vector2Int[] { new Vector2Int(0, 3), new Vector2Int(2, 5) },  // Top-left sub-grid
        new Vector2Int[] { new Vector2Int(3, 3), new Vector2Int(5, 5) },   // Top-right sub-grid
        new Vector2Int[] { new Vector2Int(0, 0), new Vector2Int(2, 2) },  // Bottom-left sub-grid
        new Vector2Int[] { new Vector2Int(3, 0), new Vector2Int(5, 2) }   // Bottom-right sub-grid
    };

    // Target position of red square
    private Vector2 targetPosition;

    // Function to calculate Manhattan distance between two points
    private int ManhattanDistance(Vector2Int point1, Vector2Int point2)
    {
        return Mathf.Abs(point1.x - point2.x) + Mathf.Abs(point1.y - point2.y);
    }

    // Function to calculate closest positions for each sub-grid
    private void ChangeCenterMostSquare()
    {
        int subgridIndex = 0;
        Dictionary<Vector2Int, char> closestPositions = new Dictionary<Vector2Int, char>();
        targetPosition = targetPositions[playerColour];

        foreach (var subgrid in subgrids)
        {
            // Don't change subgrid of winning player
            if (subgrid != subgrids[playerPos])
            {
                int minDistance = int.MaxValue;
                Vector2Int closestPosition = Vector2Int.zero;

                for (int x = subgrid[0].x; x <= subgrid[1].x; x++)
                {
                    for (int y = subgrid[0].y; y <= subgrid[1].y; y++)
                    {
                        Vector2Int currentPoint = new Vector2Int(x, y);
                        int distance = ManhattanDistance(currentPoint, Vector2Int.FloorToInt(targetPosition));
                        Tile tileCol = gameManager.GetTile(new Vector2(currentPoint.x, currentPoint.y));
                        SpriteRenderer tileRenderers = tileCol.GetComponent<SpriteRenderer>();

                        // Find shortest distance for each person who loses
                        if (distance < minDistance && tileCol.currentCol != playerColour)
                        {
                            if (tileCol.currentCol == subgridIndex)
                            {
                                if (colourAnswers[subgridIndex] != 1)
                                {
                                    minDistance = distance;
                                    closestPosition = currentPoint;
                                }
                            }
                        }
                    }
                }

                //closestPositions.Add(closestPosition, 'C');  // Mark as closest position
                // Change sprite of tile being taken
                Tile tileToChange = gameManager.GetTile(new Vector2(closestPosition.x, closestPosition.y));
                if (tileToChange != null && minDistance != int.MaxValue)
                {
                    Debug.Log("INDEX IS " + subgridIndex + " VALUE OF PLAYER ANSWER " + colourAnswers[subgridIndex]);
                    Debug.Log("SUBGRID INDEX IS " + subgridIndex + " TAKING TILE AT " + closestPosition.x + ", " + closestPosition.y);
                    tileToChange.takeTile(tileToChange.GetComponent<SpriteRenderer>(), playerColour);
                }
            }
            subgridIndex += 1;
        }

        foreach (var position in closestPositions)
        {
            Debug.Log("Closest Position: " + position.Key + "," + position.Value);
        }
    }

    private void applyStreak(int colour)
    {
        Color partCol = getParticleColour(colour);

        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                Tile tileToChange = gameManager.GetTile(new Vector2(x, y));
                if (tileToChange.currentCol == colour)
                {
                    ParticleSystem newStreak = Instantiate(streakParticle, new Vector3(x, y), Quaternion.identity);
                    ParticleSystem.MainModule mainModule = newStreak.main;
                    mainModule.startColor = partCol;
                    newStreak.Play();
                    streakList.Add(newStreak);
                    streakArrays[0] = streakList;
                }
            }
        }
    }

    // Remove streak if someone loses their streak
    private void removeStreak(int i)
    {
        List<ParticleSystem> streakList = streakArrays[i];
        foreach (ParticleSystem streakParticleSystem in streakList)
        {
            Destroy(streakParticleSystem.gameObject);
        }
        streakList.Clear();
    }

    private Color getParticleColour(int i)
    {
        Color particleCol = Color.white;
        switch (i)
        {
            case 0:
                particleCol = Color.red;
                break;
            case 1:
                particleCol = Color.blue;
                break;
            case 2:
                particleCol = Color.green;
                break;
            case 3:
                particleCol = Color.yellow;
                break;
            default:
                Debug.LogError("Unknown color");
                break;
        }
        return particleCol;
    }
}