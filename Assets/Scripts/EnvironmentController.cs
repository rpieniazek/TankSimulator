using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnvironmentController : MonoBehaviour {

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.X)){
			resetScene();
		}
         
	}

	void resetScene(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
