using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
public class ModuleTest
{
    [UnitySetUp]
    public IEnumerator SetUpScene()
    {
        // Load Scene 0 (assuming it's the first scene in the build settings)
        SceneManager.LoadScene(0);

        // Wait for the scene to load
        yield return null;
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ModuleTestButton()
    {
        // Arrange
        GameObject gameObject = new GameObject();
        ModuleManager moduleManager = gameObject.AddComponent<ModuleManager>();

        // Capture the current build index
        int initialBuildIndex = SceneManager.GetActiveScene().buildIndex;

        // Act
        moduleManager.onClickMethod();

        // Wait for the next frame to allow SceneManager.LoadScene to take effect
        yield return null;

        // Assert
        int newBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Assert.AreEqual(initialBuildIndex + 1, newBuildIndex);
    }



    [UnityTest]
    public IEnumerator ModuleTestButtonPresence()
    {
        yield return null;

        // Assert

        /*
        Button[] buttons = GameObject.FindObjectsOfType<Button>();
        bool buttonExists = false;
        foreach (Button button in buttons)
        {
            if (button.gameObject.activeInHierarchy)
            {
                buttonExists = true;
                break;
            }
        }

        Assert.IsTrue(buttonExists, "A button does not exist in the scene.");
        */
    }
}
