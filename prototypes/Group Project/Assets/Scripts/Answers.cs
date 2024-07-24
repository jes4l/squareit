using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections.Generic;
using UnityEngine.UI;

public class Answers : MonoBehaviourPunCallbacks
{
    public QuestionManager qM;
    public GameObject background;
    public ScoreLogic sC;
    private bool correctAns;
    private Color originalCol;
    private int buttonPressCount = 0;
    private int updatedVals = 0;
    private int totalPlayers;
    private bool buttonPressed = false;
    private bool getAnswer = true;
    private bool calledFunc = true; // Flag to check if CheckAllPlayersUpdated has been called
    private bool checkAnswerCalled = false; // Flag to check if sC.checkAnswer() has been called
    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    private Player currentPlayer;
    private Button button;
    public Button[] buttons = new Button[4];
    Color temp;

    void Start()
    {
        totalPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        currentPlayer = PhotonNetwork.LocalPlayer;
        Debug.Log("NUMBER OF PLAYERS: " + totalPlayers);
    }

    // Method to call when the button is pressed
    public void OnButtonPressed(int buttonIndex)
    {
        GameObject clickedButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        button = clickedButton.GetComponent<Button>();
        button.interactable = false;



        correctAns = clickedButton.GetComponent<CorrectAnswerHolder>().correctAns;

        Debug.Log("PRESSED BUTTON");
        if (!buttonPressed)
        {
            buttonPressed = true;
            getAnswer = true;
            // Increment the button press count
            buttonPressCount += 1;
            photonView.RPC("SyncButtonPressCount", RpcTarget.AllBuffered, buttonPressCount); // Buffer the RPC call
        }
    }

    [PunRPC]
    private void SyncButtonPressCount(int count)
    {
        // Update the button press count
        buttonPressCount = count;

        // Check if all players have pressed the button
        if (buttonPressCount >= totalPlayers)
        {
            // Call your function here since all players have pressed the button
            SetAnswerProperty();
        }
    }

    private void SetAnswerProperty()
    {
        // Set the custom property based on whether the answer is correct or not
        int answerValue = correctAns ? 1 : -1;
        PhotonNetwork.LocalPlayer.CustomProperties["Answer"] = answerValue;
        playerProperties["Answer"] = answerValue;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);

        // Trigger UI reset and color change
        HandleAnswerVisuals(answerValue);
    }

    private void HandleAnswerVisuals(int answerValue)
    {
        originalCol = background.GetComponent<SpriteRenderer>().color;
        if (answerValue == 1) // Correct answer
        {
            background.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else // Incorrect answer
        {
            SetButtonColors();
        }

        // Reset UI and color after a delay
        Invoke("CheckAllPlayersUpdated", 0.5f);
    }

    private void ResetBackground()
    {
        buttonPressCount = 0;
        buttonPressed = false;
        background.GetComponent<SpriteRenderer>().color = originalCol;
        // READY FOR NEXT QUESTION
        checkAnswerCalled = false;
        button.interactable = true;
        ResetButtonColors();
    }

    private void CheckAllPlayersUpdated()
    {
        int newval = updatedVals;
        calledFunc = false;
        Debug.Log("updated vals: " + newval);
        // Check if the number of updated players is equal to the total number of players in the room
        if (newval == totalPlayers && getAnswer && !checkAnswerCalled)
        {
            checkAnswerCalled = true;
            Debug.Log("CALLED CHECK ANSWER");
            // Call your function here since all players have updated their answers
            sC.checkAnswer();
            Invoke("ResetBackground", 0.5f);
            qM.resetUI();
            updatedVals = 0; // Clear the list for future use
            getAnswer = false;
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps.ContainsKey("Answer") && targetPlayer.CustomProperties.ContainsKey("playerAvatar"))
        {
            if (targetPlayer.CustomProperties.ContainsKey("Answer"))
            {
                Debug.Log("PLAYER PROP UPDATED");
                updatedVals += 1; // Add the player to the updated list
                if (calledFunc)
                {
                    photonView.RPC("CheckAllPlayersUpdated", RpcTarget.AllBuffered, updatedVals); // Buffer the RPC call
                }
            }
        }
    }



    public void SetButtonColors()
    {
        // Loop through all buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            ColorBlock colors = buttons[i].colors;

            // Set the normal color to grey
            colors.normalColor = Color.grey;

            // Assign the modified ColorBlock back to the button
            buttons[i].colors = colors;
        }

        // Change the color of the correct answer button to green
        if (qM.correctAnsButton >= 0 && qM.correctAnsButton < buttons.Length)
        {
            ColorBlock correctColors = buttons[qM.correctAnsButton].colors;

            // Set the normal color to green
            correctColors.normalColor = Color.green;

            // Assign the modified ColorBlock back to the correct answer button
            buttons[qM.correctAnsButton].colors = correctColors;
        }


    }

    public void ResetButtonColors()
    {
        // Loop through all buttons
        for (int i = 0; i < buttons.Length; i++)
        {
            ColorBlock colors = buttons[i].colors;

            // Set the normal color to grey
            colors.normalColor = Color.white;

            // Assign the modified ColorBlock back to the button
            buttons[i].colors = colors;
        }
    }
}