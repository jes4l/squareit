using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreSaver", menuName = "Persistence")]
public class ScoreSaver : ScriptableObject
{
    public int[] savedScores = new int[4];
    public string[] players = new string[4];
}