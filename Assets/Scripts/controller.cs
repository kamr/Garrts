using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour {

	bool boxClickDown = false;
	Vector2 mouseStart = new Vector2();

	void Start () {
	}

	private bool IsWithinSelectionBounds(GameObject u) {
		Bounds viewBounds = Utils.GetViewportBounds(Camera.main, mouseStart, Input.mousePosition );
		return viewBounds.Contains(Camera.main.WorldToViewportPoint(u.transform.position) );
	}

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
