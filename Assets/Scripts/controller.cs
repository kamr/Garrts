using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour {

	bool boxClickDown = false;
	Vector2 mouseStart = new Vector2();

	void Start () {
	}

	private bool IsWithinSelectionBounds(GameObject u) {
		/*Debug.Log (mouseStart.x + " " + mouseStart.y + " " + Input.mousePosition.x + " " + Input.mousePosition.y);
		Bounds viewBounds = Utils.GetViewportBoundsFromScreen(Camera.main, mouseStart, Input.mousePosition );

		Bounds objectBounds = u.GetComponent<Renderer> ().bounds;

		// NEED TO CONVERT THE OBJECTS POSITION IN WORLD TO OBJECTBOUNDSINVIEW USING UTILS.GETVIEWPORTBOUNDSFROMWORLD
		// ALSO MUST MODIFY GETVIEWPORTBOUNDSFROMWORLD

		//Bounds objectBoundsInView = Utils.GetViewportBoundsFromWorld(Camera.main, objectBounds.

		//Bounds objectBounds = u.GetComponent<Renderer> ().bounds;
		//Debug.Log(viewBounds.min + " " + viewBounds.max + " " + objectBounds.min + " " + objectBounds.max);
		//return viewBounds.Contains (Camera.main.WorldToViewportPoint (u.transform.position));
		return viewBounds.Intersects(objectBounds);
		*/
		//not working code just commented 4 now so i can work on other stuff

			Debug.Log (mouseStart.x + " " + mouseStart.y + " " + Input.mousePosition.x + " " + Input.mousePosition.y);
			Bounds viewBounds = Utils.GetViewportBoundsFromScreen(Camera.main, mouseStart, Input.mousePosition );
			return viewBounds.Contains (Camera.main.WorldToViewportPoint (u.transform.position));
	

	}

	/*Bounds GetMaxBounds(GameObject g) {

		return b;
	}*/

	void Update () {
		if (Input.GetMouseButtonDown (0) && boxClickDown == false) {
			mouseStart = Input.mousePosition;
			boxClickDown = true;
		}

		if (Input.GetMouseButtonUp (0) && boxClickDown == true) {
			ArrayList unitsSelected = new ArrayList();

			GameObject[] units = GameObject.FindGameObjectsWithTag ("unit");
			foreach (GameObject u in units) {
				if (IsWithinSelectionBounds(u)) {
					u.GetComponent<unit> ().Select ();
					unitsSelected.Add (u.GetComponent<unit> ());
				} else u.GetComponent<unit> ().Unselect ();
			}
			if (unitsSelected.Count == 0) {
				foreach (GameObject u in units) {
					u.GetComponent<unit> ().Select ();

				}
			}
			boxClickDown = false;
		}
	}

	void OnGUI()
	{
		if (boxClickDown)
		{
			var rect = Utils.GetScreenRect( mouseStart, Input.mousePosition );
			Utils.DrawScreenRect( rect, new Color( 0.8f, 0.8f, 0.95f, 0.25f ) );
			Utils.DrawScreenRectBorder( rect, 2, new Color( 0.8f, 0.8f, 0.95f ) );
		}
	}
}
