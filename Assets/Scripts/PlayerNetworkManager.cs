﻿using UnityEngine;
using System.Collections;

public class PlayerNetworkManager : Photon.MonoBehaviour
{

	public GameObject player;
	public bool isDead;

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
		Respawn ();			
	}

	void Respawn ()
	{
		if (isDead)
		{
			player.SetActive(false);
			respawnCam.SetActive(true);
		}
	}

	void Start ()
	{
		if (photonView.isMine)
		{
			GetComponent<UnitySampleAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
			GetComponentInChildren<Shooting>().enabled = true;
			foreach(Camera cam in GetComponentsInChildren<Camera>()) cam.enabled = true;
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
			stream.SendNext(GetComponent<PlayerNetworkManager>().isDead);
		}
		else if (stream.isReading)
		{
			nextPos = (Vector3)stream.ReceiveNext();
			nextRot = (Quaternion)stream.ReceiveNext();
			camRot = (Quaternion)stream.ReceiveNext();
			isDead = (bool)stream.ReceiveNext();
		}
	}
}
