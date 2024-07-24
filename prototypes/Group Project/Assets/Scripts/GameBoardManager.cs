using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Tilemaps;
using static System.TimeZoneInfo;

public class GameBoardManager : MonoBehaviour
{
    Timer gameTimer;
    public ParticleSystem paintSplash;
    private float splashTime = 0.5f;
    [SerializeField] public int width, height;
    [SerializeField] private Tile gameTile;
    [SerializeField] public Transform centerCam;
    public int setColour;
    public Dictionary<Vector2, Tile> boardTiles;

    // Start is called before the first frame update
    void Start()
    {
        boardTiles = new Dictionary<Vector2, Tile>();
        CreateBoard();
        //Timer.StartTimer();

        //paintSplash.Play();
       // StartCoroutine(increaseSplash());
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Effect for when taking a tile
    private IEnumerator increaseSplash()
    {
        float timer = 0f;
        var shapeModule = paintSplash.shape;
        while (timer < splashTime && shapeModule.radius < 1.5f)
        {
            timer += Time.deltaTime;
            shapeModule.radius = Mathf.Lerp(0, 1.5f, (timer / splashTime));
            yield return new WaitForSeconds(0.01f);
        }
    } // Initial board

    private void CreateBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Create the grid
                var addTile = Instantiate(gameTile, new Vector3(x, y), Quaternion.identity);
                addTile.name = $"Tile {x} {y}";
                getColour(x, y);
                addTile.Init(setColour);
                boardTiles[new Vector2(x, y)] = addTile;
                //PhotonNetwork.Instantiate(addTile.)
            }
        }
        // Center camera to position of the grid
        centerCam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
        Debug.Log("Camera position should be: " + centerCam.transform.position);
    }

    public void getColour(int x, int y)
    {
        int halfWidth = width / 2;
        int halfHeight = height / 2;

        if (x < halfWidth && y >= halfHeight)
        {
            // Top-left quarter
            setColour = 0;
        }
        else if (x >= halfWidth && y >= halfHeight)
        {
            // Top-right quarter
            setColour = 1;
        }
        else if (x < halfWidth && y < halfHeight)
        {
            // Bottom-left quarter
            setColour = 2;
        }
        else
        {
            // Bottom-right quarter
            setColour = 3;
        }
    }

    // return tile
    public Tile GetTile(Vector2 pos)
    {
        if (boardTiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        else
        {
            Debug.Log("Invalid Tile Position");
            return null;
        }
    }
}
