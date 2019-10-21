using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster: MonoBehaviour {
    public float monsterHP;

    public float monsterAttack;

    public float monsterDef;

    public float monsterAttackSpeed;

    public float monsterAttackMoveSpeed = 10; //maybe same as hero

    public int monsterMoneyMax = 0;
    public int monsterMoneyMin = 0;

    private bool isForward = true;
    private float colPosX = 0;
    private int money = 0;
    private GameObject Hero;
    private GameObject controller;
    private bool canFight = false;
    private Vector2 monsterPos;
    // Start is called before the first frame update
    void Start() {
        Hero = GameObject.FindGameObjectWithTag("Player");
        controller = GameObject.Find("Controller");
        monsterPos = new Vector2(transform.position.x, transform.position.y);
        money = Random.Range(monsterMoneyMin, monsterMoneyMax);
    }

    // Update is called once per frame
    void Update() {
        if (canFight) {
            monsterAttackSpeed += Time.deltaTime;
            if (monsterAttackSpeed >= 1) {
                monsterAttackSpeed = 0;
                Hero.SendMessage("damageHP", monsterAttack);
            }

            if (!isForward) {
                if (transform.position.x < 8.5) {
                    monsterPos.x += monsterAttackMoveSpeed * Time.deltaTime;
                    transform.position = monsterPos;
                    if (transform.position.x - colPosX >= 1) {
                        isForward = true;
                    }
                }
            }
            if (canFight) {
                if (isForward) {
                    monsterPos.x += monsterAttackMoveSpeed * Time.deltaTime;
                    transform.position = monsterPos;
                } else {
                    monsterPos.x += monsterAttackMoveSpeed * Time.deltaTime;
                    transform.position = monsterPos;
                    if (transform.position.x - colPosX >= 1) {
                        isForward = true;
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            isForward = false;
            colPosX = transform.position.x;
            Hero.SendMessage("damageHP", monsterAttack);
        }
    }

    void damageHP(float damage) {
        float diff = damage - monsterDef;
        print(diff);
        if (diff > 0) {
            monsterHP -= diff;
            print("Moster HP-" + diff);
        } else {
            monsterHP -= 1;
            print("Moster HP-" + 1);
        }
        if (monsterHP <= 0) {
            Hero.SendMessage("GetMoney", money);
            controller.SendMessage("MonsterKilled", gameObject);
            Destroy(gameObject);
        }
    }

    void StartFighting() {
        canFight = true;
    }

}