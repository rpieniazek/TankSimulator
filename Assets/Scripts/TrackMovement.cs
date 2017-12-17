using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMovement : MonoBehaviour {

	private WheelCollider[] wheels;
	public Bone[] bones = new Bone[16];
	// Use this for initialization
	void Start () {
		wheels = GetComponentsInChildren<WheelCollider> ();
		}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i < 16; i++) {
			bones [i].transform.position = wheels [i].GetComponentInChildren<MeshRenderer>().transform.position+new Vector3(0,-wheels[i].radius*0.8f,0);
		}
	}
}

