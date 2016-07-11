using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class unit : NetworkBehaviour {
	public GameObject circle;
	public int range = 1000000;
	bool selected = false;
	NavMeshAgent agent;
	bool aMoving = false;
	bool aClickPressed = false;
	int cooldown = 0;
	int cooldownTimer = 100;

	private Projector lightRing;

	void Start () {
		lightRing = GetComponentInChildren<Projector>();
		lightRing.enabled = false;

		agent = GetComponent <NavMeshAgent> ();
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
			gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
		}
	}

	public void Unselect() {
		selected = false;
		lightRing.enabled = false;
		gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.white);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			aClickPressed = true;
		}
		if (aClickPressed && Input.GetMouseButtonDown (1)) {
			aClickPressed = false;
		}
		if (Input.GetMouseButtonDown(1) && selected) {
			aMoving = false; 
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				print (hit.point);
				agent.SetDestination (hit.point);
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
