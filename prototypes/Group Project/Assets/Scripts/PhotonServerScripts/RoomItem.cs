using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public TMP_Text roomName;
    LobbyManager manager;
    public float maxSize = 150f; // Maximum font size
    public float minSize = 100f; // Minimum font size
    public float sizePerCharacter = 5f;


    private void Start()
    {
        manager = FindAnyObjectByType<LobbyManager>();
    }
    public void setRoomName(string _roomName)
    {
        roomName.text = _roomName;
        float newSize = Mathf.Clamp(maxSize - (sizePerCharacter * roomName.text.Length), minSize, maxSize);

        // Apply the new font size to the text component
        roomName.fontSize = newSize;
    }
    public void onClickItem()
    {
        manager.JoinRoom(roomName.text);
    }
}
