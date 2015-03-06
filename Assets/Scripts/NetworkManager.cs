using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour 
{

	public Text connectionText;

	private Vector3 spawnPos = new Vector3 (90.1f, 1.2f, 67.7f);
	private Quaternion spawnRot = new Quaternion (0f, 1f, 0f, -0.1f);

	// Use this for initialization
	void Start () 
	{
		Screen.showCursor = false;
		PhotonNetwork.logLevel = PhotonLogLevel.Full;
		PhotonNetwork.ConnectUsingSettings ("0.1");
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
		RoomOptions ro = new RoomOptions (){isVisible = true, maxPlayers = 20};
		PhotonNetwork.JoinOrCreateRoom ("Default", ro, TypedLobby.Default);
	}

	void OnJoinedRoom ()
	{
		PhotonNetwork.Instantiate ("FirstPersonCharacterController", spawnPos, spawnRot, 0);
	}
}