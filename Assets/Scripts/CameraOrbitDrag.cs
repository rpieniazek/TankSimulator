using UnityEngine;
using System.Collections;

public class CameraOrbitDrag : MonoBehaviour
{
    /*	
     *	public Transform orbitTarget;
        public Transform camTransform;
        public Camera camera;


        protected Vector3 _LocalRotation;
        protected float _CameraDistance = 10f;

        public float MouseSensitivity = 4f;
        public float ScrollSensitvity = 2f;
        public float OrbitDampening = 10f;
        public float ScrollDampening = 6f;

        public bool CameraDisabled = false;

    */

    [SerializeField] bool OnMouseButtonDown;
    public bool CanMove; 

   

    public Transform target;
    public float distance = 10.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;


    float minFov = 5f;
    float maxFov = 50f;
    float sensitivity = 5f;


    private Rigidbody rigid;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start () 
	{
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		rigid = GetComponent<Rigidbody>();

		// Make the rigid body not change rotation
		if (rigid != null)
		{
			rigid.freezeRotation = true;
		}


		x += Input.GetAxis ("Mouse X") * xSpeed * distance * 0.02f;
		y -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;

		y = ClampAngle (y, yMinLimit, yMaxLimit);

		Quaternion rotation = Quaternion.Euler (y, x, 0);

		//	distance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

		/*	RaycastHit hit;
				if (Physics.Linecast (target.position, transform.position, out hit)) {
					distance -= hit.distance;
				}*/
		Vector3 negDistance = new Vector3 (0.0f, 0.0f, -distance);
		Vector3 position = rotation * negDistance + target.position;

		transform.rotation = rotation;
		transform.position = position;
	}

	void LateUpdate () 
	{
       

		if (target) {

			float dist = distance;
			dist += Input.GetAxis("Mouse ScrollWheel") * sensitivity*-1;
			dist= Mathf.Clamp(dist, minFov, maxFov);
			distance = dist;


			if (CanMove &&
                !OnMouseButtonDown || 
                (OnMouseButtonDown && Input.GetMouseButton (0))
                )
            {

                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);



            }
				Quaternion rotation = Quaternion.Euler (y, x, 0);

				Vector3 negDistance = new Vector3 (0.0f, 0.0f, -distance);
				Vector3 position = rotation * negDistance + target.position;

				transform.rotation = rotation;
				transform.position = position;

		}
	}

	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
	}
