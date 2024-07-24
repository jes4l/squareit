using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;
    public TMP_Text roomName;
    public GameObject roomPanel;
    public GameObject lobbyPanel;
    public RoomItem roomItemPrefab;
    List<RoomItem> roomItems = new List<RoomItem>();
    public Transform contentObject;
    public float timeBetweenUpdatingRoom = 1.5f;
    float nextUpdateTime;
    public float maxSize = 150f; // Maximum font size
    public float minSize = 100f; // Minimum font size
    public float sizePerCharacter = 5f;
    public List<PlayerItem> playeritemsList = new List<PlayerItem>();
    public PlayerItem playerItemPrefab;
    public Transform playerItemParent;
    public GameObject startButton;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void onClickCreate()
    {
        if (roomNameInput.text.Length >= 1)
        {
            float newSize = Mathf.Clamp(maxSize - (sizePerCharacter * roomName.text.Length), minSize, maxSize);

            // Apply the new font size to the text component
            roomName.fontSize = newSize;

            PhotonNetwork.CreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = 4, BroadcastPropsChangeToAll = true });
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdatingRoom;
        }
    }

    private void UpdateRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomItem item in roomItems)
        {
            Destroy(item.gameObject);
        }
        roomItems.Clear();

        foreach (RoomInfo roomInfo in roomList)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.setRoomName(roomInfo.Name);
            roomItems.Add(newRoom);
        }
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void OnClickRoomLeave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }


    public void UpdatePlayerList()
    {
        foreach (PlayerItem player in playeritemsList)
        {
            Destroy(player.gameObject);
        }
        playeritemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerItem newPlayer = Instantiate(playerItemPrefab, playerItemParent);
            newPlayer.setPlayerInfo(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                newPlayer.applyHighlightChange();
            }

            playeritemsList.Add(newPlayer);
        }
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    public void onClickPlayGame()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }
}