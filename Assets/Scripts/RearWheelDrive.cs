using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RearWheelDrive : MonoBehaviour {

	private WheelCollider[] wheels;

	//public float turnSpeed = 300;
	public float forwardTorque = 300;

	public float breakDampingRate = 100;
	public float driveDampingRate = 10;
	public GameObject wheelShapeFront;
	public GameObject wheelShapeMid;
	public GameObject wheelShapeBack;
	public Rigidbody tank;
	public Text leftTorque;
	public Text rightTorque;
	public Text speed;
	// here we find all the WheelColliders down in the hierarchy
	public void Start()
	{
		wheels = GetComponentsInChildren<WheelCollider>();

		for (int i = 0; i < wheels.Length; ++i) 
		{
			var wheel = wheels [i];

			if (wheel.name.Contains ("0")) {
				// create wheel shapes only when needed
				if (wheelShapeFront != null) {
					var ws = GameObject.Instantiate (wheelShapeFront);
					ws.transform.parent = wheel.transform;
				}
			} else if (wheel.name.Contains ("6")) {

				if (wheelShapeBack != null) {
					var ws = GameObject.Instantiate (wheelShapeBack);
					ws.transform.parent = wheel.transform;
				}
			} else {
				if (wheelShapeMid != null) {
					var ws = GameObject.Instantiate (wheelShapeMid);
					ws.transform.parent = wheel.transform;
				}
			}
		} 
	}


	public void Update()
	{

		/*
		if (Input.GetAxis ("Vertical") != 0 &&  Input.GetAxis("Horizontal") == 0)
			forwardTorque = 45000;
		else
			forwardTorque = 20000;
		*/
		float turn = forwardTorque * Input.GetAxis("Horizontal")*0.5f;

		foreach (WheelCollider wheel in wheels) {

			if (Input.GetAxis ("Vertical") == 0 ) {

				wheel.wheelDampingRate = breakDampingRate;
			} else {
				wheel.wheelDampingRate = driveDampingRate;
			}


			if (Input.GetAxis ("Vertical") == 0) {

				

				if (wheel.transform.localPosition.x > 0) {
					wheel.motorTorque = -1 * turn;
					leftTorque.text = "L: " + -1 * turn;
				}
				if (wheel.transform.localPosition.x < 0) {
					wheel.motorTorque = turn;
					rightTorque.text=   "R: " + turn;
				}
			} else {
				float leftPercentage = 0.5f - Input.GetAxis ("Horizontal")/2 ;
				float rightPercentage = 0.5f + Input.GetAxis ("Horizontal")/2;

				if (wheel.transform.localPosition.x > 0) {
					wheel.motorTorque = leftPercentage * forwardTorque *Input.GetAxis("Vertical");
					leftTorque.text=  "L: "+ leftPercentage * forwardTorque * Input.GetAxis ("Vertical");
				}
				if (wheel.transform.localPosition.x < 0) {
					wheel.motorTorque = rightPercentage* forwardTorque*Input.GetAxis("Vertical");
					rightTorque.text= "R: "+ rightPercentage* forwardTorque*Input.GetAxis("Vertical");
				}

			}
				
		
			// a simple car where front wheels steer while rear ones drive

			// update visual wheels if any
			if (wheelShapeFront || wheelShapeMid || wheelShapeBack) 
			{
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose (out p, out q);

				// assume that the only child of the wheelcollider is the wheel shape
				Transform shapeTransform = wheel.transform.GetChild (0);
				shapeTransform.position = p;
				shapeTransform.rotation = q;
			}

		}

		speed.text= "Speed: " + (3.6*tank.velocity.magnitude).ToString("N3")+ " kmh";

	}
}
