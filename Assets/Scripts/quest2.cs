using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class quest2: MonoBehaviour {
    public Text questText;

    private int CanGetQuest2 = 1;
    private int FinishedQuest2 = 0;
    private int food = 0;
    private string Background = "（有村民想出外採集食物，但怪物太多，所以委託您幫忙）";
    private string Statement1 = "村民　:　屋企已經缺糧到連穀種都無得食，出面又咁多怪物… 點算好QAQ";
    private string Statement2 = "村民　:　唉!!等等!!前面個位少年!!見你一身裝備，一定係勇者啦@v@ 可以幫我個忙嗎?";
    private string StatementNo = "我明白既，大家都一樣缺糧，咁無辦法啦…..唯有靠自己，再見T^T";
    private string StatementYes = "真係既?!QvQ不愧係傳說中既勇者，咁我屋企老幼靠哂你啦!@v@";
    private string Statement4 = "村民　:　Thanks!! 雖然我想搵野報答你...不過我身上無咩值錢野…哼~(思考狀";
    private string Statement5 = "村民　:　啊!就俾依個你作為報酬啦 ^v^!唔好睇少依支掃把，佢絕對係非一般掃把! =v=";
    private string Statement6 = "（勇者得到特別武器-\"普通\"後園用掃把）";
    private GameObject questCon;
    // private GameObject TextCon;
    // private string[] S;
    // private string[] st;
    // private string s = "";

    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("CanGetQuest2")) {
            CanGetQuest2 = PlayerPrefs.GetInt("CanGetQuest2");
            FinishedQuest2 = PlayerPrefs.GetInt("FinishedQuest2");
            food = PlayerPrefs.GetInt("food");
        }
    }

    void MonsterKilled() {
        if (CanGetQuest2 == 0 && FinishedQuest2 == 0) {
            if (Random.Range(1, 100) <= 70) {
                food++;
                questText.text += "食物 X 1\n";
            }
            if (food >= 2) {
                PlayerPrefs.SetInt("food", 0);
                CanGetQuest2 = 0;
                FinishedQuest2 = 1;
                PlayerPrefs.SetInt("CanGetQuest2", 0);
                PlayerPrefs.SetInt("FinishedQuest2", 1);
                PlayerPrefs.Save();
            }
        }
    }

    void SavefoodNum() {
        if (CanGetQuest2 == 0 && FinishedQuest2 == 0) {
            PlayerPrefs.SetInt("food", food);
            PlayerPrefs.Save();
        }
    }

    IEnumerator GetQuestStatement() {
        questText.text = "";
        questText.text = Background;
        yield
        return new WaitForSeconds(1);
        questText.text = Statement1;
        yield
        return new WaitForSeconds(1);
        questText.text = Statement2;
        yield
        return new WaitForSeconds(1);

    }

    void GetNoStatement() {
        if (CanGetQuest2 == 1 && FinishedQuest2 == 0) {
            questCon = GameObject.Find("questController");
            questCon.SendMessage("GetStatement", StatementNo);
        } else {
            questText.text = "";
        }
    }

    void GetYesStatement() {
        if (CanGetQuest2 == 1 && FinishedQuest2 == 0) {
            questCon = GameObject.Find("questController");
            questCon.SendMessage("GetStatement", StatementYes);
        } else {
            questText.text = "";
        }
    }

    IEnumerator GetFinishStatement() {
        questText.text = "";
        questText.text = Statement4;
        yield
        return new WaitForSeconds(1);
        questText.text = Statement5;
        yield
        return new WaitForSeconds(1);
        questText.text = Statement6;
        yield
        return new WaitForSeconds(1);
        PlayerPrefs.SetInt("CanGetQuest2", 1);
        PlayerPrefs.SetInt("FinishedQuest2", 1);
        PlayerPrefs.SetInt("WeaponNo", 2);
        PlayerPrefs.Save();
    }

    void QuestAccept() {
        CanGetQuest2 = 0;
        FinishedQuest2 = 0;
        PlayerPrefs.SetInt("CanGetQuest2", 0);
        PlayerPrefs.SetInt("FinishedQuest2", 0);
        PlayerPrefs.Save();
    }
}