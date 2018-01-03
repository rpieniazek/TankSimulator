using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RearWheelDrive : MonoBehaviour {


    public static RearWheelDrive instance;

    public bool CanMove;

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
    public float scrollSpeed = 1;
    public GameObject leftTrack, rightTrack;

    public GameObject leftNonDriveWheels, rightNonDriveWheels;

    public ParticleSystem smoke1;
    public ParticleSystem smoke2;
    // here we find all the WheelColliders down in the hierarchy

    void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        wheels = GetComponentsInChildren<WheelCollider>();

        for (int i = 0; i < wheels.Length; ++i)
        {
            var wheel = wheels[i];
            if (wheelShapeMid != null) {
                var ws = GameObject.Instantiate(wheelShapeMid);
                ws.transform.parent = wheel.transform;
                if (ws.transform.localPosition.x < 0f) {
                    ws.transform.localScale = new Vector3(ws.transform.localScale.x * -1f, ws.transform.localScale.y, ws.transform.localScale.z);
                }
            }
        }

        smoke1.Stop();
        smoke2.Stop();
            
    }


    public void Emit(bool forward)
    {

        Vector3 rotation = new Vector3();
        rotation.y = forward ? 180f : 0f;

        smoke1.transform.localEulerAngles = rotation;
        smoke2.transform.localEulerAngles = rotation;

        smoke1.Emit(1);
        smoke2.Emit(1);
    }

	public void Update()
	{

        float HorizontalTank = Input.GetAxis("HorizontalTank");
        float VerticalTank = Input.GetAxis("VerticalTank");


        if(!CanMove)
        {
            HorizontalTank = 0f;
            VerticalTank = 0f;
        }


        float leftRpm = 0;
        float rightRpm = 0;


        float turn = forwardTorque * HorizontalTank * 0.5f;




        foreach (WheelCollider wheel in wheels) {
			if(wheel.name.Contains("l")){
				leftRpm+= wheel.rpm;
			} else{
				rightRpm += wheel.rpm;
			}
			if (VerticalTank == 0 ) {

				wheel.wheelDampingRate = breakDampingRate;
			} else {
				wheel.wheelDampingRate = driveDampingRate;
			}

			if (VerticalTank == 0) {			

				if (wheel.transform.localPosition.x > 0) {
					wheel.motorTorque = -1 * turn;
				}
				if (wheel.transform.localPosition.x < 0) {
					wheel.motorTorque = turn;
				}
			} else {
				float leftPercentage = 0.5f - HorizontalTank * turnSensivity /2 ;
				float rightPercentage = 0.5f + HorizontalTank * turnSensivity /2;

				if (wheel.transform.localPosition.x > 0) {
					wheel.motorTorque = leftPercentage * forwardTorque *Mathf.Clamp(VerticalTank, -0.5f,1);
					}
				if (wheel.transform.localPosition.x < 0) {
					wheel.motorTorque = rightPercentage* forwardTorque*Mathf.Clamp(VerticalTank, -0.5f,1);
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


        // Jeśli odpowiednia prędkość - emituj dym
        if (velocity > 10)
        {
            bool forward = (Time.deltaTime * rightRpm > 0) ? true : false;
            Emit(forward);
        }



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
     


		GetComponentInChildren<AudioSource>().pitch = Mathf.Clamp(0.75f +velocity/60f + 0.5f*Mathf.Abs(VerticalTank) +  Mathf.Abs(HorizontalTank),  0.75f, 2.18f);
	}
}
