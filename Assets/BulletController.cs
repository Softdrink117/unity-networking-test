using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	void OnCollisionEnter(Collision collision){
		GameObject hit = collision.gameObject;
		PlayerHealth health = hit.GetComponent<PlayerHealth>();
		if(health != null) health.takeDamage(5);

		Destroy(gameObject);
	}
}
