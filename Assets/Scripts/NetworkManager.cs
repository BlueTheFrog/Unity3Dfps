using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour 
{

	public Text connectionText;

	// Use this for initialization
	void Start () 
	{
		Screen.showCursor = true;
		PhotonNetwork.logLevel = PhotonLogLevel.Full;
		PhotonNetwork.ConnectUsingSettings ("0.2");
	}
	
	// Update is called once per frame
	void Update () 
	{
		checkKeysPressed();
		connectionText.text = PhotonNetwork.connectionStateDetailed.ToString ();
	}

	void checkKeysPressed ()
	{
		if (Input.GetKeyDown ("escape"))
		{
			Application.Quit ();
		}
	}

	void OnJoinedLobby ()
	{
		RoomOptions ro = new RoomOptions (){isVisible = true, maxPlayers = 10};PhotonNetwork.JoinOrCreateRoom ("Default", ro, TypedLobby.Default);
	}
}