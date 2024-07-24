using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateWinner : MonoBehaviour
{
    private int redAmount = 0;
    private int greenAmount = 0;
    private int blueAmount = 0;
    private int yellowAmount = 0;
    private Tile tempTile;
    public GameBoardManager boardManager;
    public void CalculateWin()
    {
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                Debug.Log("CHECKING IN CALCU WIN");
                tempTile = boardManager.GetTile(new Vector2(x, y));
                int color = tempTile.currentCol;
                switch (color)
                {
                    case 0:
                        redAmount++;
                        break;
                    case 1:
                        blueAmount++;
                        break;
                    case 2:
                        greenAmount++;
                        break;

                    case 3:
                        yellowAmount++;
                        break;

                    default:
                        Debug.LogError("Unknown color");
                        break;
                }

            }
        }

        int winningColour = FindMax(redAmount, blueAmount, greenAmount, yellowAmount);
        Debug.Log("WINNDER COLOUR IS" + winningColour);

    }


    public int FindMax(int a, int b, int c, int d)
    {
        int max = a; // Assume the first number is the maximum
        int colour = 0;
        // Compare with the remaining numbers
        if (b > max)
        {
            max = b;
            colour = 1;
        }
        if (c > max)
        {
            max = c;
            colour = 2;
        }
        if (d > max)
        {
            max = d;
            colour = 3;
        }

        return colour;
    }
}
