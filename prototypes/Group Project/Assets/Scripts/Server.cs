using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Server : NetworkManager
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

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        connectedPlayers++;
        Debug.Log("JOINED");


        if (connectedPlayers >= 2)
        {
            Debug.Log("Two players connected. Starting SQUAREIT!");
            StartSquareIt();
        }
    }


    public override void OnClientDisconnect()
    {
        base.OnClientDisconnect();
        Debug.Log("Disconnected from server");

        connectedPlayers--;
    }

    /*

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
    */

    public void StartSquareIt()
    {
        // Call SquareIt here
        ServerChangeScene("SampleScene");
        Debug.Log("SquareIt game started");
    }

}
