using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClicked : MonoBehaviour
{
    // Reference to the button's text component
    private TextMeshProUGUI buttonText;
    public ScoreLogic checkValid;

    void Start()
    {
        // Get the text component of the button
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        // List to click
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    // Method to handle button click
    void OnButtonClick()
    {

    }
}
