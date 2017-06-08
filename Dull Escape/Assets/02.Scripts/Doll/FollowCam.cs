using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {
    public Transform targetTr;
    public float dist = 0.0f;
    public float height = 5.0f;
    public float dampTrace = 20.0f;
    public float rotSpeed = 100.0f;
    private Transform tr;
    // Use this for initialization
    void Start () {

        tr = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        tr.position = Vector3.Lerp(tr.position
            , targetTr.position 
            - (targetTr.forward * dist)
            +(Vector3.up * height)
            , Time.deltaTime * dampTrace);

        rotatePlayer();

        tr.LookAt(targetTr.position);

      
    }

    void rotatePlayer()
    {
        tr.Rotate(Vector3.right * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse Y"));
    }
}
