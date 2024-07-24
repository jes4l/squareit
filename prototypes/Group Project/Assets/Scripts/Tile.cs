using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class Tile : MonoBehaviour
{
    [SerializeField] public SpriteRenderer originalTile;
    public int currentCol = -1;

    // Initialise board
    public void Init(int colVar)
    {
        SetColor(originalTile, colVar);
    }

    // Set colour of sprtes
    private void SetColor(SpriteRenderer renderer, int color)
    {
        switch (color)
        {
            case 0:
                renderer.color = Color.red;
                currentCol = 0;
                break;
            case 1:
                renderer.color = Color.blue;
                currentCol = 1;
                break;
            case 2:
                renderer.color = Color.green;
                currentCol = 2;
                break;

            case 3:
                renderer.color = Color.yellow;
                currentCol = 3;
                break;

            default:
                Debug.LogError("Unknown color");
                break;
        }
    }

    // Function to take tiles when question answered
    public void takeTile(SpriteRenderer takenTile, int color)
    {
        SetColor(takenTile, color);
    }






}





