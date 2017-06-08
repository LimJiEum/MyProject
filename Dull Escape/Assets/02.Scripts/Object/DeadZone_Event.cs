using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone_Event : MonoBehaviour {


   
	// Use this for initialization
	void Start () {
		
	}

    
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        /*if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
           
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Punch")
        {
            
            Destroy(this.gameObject);

        }
    }
}
