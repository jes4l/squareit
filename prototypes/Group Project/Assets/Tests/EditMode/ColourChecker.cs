using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ColourChecker
{
    // A Test behaves as an ordinary method
    [Test]
    public void ColourCheckerRed()
    {
        GameObject tileObject = new GameObject();
        Tile tile = tileObject.AddComponent<Tile>();
        tile.originalTile = tileObject.AddComponent<SpriteRenderer>();

        // Initialise Tile
        tile.Init(0);

        // Assert
        Assert.AreEqual(Color.red, tile.originalTile.color);
    }

    [Test]
    public void ColourCheckerBlue()
    {
        GameObject tileObject = new GameObject();
        Tile tile = tileObject.AddComponent<Tile>();
        tile.originalTile = tileObject.AddComponent<SpriteRenderer>();

        // Initialise Tile
        tile.Init(1);

        // Assert
        Assert.AreEqual(Color.blue, tile.originalTile.color);
    }

    [Test]
    public void ColourCheckerYellow()
    {
        GameObject tileObject = new GameObject();
        Tile tile = tileObject.AddComponent<Tile>();
        tile.originalTile = tileObject.AddComponent<SpriteRenderer>();

        // Initialise Tile
        tile.Init(3);

        // Assert
        Assert.AreEqual(Color.yellow, tile.originalTile.color);
    }

    [Test]
    public void ColourCheckerGreen()
    {
        GameObject tileObject = new GameObject();
        Tile tile = tileObject.AddComponent<Tile>();
        tile.originalTile = tileObject.AddComponent<SpriteRenderer>();

        // Initialise Tile
        tile.Init(2);

        // Assert
        Assert.AreEqual(Color.green, tile.originalTile.color);
    }
}