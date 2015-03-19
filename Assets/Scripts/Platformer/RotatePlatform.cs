using UnityEngine;
using System.Collections;

public class RotatePlatform : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    transform.eulerAngles = new Vector3 (0f, 0f, tranform.eulerAngles.z);
	
	}
}
