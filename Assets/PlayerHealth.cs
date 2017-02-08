using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : NetworkBehaviour {

	public const int maxHealth = 100;
	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;

	public RectTransform healthBar;

	public void takeDamage(int amount){
		// Only calculate damage on the server side, and then synchronize the result with the clients
		if(!isServer) return;

		currentHealth -= amount;
		if(currentHealth <= 0){
			currentHealth = maxHealth;
			RpcRespawn();
		}

		
	}

	void OnChangeHealth(int cHealth){
		healthBar.sizeDelta = new Vector2(cHealth, healthBar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn(){
		if(!isLocalPlayer) return;

		transform.position = Vector3.zero;
	}
}
