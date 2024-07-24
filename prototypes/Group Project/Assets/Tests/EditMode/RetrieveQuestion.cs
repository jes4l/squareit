using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static UnityEditor.PlayerSettings;

public class RetrieveQuestion
{
    // A Test behaves as an ordinary method
    [Test]
    public void RetrieveQuestion_GetQuestion()
    {
        // Arrange
        GameObject questionObject = new GameObject();
        QuestionAnswer question = questionObject.AddComponent<QuestionAnswer>();
        question.Question = "When was the battle of hastings";

        GameObject questionHolderObject = new GameObject();
        QuestionManager questionManager = questionHolderObject.AddComponent<QuestionManager>();

        // Act
        questionManager.questionSet = new List<QuestionAnswer>(); // Initialize questionSet
        questionManager.questionSet.Add(question);

        // Assert
        Assert.AreEqual("When was the battle of hastings", questionManager.questionSet.First().Question);
    }

    [Test]
    public void RetrieveQuestion_GetAnswers()
    {
        // Arrange
        GameObject questionObject = new GameObject();
        QuestionAnswer question = questionObject.AddComponent<QuestionAnswer>();
        question.Answers = new string[] { "966", "1066", "1166", "1065" };
        question.correctAns = 1;

        GameObject questionHolderObject = new GameObject();
        QuestionManager questionManager = questionHolderObject.AddComponent<QuestionManager>();

        // Act
        questionManager.questionSet = new List<QuestionAnswer>(); // Initialize questionSet
        questionManager.questionSet.Add(question);

        // Assert correct answer
        Assert.AreEqual(1, questionManager.questionSet.First().correctAns);
        Assert.AreEqual("1066", questionManager.questionSet.First().Answers[questionManager.questionSet.First().correctAns]);
    }
}