using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour {

	bool boxClickDown = false;
	float mouseXStart = 0;
	float mouseYStart = 0;
	Vector3 mouseStart = new Vector2();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && boxClickDown == false) {
			mouseXStart = Input.mousePosition.x;
			mouseYStart = Input.mousePosition.y;
		//	mouseStart = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mouseStart = Input.mousePosition;
			boxClickDown = true;
		}

		if (Input.GetMouseButtonUp (0) && boxClickDown == true) {
			boxClickDown = false;
		//	Vector3 mouseEndPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector3 mouseEndPoint =  Input.mousePosition;
			Vector2 getLowestValues = new Vector2 ();
			Vector2 getHighestValues = new Vector2 ();

			getLowestValues.x = Mathf.Min (mouseStart.x,mouseEndPoint.x);
			getLowestValues.y = Mathf.Min (mouseStart.y, mouseEndPoint.y);

			getHighestValues.x = Mathf.Max (mouseStart.x, mouseEndPoint.x);
			getHighestValues.y = Mathf.Max (mouseStart.y, mouseEndPoint.y);

			Rect rectangleBox = new Rect (
				                    getLowestValues, getHighestValues - getLowestValues);


			//rectangleBox.height = 0.5f;

			int numberOfUnitsSelected = 0;
			ArrayList unitsSelected = new ArrayList();

			GameObject[] units = GameObject.FindGameObjectsWithTag ("unit");
			foreach (GameObject u in units) {


				if (rectangleBox.Contains (Camera.main.WorldToScreenPoint (u.transform.position))) {

					numberOfUnitsSelected++;
					unitsSelected.Add (u.GetComponent<unit>());
					
				}	

			}
			if (numberOfUnitsSelected > 0) {
				foreach (GameObject u in units) {
					u.GetComponent<unit> ().selected = false;
				}
				foreach (unit un in unitsSelected)
				{
					un.selected = true;
				}
			}

			}
		if (Input.mousePosition.x > 0.95 * Screen.width) {
			
			Vector3 endPosition = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
			endPosition.x += 3;	
			endPosition.z -= 3;
		//	Camera.main.transform.position = endPosition;
		}
		if (Input.mousePosition.x < 0.05 * Screen.width) {

			Vector3 endPosition = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
			endPosition.x -= 3;	
			endPosition.z += 3;
		//	Camera.main.transform.position = endPosition;
		}

		if (Input.mousePosition.y < 0.05 * Screen.height) {

			Vector3 endPosition = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
			endPosition.x -= 3;	
			endPosition.z -= 3;
			//Camera.main.transform.position = endPosition;
		}
		if (Input.mousePosition.y > 0.95 * Screen.height) {

			Vector3 endPosition = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
			endPosition.x += 3;	
			endPosition.z += 3;
			//Camera.main.transform.position = endPosition;
		}

	}
}
