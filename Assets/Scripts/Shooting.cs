using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public Rigidbody projectile;
	public float speed = 80;
	public AudioClip shootSound;

    [SerializeField] Transform spawn;
    [SerializeField] float reloadTime = 2f;
    [SerializeField] private float time = 0;
    [SerializeField] private bool canFire;


    void Start()
    {
        canFire = true;
    }

	// Update is called once per frame
	void Update ()
	{
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
                canFire = true;
        }

        if (RearWheelDrive.instance.CanMove && canFire && ( Input.GetButtonDown("Fire") || Input.GetAxis("RightTrigger") == 1))
        {
            Rigidbody instantiatedProjectile = Instantiate(projectile, spawn.position, spawn.rotation ) as Rigidbody;
            instantiatedProjectile.velocity = spawn.TransformDirection(new Vector3(0, 0, speed));

            time = reloadTime;
            canFire = false;

            SendMessage("Recoil", SendMessageOptions.DontRequireReceiver);
        }
	}
}
