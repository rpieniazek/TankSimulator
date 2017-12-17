using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RearWheelDrive : MonoBehaviour {

	private WheelCollider[] wheels;
	float leftOffset;
	float rightOffset;
	//public float turnSpeed = 300;
	public float forwardTorque = 300;

	public float breakDampingRate = 100;
	public float driveDampingRate = 10;
	public GameObject wheelShapeMid;
	public Rigidbody tank;
	public Text speed;
	public float turnSensivity = 1;
	public float scrollSpeed= 1;
	public GameObject leftTrack, rightTrack;

	public GameObject leftNonDriveWheels,rightNonDriveWheels;
	// here we find all the WheelColliders down in the hierarchy

	public void Start()
	{
		wheels = GetComponentsInChildren<WheelCollider>();

		for (int i = 0; i < wheels.Length; ++i) 
		{
			var wheel = wheels [i];		
			if (wheelShapeMid != null) {
					var ws = GameObject.Instantiate (wheelShapeMid);
					ws.transform.parent = wheel.transform;
				if (ws.transform.localPosition.x < 0f) {
						ws.transform.localScale = new Vector3 (ws.transform.localScale.x * -1f, ws.transform.localScale.y, ws.transform.localScale.z);
				}
				}
			}
		} 


	public void Update()
	{

		/*
		if (Input.GetAxis ("Vertical") != 0 &&  Input.GetAxis("HorizontalTank") == 0)
			forwardTorque = 45000;
		else
			forwardTorque = 20000;
		*/
		float turn = forwardTorque * Input.GetAxis("HorizontalTank")*0.5f;


		float leftRpm = 0;
		float rightRpm =0;



		foreach (WheelCollider wheel in wheels) {
			if(wheel.name.Contains("l")){
				leftRpm+= wheel.rpm;
			} else{
				rightRpm += wheel.rpm;
			}
			if (Input.GetAxis ("VerticalTank") == 0 ) {

				wheel.wheelDampingRate = breakDampingRate;
			} else {
				wheel.wheelDampingRate = driveDampingRate;
			}


			if (Input.GetAxis ("VerticalTank") == 0) {

				

				if (wheel.transform.localPosition.x > 0) {
					wheel.motorTorque = -1 * turn;
				}
				if (wheel.transform.localPosition.x < 0) {
					wheel.motorTorque = turn;
				}
			} else {
				float leftPercentage = 0.5f - Input.GetAxis ("HorizontalTank")*turnSensivity /2 ;
				float rightPercentage = 0.5f + Input.GetAxis ("HorizontalTank")*turnSensivity /2;

				if (wheel.transform.localPosition.x > 0) {
					wheel.motorTorque = leftPercentage * forwardTorque *Mathf.Clamp(Input.GetAxis("VerticalTank"),-0.5f,1);
					}
				if (wheel.transform.localPosition.x < 0) {
					wheel.motorTorque = rightPercentage* forwardTorque*Mathf.Clamp(Input.GetAxis("VerticalTank"),-0.5f,1);
				}

			}
				
		
			// a simple car where front wheels steer while rear ones drive

			// update visual wheels if any
			if ( wheelShapeMid) 
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

		leftRpm /= 8f; rightRpm /= 8f;
		float velocity = 3.6f * tank.velocity.magnitude;
			speed.text= "Speed: " + (velocity).ToString("N3")+ " kmh";



		float offset = Time.time* scrollSpeed;
		leftOffset += offset * leftRpm;
		rightOffset+= offset * rightRpm;

		leftTrack.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(leftOffset, 0));
		rightTrack.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(rightOffset, 0));

		foreach (Transform t in leftNonDriveWheels.transform) {
			t.Rotate (new Vector3 (1, 0, 0), Time.deltaTime * leftRpm);
		}
		foreach (Transform t in rightNonDriveWheels.transform) {
			t.Rotate (new Vector3 (1, 0, 0), Time.deltaTime * rightRpm);

		}



		GetComponentInChildren<AudioSource>().pitch = Mathf.Clamp(0.75f +velocity/60f + 0.5f*Mathf.Abs(Input.GetAxis("VerticalTank")) +  Mathf.Abs(Input.GetAxis("HorizontalTank")),  0.75f, 2.18f);
	}
}
