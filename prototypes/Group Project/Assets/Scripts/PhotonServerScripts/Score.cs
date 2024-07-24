using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TMP_Text playerName;
    public TMP_Text playerScore;
    public Image backgroundColour;
    public int colourIndex;
    public float maxSize = 90f; // Maximum font size
    public float minSize = 50f; // Minimum font size
    public float sizePerCharacter = 5f;

    public void setPlayerDetails(string _playerName, int colour)
    {

        playerName.text = _playerName;
        float newSize = Mathf.Clamp(maxSize - (sizePerCharacter * playerName.text.Length), minSize, maxSize);

        // Apply the new font size to the text component
        playerName.fontSize = newSize;
        colourIndex = colour;
        SetColour(colour);
    }

    private void SetColour(int colour)
    {
        switch (colour)
        {
            case 0:
                backgroundColour.color = Color.red;
                break;
            case 1:
                backgroundColour.color = Color.blue;
                break;
            case 2:
                backgroundColour.color = Color.green;
                break;

            case 3:
                backgroundColour.color = Color.yellow;
                break;

            default:
                Debug.LogError("Unknown color");
                break;
        }
    }

}
