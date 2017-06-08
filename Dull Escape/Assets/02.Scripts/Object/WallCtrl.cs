using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCtrl : MonoBehaviour {
    public int k = 0;

   

    void OnCollisionEnter(Collision other)
    {
        k = 1;
        if (other.gameObject.tag == "BULLET")
        {

            Destroy(other.gameObject);
        }
    }


}
