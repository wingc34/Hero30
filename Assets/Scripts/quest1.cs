using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class quest1: MonoBehaviour {
    public Text questText;

    private int CanGetQuest1 = 1;
    private int FinishedQuest1 = 0;
    private int Monster = 0;
    private string Background = "（您見到怪物正在襲擊村莊，村民發現身為勇者既您，所以委託您殺死怪物）";
    private string Statement1 = "村民　:　救命呀！！有一大堆怪物入村耍流氓呀！！TOT";
    private string Statement2 = "村民　:　我哋啲農作物被怪物破壞到連渣都無啦~TAT";
    private string Statement3 = "村民　:　求求勇者大人幫幫我哋搞掂啲怪物啦QvQ";
    private string StatementNo = "都係既，出面咁多怪物，勇者大人得一個人點可能應付到，我都係唔好強人所難啦TOT";
    private string StatementYes = "太好啦，我哋條村終於有救啦！勇者大人您太偉大啦！TvT";
    private string Statement4 = "村民　:　唔係勇者大人我都唔知以後靠咩賺錢";
    private string Statement5 = "村民　:　呢個係我既小小心意，請收左佢啦^v^";
    private string Statement6 = "（勇者得到補血物品x1）";
    private GameObject questCon;
    private GameObject TextCon;
    private string[] S;
    private string[] st;
    private string s = "";

    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("CanGetQuest1")) {
            CanGetQuest1 = PlayerPrefs.GetInt("CanGetQuest1");
            FinishedQuest1 = PlayerPrefs.GetInt("FinishedQuest1");
            Monster = PlayerPrefs.GetInt("Monster");
        }
    }

    void MonsterKilled() {
        if (CanGetQuest1 == 0 && FinishedQuest1 == 0) {
            Monster++;
            if (Monster >= 2) {
                PlayerPrefs.SetInt("Monster", 0);
                CanGetQuest1 = 0;
                FinishedQuest1 = 1;
                PlayerPrefs.SetInt("CanGetQuest1", 0);
                PlayerPrefs.SetInt("FinishedQuest1", 1);
                PlayerPrefs.Save();
            }
        }
    }

    void SaveMonsterNum() {
        if (CanGetQuest1 == 0 && FinishedQuest1 == 0) {
            PlayerPrefs.SetInt("Monster", Monster);
            PlayerPrefs.Save();
        }
    }

    IEnumerator GetQuestStatement() {
        questText.text = "";
        questText.text = Background;
        yield return new WaitForSeconds(1);
        questText.text = Statement1;
        yield return new WaitForSeconds(1);
        questText.text = Statement2;
        yield return new WaitForSeconds(1);
        questText.text = Statement3;
        yield return new WaitForSeconds(1);

    }

    void GetNoStatement() {
        if (CanGetQuest1 == 1 && FinishedQuest1 == 0) {
            questCon = GameObject.Find("questController");
            questCon.SendMessage("GetStatement", StatementNo);
        } else {
            questText.text = "";
        }
    }

    void GetYesStatement() {
        if (CanGetQuest1 == 1 && FinishedQuest1 == 0) {
            questCon = GameObject.Find("questController");
            questCon.SendMessage("GetStatement", StatementYes);
        } else {
            questText.text = "";
        }
    }

    IEnumerator GetFinishStatement() {
        questText.text = "";
        questText.text = Statement4;
        yield return new WaitForSeconds(1);
        questText.text = Statement5;
        yield return new WaitForSeconds(1);
        questText.text = Statement6;
        yield return new WaitForSeconds(1);

    }

    void QuestAccept() {
        CanGetQuest1 = 0;
        FinishedQuest1 = 0;
        PlayerPrefs.SetInt("CanGetQuest1", 0);
        PlayerPrefs.SetInt("FinishedQuest1", 0);
        PlayerPrefs.Save();
    }
}