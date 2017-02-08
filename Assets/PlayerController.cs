using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerController : NetworkBehaviour {

	private Transform self;

	public GameObject bulletPrefab;

	[SerializeField]
	private Transform bulletSpawn;

	[SerializeField]
	private float moveSpeedX = 150.0f;

	[SerializeField]
	private float moveSpeedZ = 3.0f;

	void Awake () {
		self = gameObject.GetComponent(typeof(Transform)) as Transform;
	}

	// Color the local player
	public override void OnStartLocalPlayer(){
		Renderer rend = gameObject.GetComponent(typeof(Renderer)) as Renderer;
		rend.material.color = Color.blue;
	}
	
	void Update () {

		// Verify that we are only controlling the local player on this instance
		if(!isLocalPlayer) return;

		float x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeedX;
		float z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeedZ;

		self.Rotate(0,x,0);
		self.Translate(0,0,z);

		// If the player presses space, fire
		if(Input.GetKeyDown(KeyCode.Space)) CmdFire();
	}

	// Create bullet from prefab, apply force to it, and destroy it after a delay
	[Command]
	void CmdFire(){
		// Instantiate
		GameObject bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6.0f;

		// Spawn the bullet on Clients
		NetworkServer.Spawn(bullet);

		Destroy(bullet, 2.0f);
	}
}
