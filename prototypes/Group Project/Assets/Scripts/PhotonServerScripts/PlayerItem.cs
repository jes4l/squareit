using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviourPunCallbacks

{
    public TMP_Text playerName;
    public Image playerBackground;
    public Color highlightColour;
    public GameObject leftArrow;
    public GameObject rightArrow;
    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();
    public Color playerColor;
    public Color[] avatarColor;
    Player player;
    private HashSet<int> avatarsInUse = new HashSet<int>(); // HashSet for faster lookup
    public float maxSize = 150f; // Maximum font size
    public float minSize = 100f; // Minimum font size
    public float sizePerCharacter = 5f;


    private void Start()
    {
        UpdateAvatarsInUse();
    }

    private void UpdateAvatarsInUse()
    {
        avatarsInUse.Clear();
        foreach (Player otherPlayer in PhotonNetwork.PlayerList)
        {
            if (otherPlayer.CustomProperties.ContainsKey("playerAvatar"))
            {
                int avatarIndex = (int)otherPlayer.CustomProperties["playerAvatar"];
                avatarsInUse.Add(avatarIndex);
            }
        }
    }

    public void setPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        float newSize = Mathf.Clamp(maxSize - (sizePerCharacter * playerName.text.Length), minSize, maxSize);

        // Apply the new font size to the text component
        playerName.fontSize = newSize;
        player = _player;
        UpdatePlayerItem(player);
    }

    public void applyHighlightChange()
    {
        // Find an available color index for the player
        int currentAvatarIndex = 0; // Default to the first color
        if (playerProperties.ContainsKey("playerAvatar"))
        {
            currentAvatarIndex = (int)playerProperties["playerAvatar"];
        }

        // Find the first available color index
        int newAvatarIndex = currentAvatarIndex;
        for (int i = 0; i < avatarColor.Length; i++)
        {
            int newIndex = (currentAvatarIndex + i) % avatarColor.Length;
            if (!avatarsInUse.Contains(newIndex))
            {
                newAvatarIndex = newIndex;
                break;
            }
        }

        // Set the player's avatar index and update avatarsInUse list
        playerProperties["playerAvatar"] = newAvatarIndex;
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
        avatarsInUse.Add(currentAvatarIndex); // Make the current avatar available again
        avatarsInUse.Remove(newAvatarIndex); // Mark the new avatar as in use

        // Set the player's background color to the chosen color
        playerBackground.color = avatarColor[newAvatarIndex];
        UpdateAvatarsInUse();

        leftArrow.SetActive(true);
        rightArrow.SetActive(true);

    }

    /*
    public void applyLeftArrowChange()
    {
        if ((int)playerProperties["playerAvatar"] == 0)
        {
            playerProperties["playerAvatar"] = avatarColor.Length - 1;
        }
        else
        {
            playerProperties["playerAvatar"] = (int) playerProperties["playerAvatar"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void applyRightArrowChange()
    {
        if ((int)playerProperties["playerAvatar"] == avatarColor.Length - 1)
        {
            playerProperties["playerAvatar"] = 0;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }
    */

    public void applyLeftArrowChange()
    {
        int currentAvatar = (int)playerProperties["playerAvatar"];
        int newAvatar = currentAvatar == 0 ? avatarColor.Length - 1 : currentAvatar - 1;

        // Check if the new avatar is not in use by other players
        if (!avatarsInUse.Contains(newAvatar))
        {
            // Avatar is available, update player's avatar and update avatarsInUse list
            playerProperties["playerAvatar"] = newAvatar;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
            avatarsInUse.Add(currentAvatar); // Make the current avatar available again
            avatarsInUse.Remove(newAvatar); // Mark the new avatar as in use
        }
    }

    public void applyRightArrowChange()
    {
        int currentAvatar = (int)playerProperties["playerAvatar"];
        int newAvatar = currentAvatar == avatarColor.Length - 1 ? 0 : currentAvatar + 1;

        // Check if the new avatar is not in use by other players
        if (!avatarsInUse.Contains(newAvatar))
        {
            // Avatar is available, update player's avatar and update avatarsInUse list
            playerProperties["playerAvatar"] = newAvatar;
            PhotonNetwork.SetPlayerCustomProperties(playerProperties);
            avatarsInUse.Add(currentAvatar); // Make the current avatar available again
            avatarsInUse.Remove(newAvatar); // Mark the new avatar as in use
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        UpdateAvatarsInUse();
        if (player == targetPlayer)
        {
            UpdatePlayerItem(targetPlayer);
        }

    }

    private void UpdatePlayerItem(Player _player)
    {
        if (_player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerBackground.color = avatarColor[(int)_player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)_player.CustomProperties["playerAvatar"];
        }
        else
        {
            playerProperties["playerAvatar"] = 0;
        }
    }
}