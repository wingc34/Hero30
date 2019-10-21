using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emHero : MonoBehaviour
{
    public GameObject Hero;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Hero, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
