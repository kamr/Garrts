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
	public void attackTarget(unit target)
	{

	}
	public void attackMove()
	{



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
	}
}
