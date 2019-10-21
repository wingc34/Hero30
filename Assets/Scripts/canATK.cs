using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canATK: MonoBehaviour {
    private GameObject Monster;
    private GameObject Hero;


    public Text attackText;
    public GameObject Explosion;
    public Animator ExplosionAnimation;
    // Start is called before the first frame update
    void Start() {
        Hero = GameObject.Find("hero_b");
        attackText.enabled = false;
    }

    // Update is called once per frame
    void Update() {

    }
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Monster") {
            Monster = collider.gameObject;
            //print(Monster);
            attackText.enabled = true;
            print("canATK = true");
            Hero.SendMessage("GetMonster", Monster);
            Hero.SendMessage("ATKTrue");
            GameObject expolpic = Instantiate(Explosion, transform.position + new Vector3((float)1.5, 0, 0), transform.rotation);
            expolpic.transform.parent = this.transform.parent;
            ExplosionAnimation.SetInteger("boom", 1);
            Destroy(expolpic, 1);
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.tag == "Monster") {
            attackText.enabled = false;
            print("canATK = false");
            Hero.SendMessage("ATKFalse");
        }
    }
}