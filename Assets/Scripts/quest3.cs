using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class quest3: MonoBehaviour {
    public Text questText;

    private int CanGetQuest3 = 1;
    private int FinishedQuest3 = 0;

    private string Background = "（您在村莊聽到哭聲,發現少女在花園中哭泣,您向她訢問事情並表示勇者身份）";
    private string Statement1 = "少女 : 您好, 我唔見左一條手鏈，係重要家傳之寶黎架QoQ";
    private string Statement2 = "少女 : 請問勇者先生可唔可以幫我搵到唔見左既手鏈 ?QAQ";

    private string StatementNo = "我明白勇者先生貴人事忙，唔好意思耽誤您既時間…..TAT (一臉傷心飄走~";
    private string StatementYes = "勇者先生真係好人，咁麻煩哂您啦!TvT (get好人卡";
    private string Statement4 = "少女 : 太感激您啦!您太好人啦!^v^(再get好人卡)";
    private string Statement5 = "少女 : 作為報酬，我送你一本特厚字典吧~~ 雖然係頭先係街拾到架(劃掉 ((一臉開心飄走";
    private string Statement6 = "（勇者得到特別防具 - 特厚字典）";

    private GameObject questCon;

    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("CanGetQuest3")) {
            CanGetQuest3 = PlayerPrefs.GetInt("CanGetQuest3");
            FinishedQuest3 = PlayerPrefs.GetInt("FinishedQuest3");
        }
    }

    void MonsterKilled() {
        if(CanGetQuest3 == 0 && FinishedQuest3 == 0){
		if (Random.Range(1,100) <= 60){
			questText.text += "手鏈 X 1\n";
			FinishedQuest3 = 1;
			PlayerPrefs.SetInt("CanGetQuest3", 0);
			PlayerPrefs.SetInt("FinishedQuest3", 1);
			PlayerPrefs.Save();
		}
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

    }

    void GetNoStatement() {
        if (CanGetQuest3 == 1 && FinishedQuest3 == 0) {
            questCon = GameObject.Find("questController");
            questCon.SendMessage("GetStatement", StatementNo);
        } else {
            questText.text = "";
        }
    }

    void GetYesStatement() {
        if (CanGetQuest3 == 1 && FinishedQuest3 == 0) {
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
        PlayerPrefs.SetInt("CanGetQuest3", 1);
        PlayerPrefs.SetInt("FinishedQuest3", 1);
	    PlayerPrefs.SetInt("EqmNo", 2);
        PlayerPrefs.Save();
    }

    void QuestAccept() {
        CanGetQuest3 = 0;
        FinishedQuest3 = 0;
        PlayerPrefs.SetInt("CanGetQuest3", 0);
        PlayerPrefs.SetInt("FinishedQuest3", 0);
        PlayerPrefs.Save();
    }
}