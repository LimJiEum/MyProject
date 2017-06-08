using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCtrl : MonoBehaviour {

    public int HP = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "BULLET")
        {
            HP -= 50;


            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Punch")
        {
            HP -= 10;
            

        }

        if(HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
