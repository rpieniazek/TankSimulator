using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTrack : MonoBehaviour {


	public GameObject link;
	public int count = 100;
	// Use this for initialization
	void Start () {
		GameObject old = null;
		for (int i = 0; i < count; i++) {
			GameObject current = GameObject.Instantiate (link);
			current.transform.SetParent (transform);
		//	current.transform.rotation = Quaternion.Euler (0, 0, 0);
			current.transform.position = transform.position;
			if (old != null) {
				old.GetComponentInChildren<HingeJoint> ().connectedBody = current.GetComponent<Rigidbody>();
				current.transform.localPosition = old.transform.localPosition + new Vector3 (0, 0, 0.11f);
			}
			old = current;
			if (i == count - 1) {
				Destroy (current.GetComponentInChildren<HingeJoint> ());
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
