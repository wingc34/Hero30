using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class up : MonoBehaviour
{
    private GameObject Hero;
    // Start is called before the first frame update
    void Start()
    {
	    Hero = GameObject.Find(transform.parent.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D (Collider2D collider){
	if (collider.gameObject.tag == "edge") {
         	Hero.SendMessage("CanUpKey", 1);
        }
    }

    void OnTriggerExit2D (Collider2D collider) {
        if (collider.gameObject.tag == "edge") {
            Hero.SendMessage("CanUpKey", -1);
        }
    }
}
