using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    private int connectedPlayers = 0;

    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("Server started");
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        Debug.Log("Server stopped");
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("Connected to server");

        connectedPlayers++;

        if (connectedPlayers >= 2)
        {
            Debug.Log("Two players connected. Starting SQUAREIT!");
            StartSquareIt();
        }
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Debug.Log("Disconnected from server");

        connectedPlayers--;
    }

    void StartSquareIt()
    {
        // Call SquareIt here
        Debug.Log("SquareIt game started");
    }
}
