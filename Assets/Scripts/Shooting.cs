using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

	public Rigidbody projectile;
	public float speed = 20;
	public AudioClip shootSound;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Rigidbody instantiatedProjectile = Instantiate(projectile,transform.position,transform.rotation)as Rigidbody;
			instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
			//instantiatedProjectile.AddForce(new Vector3(0,0,speed));

			//w audio nie ma takiej metody PlayOneShot
//			audio.PlayOneShot(shootSound);


		}
	}
}
