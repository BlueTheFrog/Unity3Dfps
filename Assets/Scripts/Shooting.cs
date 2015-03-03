using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shooting : MonoBehaviour
{
	private GameObject manager;

	private GameObject ammoText;
	private GameObject clipText;

	private GameObject gunImpactSmoke;
	private GameObject muzzleFlash;

	public GameObject player;

	public float attackInterval = 0.58f;
	public float reloadSpeed = 1.0f;
	public int clipSize = 6;
	public int ammoSize = 32;
	private int prevAmmo;
	private bool isManualReload = false;
	private bool isReloading = false;
	private bool timerIsOn = false;
	private float timer2;
	private int clip;
	private int ammo;
	private int prevClip;
	private Text clipText2;
	private Text ammoText2;

	void Awake ()
	{
		manager = GameObject.Find ("Network Manager");
		clipText = GameObject.Find ("Canvas/Clip_Text");
		ammoText = GameObject.Find ("Canvas/Ammo_Text");
		gunImpactSmoke = GameObject.Find ("Particle Systems/Gun Impact Smoke");
		muzzleFlash = GameObject.Find ("Muzzle Flash");
	}

	// Use this for initialization
	void Start () 
	{
		clipText2 = clipText.GetComponent<Text>();
		ammoText2 = ammoText.GetComponent<Text>();
		clipText2.text = clipSize.ToString();
		ammoText2.text = ammoSize.ToString();
		clip = clipSize;
		ammo = ammoSize;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Shoot();
		if (Input.GetKeyDown("p"))
		{
			PhotonNetwork.Destroy (player);
		}
	}
	
	void Shoot ()
	{
		// Delay between shots or reloading
		if (timerIsOn)
		{
			timer2 -= Time.deltaTime;
			if (timer2 <= 0)
			{
				timerIsOn = false;
				if (isReloading)
				{
					isReloading = false;
					if (isManualReload)
					{
						prevAmmo = ammo;
						ammo -= clipSize - clip;
						if (ammo < 0)
						{
							ammo = 0;
						}
						ammoText2.text = ammo.ToString();
						if (prevAmmo >= clipSize)
						{
							clip = clipSize;
						}
						else if (prevAmmo < clipSize)
						{
							clip += prevAmmo;
						}
						clipText2.text = clip.ToString();
						isManualReload = false;
					}
					else if (ammo >= clipSize)
					{
						ammo -= clipSize;
						ammoText2.text = ammo.ToString();
						clip += clipSize;
						clipText2.text = clip.ToString();
					}
					else if (ammo < clipSize)
					{
						clip += ammo;
						clipText2.text = clip.ToString();
						ammo = 0;
						ammoText2.text = ammo.ToString();
					}
				}
			}
		}
		
		// Shooting
		if(Input.GetMouseButton(0) && timerIsOn == false && clip > 0 && isManualReload == false)
		{
			clip -= 1;
			clipText2.text = clip.ToString();
			muzzleFlash.GetComponent<ParticleSystem>().Play();
			if (clip > 0)
			{
				timerIsOn = true;
				timer2 = attackInterval;
			}
			RaycastHit hit;
			if(Physics.Raycast(transform.position, transform.forward, out hit, 1000.0f))
			{
				gunImpactSmoke.transform.position = hit.point;
				gunImpactSmoke.GetComponent<ParticleSystem>().Play();
				if (hit.transform.tag == "Player")
				{
					PhotonNetwork.Destroy (hit.transform.gameObject);
				}
			}  
		}
		else if (clip <= 0 && timerIsOn == false && ammo > 0)
		{
			timerIsOn = true;
			timer2 = reloadSpeed;
			isReloading = true;
		}
		if (Input.GetKey("r") && timerIsOn == false && ammo > 0 && clip < clipSize)
		{
			timerIsOn = true;
			timer2 = reloadSpeed;
			isReloading = true;
			isManualReload = true;
		}
	}
}