using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelRecoil : MonoBehaviour {
    public Transform barrelPart;
    private Vector3 startPoint;
    private Vector3 endPoint;
    // Use this for initialization
    void Start () {
        startPoint = barrelPart.position;
        endPoint = barrelPart.position + barrelPart.up * 0.5f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startPoint = barrelPart.position;
            endPoint = barrelPart.position + barrelPart.up * 0.3f;
            StartCoroutine(triggerRecoil());
        }
    }

    IEnumerator triggerRecoil()
    {
        yield return StartCoroutine(MoveObject(barrelPart, startPoint, endPoint, 0.07f));
        yield return StartCoroutine(MoveObject(barrelPart, endPoint, startPoint, 0.15f));
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

}
