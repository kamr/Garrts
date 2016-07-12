using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class unit : NetworkBehaviour {
	public GameObject circle;
	public int range = 1000000;
	public int spellRange = 1000000;
	bool selected = false;
	NavMeshAgent agent;
	bool aMoving = false;
	bool aClickPressed = false;
	int cooldown = 0;
	int cooldownTimer = 100;

	private Projector lightRing;
	string ownerID;

	// USEFUL LATER. this.netId IS OBJECTS ID ON NETWORK OR SOMETHING

	private Color pickColour(string id) {
		switch (id) {
			case "-1":
				return Color.blue;
			case "0":
				return Color.red;
			case "1":
				return Color.magenta;
			default:
				return Color.yellow;
		}
	}
	public void setOwner(string ownerID) { // To prevent hacking, could surround in a boolean and only allow run once
		//playerID = 
		//Debug.Log(Network.isClient);
		//ownerID = this.playerControllerId;
		//playerID = GetComponent<NetworkView> ().owner.ToString();
		//Debug.Log(this.playerControllerId + " " + this.isLocalPlayer + " ");
		//print ("Player " + ownerID + " spawned " + this.name);
		gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", pickColour(ownerID));
	}

	void Start () {
		lightRing = GetComponentInChildren<Projector>();
		lightRing.enabled = false;
		agent = GetComponent <NavMeshAgent> ();
		//setOwner ();
	}
	[Command]
	public void CmdAttackTarget(GameObject targetToAttack)
	{
		cooldown = 0;
		unit target = targetToAttack.GetComponent<unit> ();
		GameObject unitToSpawn = Resources.Load ("bullet") as GameObject;
		GameObject instantiated = (GameObject)Instantiate (unitToSpawn, this.transform.position, Quaternion.identity);
		instantiated.GetComponent<moveBullet> ().target = target;
		NetworkServer.Spawn(instantiated);

	}
	public void attackMove()
	{
		print ("amoving");
		GameObject[] allUnits = GameObject.FindGameObjectsWithTag ("unit");
		ArrayList enemyUnits = new ArrayList ();
		foreach (GameObject g in allUnits) {
			if (g.transform.parent != this.transform.parent) {
				enemyUnits.Add (g);
				if (Vector3.Distance (this.transform.position, g.transform.position) < range && cooldown > cooldownTimer) {
					CmdAttackTarget (g);
					agent.Stop ();
				}
			}
		}
	}	

	public void Select() {
		if (hasAuthority) {
			selected = true;
			lightRing.enabled = true;
			print ("Unit " + this.name + " selected!");
			//gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
		}
	}

	public void Unselect() {
		selected = false;
		lightRing.enabled = false;
		//gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.white);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			aClickPressed = true;
		}
		if (aClickPressed && Input.GetMouseButtonDown (1)) {
			aClickPressed = false;
		}
		if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.T)) && selected) {
			aMoving = false; 
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				print (hit.point);
				if (Input.GetMouseButtonDown (1)) {
					agent.SetDestination (hit.point);
				} else if (Input.GetKeyDown (KeyCode.T)) {
					agent.transform.position = hit.point;
				}
			}
		}

		if (Input.GetMouseButtonDown(0) && selected && aClickPressed) {
			aMoving = true;
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				print (hit.point);
				agent.SetDestination (hit.point);
			}
		}
		if (cooldown > cooldownTimer) {
			//allow movement again
			agent.Resume();
		}
		if (aMoving) {
			attackMove ();
		}
		cooldown++;
	}
}
