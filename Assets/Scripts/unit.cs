using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class unit : NetworkBehaviour {
	public GameObject circle;

	bool selected = false;
	NavMeshAgent agent;

	void Start () {
		agent = GetComponent <NavMeshAgent> ();
	}

	public void Select() {
		selected = true;
		print ("Unit " + this.name + " selected!");
		gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
	}

	public void UnSelect() {
		selected = false;
		gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.white);
	}

	void Update () {
		if (Input.GetMouseButtonDown(1) && selected) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				print (hit.point);
				agent.SetDestination (hit.point);
			}
		}
	}
}
