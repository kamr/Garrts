using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class spawnUnits : NetworkBehaviour {
	GameObject[] allPlayers;

	public string unitOne;



	void Start () {
		//NetworkServer.SpawnWithClientAuthority (g, Network.connections [0].);

	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {

			Vector3 positionToSend = new Vector3 ();

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				positionToSend = hit.point;
			}

			NetworkIdentity i = gameObject.GetComponent<NetworkIdentity> ();
			CmdSpawnAUnit (this.gameObject,positionToSend,unitOne);

		}
	}
	//unity weirdness, this NEEDS to have the Cmd part of CmdSpawnAUnit prefixing it
	//god knows why theyre using method names as part of their code generation.......
	[Command] void CmdSpawnAUnit(GameObject player,Vector3 mousePosition,string unitStringToSpawn)  {

		GameObject unitToSpawn = Resources.Load (unitStringToSpawn) as GameObject;
		GameObject instantiated = (GameObject)Instantiate (unitToSpawn, mousePosition, Quaternion.identity);
		instantiated.transform.parent = this.transform;
		NetworkServer.SpawnWithClientAuthority (instantiated, player);

	}
}
