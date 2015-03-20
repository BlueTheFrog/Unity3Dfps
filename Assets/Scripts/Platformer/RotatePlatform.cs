using UnityEngine;
using System.Collections;

public class RotatePlatform : MonoBehaviour
{

	public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.rotation = Quaternion.Euler (0f, 0f, transform.rotation.eulerAngles.z + speed * Time.deltaTime);	
	}
}
