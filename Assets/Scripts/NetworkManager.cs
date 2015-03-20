using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviour 
{
	private GameObject respawnCam;

	public Text connectionText;

	public GameObject player;

	private Vector3 spawnPos = new Vector3 (90.1f, 1.2f, 67.7f);
	private Quaternion spawnRot = new Quaternion (0f, 1f, 0f, -0.1f);

	// Use this for initialization
	void Start () 
	{
		respawnCam = GameObject.Find ("Respawn Camera");
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

	void SpawnPlayer (float respawnTime)
	{
		Debug.Log("hi");
		respawnCam.SetActive (true);
		StartCoroutine ("RespawnPlayer", respawnTime);
	}

	IEnumerator RespawnPlayer(float respawnTime)
	{
		yield return new WaitForSeconds(respawnTime);
		PhotonNetwork.Instantiate ("FirstPersonCharacterController", spawnPos, spawnRot, 0);
		player.GetComponent<PlayerNetworkManager> ().RespawnMe += SpawnPlayer;
		respawnCam.SetActive (false);
	}
}