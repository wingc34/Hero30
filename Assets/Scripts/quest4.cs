using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class quest4: MonoBehaviour {
    public Text questText;

    private int CanGetQuest4 = 1;
    private int FinishedQuest4 = 0;

    private int wood = 0;

    private string Background = "伐木師傅 : 咩話?!你竟然要去魔王城!依家出面天下大亂，去果到咪即係送死!0O0";
    private string Statement1 = "伐木師傅 : ......好啦，既然你堅持，我可以幫你整，不過依家倉庫唔夠材料，村外咁多怪物你唔係要我搵呀0皿0";
    private string Statement2 = "伐木師傅 : 你就出去幫我收集十塊木頭啦?";

    private string StatementNo = "咁我幫唔到你啦，你有需要先再搵我啦，再見-_-";
    private string StatementYes = "咁你要加油啦，祝你好運 -v-";
    private string Statement4 = "伐木師傅 : 估唔到有真係俾你搵到十塊木頭，我仲以為你已經死左（劃掉）";
    private string Statement5 = "伐木師傅 : ......無，我無講咩，我去修橋先啦=v=\"";
    private string Statement6 = "（地圖更新，斷橋已修好，可通往魔王城）";
    private GameObject questCon;

    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("CanGetQuest4")) {
            CanGetQuest4 = PlayerPrefs.GetInt("CanGetQuest4");
            FinishedQuest4 = PlayerPrefs.GetInt("FinishedQuest4");
            wood = PlayerPrefs.GetInt("wood");
        }
    }

    void MonsterKilled() {
        if (CanGetQuest4 == 0 && FinishedQuest4 == 0) {
            if (Random.Range(1, 100) <= 70) {
                wood++;
                questText.text += "木頭 X 1\n";
            }
            if (wood >= 2) {
                PlayerPrefs.SetInt("wood", 0);
                CanGetQuest4 = 0;
                FinishedQuest4 = 1;
                PlayerPrefs.SetInt("CanGetQuest4", 0);
                PlayerPrefs.SetInt("FinishedQuest4", 1);
                PlayerPrefs.Save();
            }
        }
    }

    void SavewoodNum() {
        if (CanGetQuest4 == 0 && FinishedQuest4 == 0) {
            PlayerPrefs.SetInt("wood", wood);
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
        if (CanGetQuest4 == 1 && FinishedQuest4 == 0) {
            questCon = GameObject.Find("questController");
            questCon.SendMessage("GetStatement", StatementNo);
        } else {
            questText.text = "";
        }
    }

    void GetYesStatement() {
        if (CanGetQuest4 == 1 && FinishedQuest4 == 0) {
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
    }

    void QuestAccept() {
        CanGetQuest4 = 0;
        FinishedQuest4 = 0;
        PlayerPrefs.SetInt("CanGetQuest4", 0);
        PlayerPrefs.SetInt("FinishedQuest4", 0);
        PlayerPrefs.Save();
    }
}