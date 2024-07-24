using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class QuestionManager : MonoBehaviour
{
    public List<QuestionAnswer> questionSet;
    public int currentQuestion;
    public GameObject[] buttons;
    public TextMeshProUGUI questionText ;
    public TextMeshProUGUI questionNum ;
    public float maxSize = 50f; // Maximum font size
    public float minSize = 30f; // Minimum font size
    public float sizePerCharacter = 1f;
    public int num = 1;
    public int questionIndex = 0;
    public CalculateWinner cW;
    public int correctAnsButton = 0;
    public string[] playersNames = new string[4];

    [SerializeField]
    public ScoreSaver scoreSaver;
    private void Start()
    {
        genQuestion();
    }

    public void genQuestion()
    {
        if (num == questionSet.Count + 1)
        {
            scoreSaver.savedScores = ScoreBoard.currentScores;
            foreach (GameObject players in ScoreBoard.players)
            {
                playersNames[players.GetComponent<Score>().colourIndex] = players.GetComponent<Score>().playerName.text;
            }
            scoreSaver.players = playersNames;

            cW.CalculateWin();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            questionNum.text = "Question: " + num;
            currentQuestion = questionIndex;
            questionText.text = questionSet[currentQuestion].Question;
            setAns();
            //questionSet.RemoveAt(currentQuestion);
        }
    }

    private void setAns()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.GetComponent<CorrectAnswerHolder>().correctAns = false;
            buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questionSet[currentQuestion].Answers[i];


            float newSize = Mathf.Clamp(maxSize - (sizePerCharacter * buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Length), minSize, maxSize);

            // Apply the new font size to the text component
            buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = newSize;
            if (questionSet[currentQuestion].correctAns == i)
            {
                Debug.Log("Position of correct answer" + i);
                correctAnsButton = i;
                buttons[i].GetComponent<CorrectAnswerHolder>().correctAns = true;
            }
        }
    }

    public void resetUI()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            //buttons[i].GetComponent<Button>().n = Color.grey;
        }

        num++;
        questionIndex++;
        Invoke(nameof(genQuestion), 1f);
    }

    public void NextQuestion()
    {
        //Increment the current question index
        currentQuestion++;
        // Check if we've gone past the end of the question set
        if (currentQuestion >= questionSet.Count)
        {
            // If so, wrap around to the start of the question set
            currentQuestion = 0;
        }

        // Update the question text
        questionText.text = questionSet[currentQuestion].Question;
        float newSize = Mathf.Clamp(maxSize - (sizePerCharacter * questionText.text.Length), minSize, maxSize);

        // Apply the new font size to the text component
        questionText.fontSize = newSize;

        // Update the answers
        setAns();
    }


}