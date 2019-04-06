using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

    private Transform trans;
    private Transform cam;

    // Use this for initialization
    void Start () {
        trans = GetComponent<Transform>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.LookAt(cam.position);
        trans.eulerAngles = new Vector3(0, trans.eulerAngles.y, 0);
	}
}
