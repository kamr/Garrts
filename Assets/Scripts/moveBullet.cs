using UnityEngine;
using System.Collections;

public class moveBullet : MonoBehaviour {

	public unit target;
	public float moveSpeed = 5f;
	Vector3 targetToHit;
	Bounds b = new Bounds();
	// Use this for initialization
	void Start () {
		targetToHit = target.transform.position;
		b = target.GetComponent<Renderer> ().bounds;
	}
	
	// Update is called once per frame
	void Update () {
		
		this.transform.LookAt (targetToHit);
		this.transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime); 

		if (b.Contains (this.transform.position)) {
			Destroy (this);
		}
	}
}
