//using System.Collections;
//using System.Collections.Generic;
//using NUnit.Framework;
//using UnityEngine;
//using UnityEngine.TestTools;
//using UnityEngine.UI;
//
//public class NextQuestionDisplayTests
//{
//    private QuestionManager questionManager;
//    private Button nextButton;
//
//    [SetUp]
//    public void Setup()
//    {
//        // Assuming you have a prefab for your QuestionManager
//        GameObject managerPrefab = Resources.Load<GameObject>("QuestionManager");
//        GameObject instance = Object.Instantiate(managerPrefab);
//        questionManager = instance.GetComponent<QuestionManager>();
//
//        // Assuming your next button is a child of your QuestionManager
//        nextButton = questionManager.transform.Find("NextButton").GetComponent<Button>();
//    }
//
//    [UnityTest]
//    public IEnumerator NextQuestionDisplaySimplePasses()
//    {
//        // Get the current question text
//        string initialQuestionText = questionManager.questionText.text;
//
//        // Simulate a button click
//        nextButton.onClick.Invoke();
//
//        // Wait for one frame
//        yield return null;
//
//        // Check that the question text has changed
//        string newQuestionText = questionManager.questionText.text;
//        Assert.AreNotEqual(initialQuestionText, newQuestionText);
//    }
//
//    [TearDown]
//    public void Teardown()
//    {
//        // Clean up
//        Object.Destroy(questionManager.gameObject);
//    }
//}
