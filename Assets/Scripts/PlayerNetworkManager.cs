using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerNetworkManager : Photon.MonoBehaviour
{
	public delegate void Respawn(float time);
	public event Respawn RespawnMe;	

	public GameObject player;
	private GameObject healthText;
	public int health = 100;

	public float smoothing;

	public GameObject mainCam;
	private GameObject respawnCam;

	private Vector3 nextPos;
	private Quaternion nextRot;
	private Quaternion camRot;

	void Awake ()
	{
		respawnCam = GameObject.Find("/Respawn Camera");
		respawnCam.SetActive (false);
	}

	// Update is called once per frame
	void Update ()
	{
	}


	void Start ()
	{
		healthText = GameObject.Find ("Canvas/Health_Text");
		if (photonView.isMine)
		{
			GetComponent<UnitySampleAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
			GetComponentInChildren<Shooting>().enabled = true;
			foreach(Camera cam in GetComponentsInChildren<Camera>())
			{
				cam.enabled = true;
			}
		}
		else
		{
			StartCoroutine("UpdatePosition");
		}
	}

	IEnumerator UpdatePosition ()
	{
		while (true)
		{
		    transform.position = Vector3.Lerp (transform.position, nextPos, Time.deltaTime * smoothing);
		    transform.rotation = Quaternion.Lerp (transform.rotation, nextRot, Time.deltaTime * smoothing);
			mainCam.transform.rotation = Quaternion.Lerp (mainCam.transform.rotation, camRot, Time.deltaTime * smoothing);
		    yield return null;
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(mainCam.transform.rotation);
		}
		else if (stream.isReading)
		{
			nextPos = (Vector3)stream.ReceiveNext();
			nextRot = (Quaternion)stream.ReceiveNext();
			camRot = (Quaternion)stream.ReceiveNext();
		}
	}

	[RPC]
	public void DamageDelt (int damage)
	{
		health -= damage;
		healthText.GetComponent<Text> ().text = health.ToString ();
		if (health <= 0 && photonView.isMine)
		{
			Debug.Log(RespawnMe);
			if (RespawnMe != null)
			{
				RespawnMe(5f);
				Debug.Log("hi");
			}
			PhotonNetwork.Destroy (gameObject);
		}
	}
}
