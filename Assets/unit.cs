using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class unit : NetworkBehaviour {
	public bool selected = false;
	NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		agent = GetComponent <NavMeshAgent> ();


	}
	
	// Update is called once per frame
	void Update () {

		if (selected) {
			print ("unit selected!");
			gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.red);
		}
		if (!selected) {
			gameObject.GetComponent<Renderer> ().material.SetColor ("_Color", Color.white);
		}
		if (Input.GetMouseButtonDown(1) && selected)
			{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				print (hit.point);
				agent.SetDestination (hit.point);
			}
			}
	}
}
