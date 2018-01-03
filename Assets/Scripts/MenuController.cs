using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField] float time;
    [SerializeField] Text text;
    [SerializeField] GameObject arrows;
    bool appearing;
    float alpha = 1;
    Outline outline;

    private void Start()
    {
        appearing = false;
        outline = text.GetComponent<Outline>();

        StartCoroutine(Sign());
    }

    // Update is called once per frame
    void Update () {

        if(Input.GetButtonDown("Fire1"))
        {
            arrows.SetActive(true);
            SceneManager.LoadSceneAsync(1);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            Application.Quit();
        }

        
    }


    IEnumerator Sign()
    {
        while (true)
        {
            text.CrossFadeAlpha(0, time, true);
            yield return new WaitForSeconds(time);
            text.CrossFadeAlpha(1, time, true);
            yield return new WaitForSeconds(time);

            yield return null;
        }


    }
}
