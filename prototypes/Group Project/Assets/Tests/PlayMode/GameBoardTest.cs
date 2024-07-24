using NUnit.Framework;
using UnityEngine;

public class GameBoardTest
{
    // Reference to the game board manager
    private GameBoardManager gameBoardManager;

    [SetUp]
    public void Setup()
    {
        // Initialize the game board manager
        gameBoardManager = new GameBoardManager();
    }

    [Test]
    public void TestGridPosition()
    {
        // Iterate over the width and height of the game board
        for (int x = 0; x < gameBoardManager.width; x++)
        {
            for (int y = 0; y < gameBoardManager.height; y++)
            {
                // Get the tile at the current position
                Tile tile = gameBoardManager.GetTile(new Vector2(x, y));

                // Convert the tile's position to viewport coordinates
                Vector3 viewportPos = Camera.main.WorldToViewportPoint(tile.transform.position);

                // Check if the viewport coordinates are within the bounds of the viewport
                Assert.IsTrue(viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1);
            }
        }
    }
}
