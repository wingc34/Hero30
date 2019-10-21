using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroBattle: MonoBehaviour {
    public float heroHP = 100; //hero's hp

    public float heroAttack = 10; //hero's ATK

    public float heroDef = 10; //hero's DEF

    public float heroMoveSpeed = 10; //?
    public float heroAttackSpeed;

    public int heroMoney = 100;
    public float heroAttackMoveSpeed = 3;

    public GameObject Money; //the coin object
    public Animator FightAnimation;
    public AudioSource hittingSound;

    private bool isFighting = false; //?
    private bool isMap = true; //?
    private bool isForward = true; //in battle scene, object is go to it's direction
    private float colPosX = 0; //decide isForward or not
    private bool canATK = false; //Hero can attack or not
    private GameObject Monster; //which monster is attacked to
    private GameObject Controller; //battle scene controller
    private float timer = 30; //this will move to controller laster
    private bool canFight = false;
    private Vector2 heroPos;

    // Start is called before the first frame update
    void Start() {
        heroHP = PlayerPrefs.GetFloat("heroHP");
        heroMoney = PlayerPrefs.GetInt("heroMoney");
        PlayerPrefs.Save();
        Controller = GameObject.Find("Controller");
        heroPos = new Vector2(transform.position.x,transform.position.y);
    }

    // Update is called once per frame
    void Update() {
        if (canFight) {
            heroAttackSpeed += Time.deltaTime;
            FightAnimation.SetInteger("Fight", 1);

            //control the movement
            if (isForward) {
                heroPos.x += heroAttackMoveSpeed * Time.deltaTime;
                transform.position = heroPos;
            } else {
                if (transform.position.x > -8.5) {
                    heroPos.x -= heroAttackMoveSpeed * Time.deltaTime;
                    transform.position = heroPos;
                } else {
                    isForward = true;
                }
                //transform.position.x -= heroAttackMoveSpeed * Time.deltaTime;
                if (transform.position.x - colPosX <= -1) {
                    isForward = true;
                }
            }

            if (canATK && Input.GetKeyDown("a") && Monster != null && heroAttackSpeed > 0.2) {
                heroAttackSpeed = 0;
                Monster.SendMessage("damageHP", heroAttack);
                print("press A & ATK");
                hittingSound.Play();
            }

            //quit the battle scene to map scene
            //this part will move to controller laster
            if (Input.GetKeyDown("d")) {
                Controller.SendMessage("setTime");
                SavePrefs();
            }

        }
    }
    //getting damage
    void damageHP(float damage) {
        isForward = false;
        colPosX = transform.position.x;
        float diff = damage - heroDef;
        if (diff > 0) {
            heroHP -= diff;
            print("Hero HP-" + diff);
            Controller.SendMessage("damage", diff);
        } else {
            heroHP -= 1;
            print("Hero HP-" + 1);
            Controller.SendMessage("damage", 1);
        }

        //hero lose because hp <= 0
        if (heroHP <= 0) {
            Destroy(gameObject);
            //Time.timeScale = 0.0; 
            Controller.SendMessage("Gameover");
            // this is for if it is lose, and will change to lose scene or other develop eg. Lose()
        }
    }

    //know which monster is attacked to
    void GetMonster(GameObject getMonster) {
        Monster = getMonster;
    }

    //can attack
    void ATKTrue() {
        canATK = true;
    }

    //can't attack
    void ATKFalse() {
        canATK = false;
    }

    //get money and show the coin
    void GetMoney(int money) {
        heroMoney += money;
        Controller.SendMessage("GetMoney", money);
        GameObject moneypic = Instantiate(Money, transform.localPosition + new Vector3(0, 2, 0), transform.rotation);
        moneypic.transform.parent = this.transform;
        Destroy(moneypic, (float)0.5);
        //print(money);
    }

    void SavePrefs() {
        PlayerPrefs.SetInt("heroMoney", heroMoney);
        PlayerPrefs.SetFloat("heroHP", heroHP);
        PlayerPrefs.Save();
        print("asd");
        Application.LoadLevel(1);
    }

    void AddwpATK(float weaponATK) {
        heroAttack += weaponATK;
    }

    void AddeqmDEF(float eqmDEF) {
        heroDef += eqmDEF;
    }

    void AddHP() {
        heroHP += 5;
        if (heroHP > 100) {
            heroHP = 100;
        }
    }

    void StartFighting() {
        canFight = true;
    }

    void Gameover() {
        Application.LoadLevel(0);
    }

    void cantFighting() {
        canFight = false;
    }
}