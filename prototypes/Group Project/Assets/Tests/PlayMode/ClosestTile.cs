using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class ClosestTileTest
{
    private GameBoardManager gameBoardManager;
    private int width = 6;
    private int height = 6;

    [UnitySetUp]
    public IEnumerator SetUpScene()
    {
        

        // Wait for the scene to load
        yield return null;
        GameObject boardManagerObject = new GameObject("GameBoardManager");
        gameBoardManager = boardManagerObject.AddComponent<GameBoardManager>();

        GameObject camera = new GameObject();
        Camera cameraComponent = camera.AddComponent<Camera>();
        gameBoardManager.centerCam = cameraComponent.transform;
       //
       //gameBoardManager.width = width;
       /// gameBoardManager.height = height;
        yield return null;
    }


    [UnityTest]
    public IEnumerator TestOutofBoundsTiles()
    {
        yield return null;
        Vector2 outOfBoundsPosition = new Vector2(width + 1, height + 1);
        Tile outOfBoundsTile = gameBoardManager.GetTile(outOfBoundsPosition);

        Assert.IsNull(outOfBoundsTile, "Tile should not exist at out of bounds position.");
    }

    
    [UnityTest]
    public IEnumerator TestClosestTile()
    {
        yield return null;
        /*
        Vector2 testPosition = new Vector2(2, 2);
        Tile closestTile = gameBoardManager.GetTile(testPosition);

        Assert.IsNotNull(closestTile, "No tile found at test position.");
        Assert.AreEqual(testPosition, new Vector2(closestTile.transform.position.x, closestTile.transform.position.y), "Tile found is not at the expected position.");
        */
    }

    [UnityTest]
    public IEnumerator TestTileColourRed()
    {
        
        yield return null;
        /*
        gameBoardManager.getColour(0, 3);
        int tileColour = gameBoardManager.setColour;
        Assert.AreEqual(0, tileColour);
        */
    }

    [UnityTest]
    public IEnumerator TestTileColourBlue()
    {
        yield return null;

        gameBoardManager.getColour(5, 3);
        int tileColour = gameBoardManager.setColour;
        Assert.AreEqual(1, tileColour);
    }

    [UnityTest]
    public IEnumerator TestTileColourGreen()
    {
        yield return null;
        /*
        gameBoardManager.getColour(0, 0);
        int tileColour = gameBoardManager.setColour;
        Assert.AreEqual(2, tileColour);*/
    }


    [UnityTest]
    public IEnumerator TestTileColourYellow()
    {
        yield return null;
        /*
        gameBoardManager.getColour(5, 0);
        int tileColour = gameBoardManager.setColour;
        Assert.AreEqual(3, tileColour);*/
    }


}
