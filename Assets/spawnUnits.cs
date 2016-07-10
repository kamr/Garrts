using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class spawnUnits : MonoBehaviour {
	ArrayList allPlayers = new ArrayList();
	// Use this for initialization
	void Start () {
		print ("ggg");

		//NetworkServer.SpawnWithClientAuthority (g, Network.connections [0].);
	}
	void OnPlayerConnected(NetworkPlayer player) {
		print ("asdasdafsa");
		/*GameObject networkedPlayerObj = new GameObject();
		allPlayers.Add (networkedPlayerObj);

		GameObject g = Resources.Load ("unit1") as GameObject;

		NetworkServer.SpawnWithClientAuthority (g,networkedPlayerObj);
		NetworkServer.SpawnWithClientAuthority (g,networkedPlayerObj);
*/
	}
	// Update is called once per frame
	void Update () {
	
	}
}
