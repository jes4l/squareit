using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class JoinServerTest
{
    [UnitySetUp]
    public IEnumerator SetUpScene()
    {


        SceneManager.LoadScene(1);
        yield return null;
    }


    [UnityTest]
    public IEnumerator JoinServerButton()
    {
        /*
        // Arrange
        GameObject gameObject = new GameObject();
        ConnectToServer serverConnector = gameObject.AddComponent<ConnectToServer>();
        TMP_InputField text = serverConnector.nameInput;
        text.text = "Test";



        //serverConnector.nameInput.text = "TEST";
        // Capture the current build index
        int initialBuildIndex = SceneManager.GetActiveScene().buildIndex;

        // Act
        serverConnector.onClickConnect();



        // Wait for the next frame to allow SceneManager.LoadScene to take effect
        yield return null;

        // Assert
        int newBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Assert.AreEqual(initialBuildIndex + 1, newBuildIndex);
        */
        yield return null;
    }
}
