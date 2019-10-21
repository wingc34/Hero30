using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleControl: MonoBehaviour {
    public Image HP;
    public Image converse;
    public Text TimeText;
    public Text MoneyText;
    public Text dangerText;
    public Text questItemText;
    public GameObject bossTxt;

    private int heroHP;
    private int heroMoney = 100;
    private int totalMonster = 3;
    private GameObject Hero;
    private GameObject Quest1;
    private GameObject Quest2;
    private GameObject Quest3;
    private GameObject Quest4;
    private float timer = 30;
    private bool canFight = false;
    private GameObject[] Mons = null;
    private bool isEnd = false;
    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("heroHP")) {
            heroHP = PlayerPrefs.GetInt("heroHP");
        }
        if (PlayerPrefs.HasKey("timer")) {
            timer = PlayerPrefs.GetFloat("timer");
        }
        if (PlayerPrefs.HasKey("heroMoney")) {
            heroMoney = PlayerPrefs.GetInt("heroMoney");
        }

        Hero = GameObject.FindGameObjectWithTag("Player");
        Quest1 = GameObject.Find("Quest1");
        Quest2 = GameObject.Find("Quest2");
        Quest3 = GameObject.Find("Quest3");
        Quest4 = GameObject.Find("Quest4");
        MoneyText.text = heroMoney.ToString();
        TimeText.text = timer.ToString("F2");

        HP.rectTransform.sizeDelta = new Vector2((float)(heroHP * 2.5), 30);
        HP.transform.position = new Vector2(40, HP.transform.position.y);
        questItemText.text = "";

        if (PlayerPrefs.HasKey("area")) {
            if ((PlayerPrefs.GetInt("area") == 4)) {
                dangerText.enabled = false;
                converse.enabled = true;
            } else {
                dangerText.enabled = true;
            }

        } else {
            dangerText.enabled = true;
        }
    }

    // Update is called once per frame
    void Update() {
        if (canFight) {
            timer -= Time.deltaTime;
            TimeText.text = timer.ToString("F2");
            if (timer < 0) {
                Gameover();
                // this is for if it is lose, and will change to lose scene or other develop eg. Lose()
            }
        }
    }

    void getTotalMonster(int num) {
        totalMonster = num;
    }
    IEnumerator MonsterKilled(GameObject monster) {
        print(totalMonster);
        totalMonster--;
        //print(totalMonster);
        Quest1.SendMessage("MonsterKilled");
        Quest2.SendMessage("MonsterKilled");
        //print(monster);
        if (monster.name == "monster3_b(Clone)") {
            //print(monster);
            Quest3.SendMessage("MonsterKilled");
        }
        if (monster.name == "monster5_b(Clone)") {
            //print(monster);
            Quest4.SendMessage("MonsterKilled");
        }
        if (totalMonster < 0) {
            //print(totalMonster);
            yield return new WaitForSeconds(1);
            setTime();
            Quest1.SendMessage("SaveMonsterNum");
            Quest2.SendMessage("SavefoodNum");
            Quest4.SendMessage("SavewoodNum");
            //PlayerPrefs.SetInt("heroMoney", heroMoney);
            //PlayerPrefs.Save();
            print("asd");
            Hero.SendMessage("SavePrefs");

        }
    }

    void damage(int damageHP) {
        heroHP -= damageHP;
        HP.rectTransform.sizeDelta = new Vector2((float)(heroHP * 2.5), 30);
        HP.transform.position = new Vector2(40, HP.transform.position.y);
    }

    void setTime() {
        PlayerPrefs.SetFloat("timer", timer);
        PlayerPrefs.Save();
    }

    void GetMoney(int money) {
        heroMoney += money;
        MoneyText.text = heroMoney.ToString();
    }

    IEnumerator GetMonsters(GameObject[] clone) {
        //this is wait for DANGEROUS or boss's dialogue
        Mons = clone;
        if (PlayerPrefs.GetInt("area") == 4) {
            //wait for boss
            bossTxt.SendMessage("SetPrintQuest", true);
            bossTxt.SendMessage("GetStartQuest");
            dangerText.enabled = false;
        } else {
            yield return new WaitForSeconds((float)0.35);
            dangerText.enabled = false;
            yield return new WaitForSeconds((float)0.35);
            dangerText.enabled = true;
            yield return new WaitForSeconds((float)0.35);
            dangerText.enabled = false;
            for (int i = 0; i < clone.Length; i++) {
                clone[i].SendMessage("StartFighting", true);
            }
            Hero.SendMessage("StartFighting", true);
            canFight = true;
        }
    }

    void Gameover() {
        if (PlayerPrefs.HasKey("area")) {
            if ((PlayerPrefs.GetInt("area") == 4)) {
                LossBoss();
            } else {
                Application.LoadLevel(0);
            }

        } else {
            Application.LoadLevel(0);
        }
    }

    IEnumerator SetEndOfPrintingQuest(bool can) {
        if (!isEnd) {
            converse.enabled = false;
            dangerText.enabled = true;
            yield return new WaitForSeconds((float)0.35);
            dangerText.enabled = false;
            yield return new WaitForSeconds((float)0.35);
            dangerText.enabled = true;
            yield return new WaitForSeconds((float)0.35);
            dangerText.enabled = false;
            Mons[0].SendMessage("StartFighting", true);
            Hero.SendMessage("StartFighting", true);
            canFight = true;
        } else {
            Application.LoadLevel(0);
        }
    }

    void WinBoss() {
        converse.enabled = true;
        bossTxt.SendMessage("SetPrintQuest", true);
        bossTxt.SendMessage("GetWinStart");
        Mons[0].SendMessage("cantFighting", true);
        Hero.SendMessage("cantFighting", true);
        canFight = false;
        isEnd = true;
    }

    void LossBoss() {
        converse.enabled = true;
        bossTxt.SendMessage("SetPrintQuest", true);
        bossTxt.SendMessage("GetLoseStart");
        Mons[0].SendMessage("cantFighting", true);
        //Hero.SendMessage("cantFighting", true);
        canFight = false;
        isEnd = true;
    }
}