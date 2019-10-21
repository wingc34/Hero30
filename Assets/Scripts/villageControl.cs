using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class villageControl: MonoBehaviour {
    public Text TimeText;
    public Text MoneyText;
    public Image select1;
    public Image select2;
    public GameObject selectGameObject;
    public Image select3;
    public Text select3Text;
    public Image select4;
    public Sprite selected;
    public Text ConText;
    public GameObject selectConGameObject;
    public Image Conselect1;
    public Image Conselect2;
    public Text ConselectText1;
    public Text ConselectText2;
    public Image HP;
    public Image MissionMark;
    public Animator girl;
    public Animator boy;
    public Animator girlgod;

    public GameObject Girl;
    public GameObject Boy;
    public GameObject Girlgod;


    public GameObject BG1;
    public GameObject BG2;
    public GameObject BG3;

    private GameObject BG;
    private int heroMoney;
    private int position = 1;
    private int Conposition = 0;
    private int randMission;
    private bool isItemHouse = false;
    private bool isConSelect = false;
    private bool MissionAccept = false;
    private bool isMission = false;
    private bool printQuest = false;
    private bool EndOfPrintingQuest = false;
    private GameObject questCon;
    private GameObject texting;
    private bool canMoveUpDown = true;
    private string acceptStatement = "你是否接受該任務？"; //"你是否接受該任務"；

    private float timer = 30;
    private int heroHP = 100;
    private int money = 0;
    private int recoverMoney = 100;
    private int itemMoney = 10;
    private int area = 1;
    private Color tempColor1;
    private Color tempColor2;
    private Color tempColor3;
    private Color tempColor4;
    private Color conTempColor1;
    private Color conTempColor2;
    private Vector2 selectedPos;
    // Start is called before the first frame update
    void Start() {
        if (PlayerPrefs.HasKey("heroHP")) {
            heroHP = PlayerPrefs.GetInt("heroHP");
        }
        if (PlayerPrefs.HasKey("heroMoney")) {
            heroMoney = PlayerPrefs.GetInt("heroMoney");
        }
        if (PlayerPrefs.HasKey("recoverMoney")) {
            recoverMoney = PlayerPrefs.GetInt("recoverMoney");
        }

        if (PlayerPrefs.HasKey("area")) {
            area = PlayerPrefs.GetInt("area");
        }
        SetBG();

        SetItemEst();

        questCon = GameObject.Find("questController");
        texting = GameObject.Find("Texting");

        MissionMark.enabled = false;

        select1.sprite = selected;

        if (isItemHouse == false)
            select3Text.text = "離開";

        ConText.text = "";

        ConselectInvisable();
        //randMission = 1;
        randMission = Random.Range(2, 5);
        ShowMissionMark();

        if (PlayerPrefs.HasKey("FinishedQuest1")) {
            if (PlayerPrefs.GetInt("FinishedQuest1") == 1 && PlayerPrefs.GetInt("CanGetQuest1") == 0) {
                randMission = 1;
                MissionMark.enabled = true;
            }
        }

        if (PlayerPrefs.HasKey("FinishedQuest3")) {
            if (PlayerPrefs.GetInt("FinishedQuest3") == 1 && PlayerPrefs.GetInt("CanGetQuest3") == 0 && PlayerPrefs.GetInt("area") == 2) {
                randMission = 3;
                MissionMark.enabled = true;
            }
        }

        if ((PlayerPrefs.GetInt("area") != 3 && randMission == 2) || (PlayerPrefs.GetInt("area") != 2 && randMission == 3) || (PlayerPrefs.GetInt("area") != 3 && randMission == 4)) {
            randMission = 5;
        }

        if (PlayerPrefs.HasKey("StartQuest4") && PlayerPrefs.GetInt("area") == 3) {
            if (PlayerPrefs.GetInt("StartQuest4") == 1 && !PlayerPrefs.HasKey("CanGetQuest4")) {
                randMission = 4;
                MissionMark.enabled = true;
            } else if (!PlayerPrefs.HasKey("CanGetQuest2") && randMission == 2) {
                randMission = 2;
                MissionMark.enabled = true;
            } else if (PlayerPrefs.HasKey("FinishedQuest2")) {
                if (PlayerPrefs.GetInt("FinishedQuest2") == 1 && PlayerPrefs.GetInt("CanGetQuest2") == 0 && PlayerPrefs.GetInt("area") == 3) {
                    randMission = 2;
                    MissionMark.enabled = true;
                }
            } else {
                randMission = 5;
                MissionMark.enabled = false;
            }
        }
        if (PlayerPrefs.HasKey("FinishedQuest4")) {
            if (PlayerPrefs.GetInt("FinishedQuest4") == 1 && PlayerPrefs.GetInt("CanGetQuest4") == 0 && PlayerPrefs.GetInt("area") == 3) {
                randMission = 4;
                MissionMark.enabled = true;
            }
        }

        HP.rectTransform.sizeDelta = new Vector2((float)(heroHP * 2.5), 30);
        HP.transform.position = new Vector2(40, HP.transform.position.y);

        if (area == 1 || area == 3) {
            Boy.SetActive(true);
            Girl.SetActive(false);
            girl.SetInteger("girlchange", 0);
            boy.SetInteger("change", 0);
        } else {
            Boy.SetActive(false);
            Girl.SetActive(true);
            girl.SetInteger("girlchange", 0);
            boy.SetInteger("change", 0);
        }
        selectedPos = new Vector2(selectGameObject.transform.localPosition.x, selectGameObject.transform.localPosition.y);
        tempColor1 = select1.GetComponent < Image > ().color;
        tempColor2 = select2.GetComponent < Image > ().color;
        tempColor3 = select3.GetComponent < Image > ().color;
        tempColor4 = select4.GetComponent < Image > ().color;
        conTempColor1 = Conselect1.GetComponent < Image > ().color;
        conTempColor2 = Conselect2.GetComponent < Image > ().color;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("down")) {
            if (isConSelect == false)
                position += 1;
            else
                Conposition += 1;
        }

        if (Input.GetKeyDown("up")) {
            if (isConSelect == false)
                position -= 1;
            else
                Conposition -= 1;
        }

        //}


        if (isItemHouse == false && isConSelect == false) {
            if (position > 3)
                position = 3;

            if (Input.GetKeyDown("c")) {
                switch (position) {
                    case 1:
                        ConText.text = "要$" + recoverMoney.ToString() + "補血";
                        Conselectappear();
                        Girl.SetActive(false);
                        Girlgod.SetActive(true);
                        Boy.SetActive(false);
                        girlgod.SetInteger("change", 0);
                        break;
                    case 2:
                        //randMission = Random.Range(1,5);
                        //questCon.SendMessage("Quest",randMission);
                        if (MissionMark.enabled == true)
                            isMission = true;
                        //texting.SendMessage("SetQuestNum", randMission);
                        questCon.SendMessage("Quest", randMission);
                        if (area == 1 || area == 3) {
                            //print("Boy");
                            Boy.SetActive(true);
                            Girl.SetActive(false);
                            girl.SetInteger("girlchange", 0);
                            boy.SetInteger("change", 0);
                        } else {
                            Boy.SetActive(false);
                            Girl.SetActive(true);
                            girl.SetInteger("girlchange", 0);
                            boy.SetInteger("change", 0);
                        }
                        Girlgod.SetActive(false);
                        Conselectappear();
                        //print(randMission);
                        break;
                    case 3:
                        BackToMap();
                        break;
                }
            }


            switch (position) { //change target sprite if you want to select
                case 1:
                    select1.sprite = selected;
                    tempColor1.a = 1;
                    tempColor2.a = 0;
                    tempColor3.a = 0;
                    break;
                case 2:
                    select2.sprite = selected;
                    tempColor2.a = 1;
                    tempColor1.a = 0;
                    tempColor3.a = 0;
                    break;
                case 3:
                    select3.sprite = selected;
                    tempColor3.a = 1;
                    tempColor1.a = 0;
                    tempColor2.a = 0;
                    break;
            }
        }
        select1.GetComponent < Image > ().color = tempColor1;
        select2.GetComponent < Image > ().color = tempColor2;
        select3.GetComponent < Image > ().color = tempColor3;

        if (position < 1)
            position = 1;

        ConSelection();

        if (Conposition > 2)
            Conposition = 2;

        if (Conposition < 1)
            Conposition = 1;

        //itemCase();


        print(position + "      " + Conposition);
        HP.rectTransform.sizeDelta = new Vector2((float)(heroHP * 2.5), 30);
        TimeText.text = timer.ToString("F2");
        MoneyText.text = heroMoney.ToString();
    }
    void BackToMap() {
        PlayerPrefs.SetFloat("timer", timer);
        PlayerPrefs.SetInt("heroMoney", heroMoney);
        PlayerPrefs.SetInt("recoverMoney", recoverMoney);
        PlayerPrefs.SetFloat("heroHP", heroHP);
        PlayerPrefs.Save();
        Application.LoadLevel(1);
    }
    void itemCase() {
        if (isItemHouse) {
            switch (position) { //change target sprite if you want to select
                case 1:
                    select1.sprite = selected;
                    tempColor1.a = 1;
                    tempColor2.a = 0;
                    tempColor3.a = 0;
                    tempColor4.a = 0;
                    selectedPos.y = -5;
                    break;
                case 2:
                    select2.sprite = selected;
                    tempColor2.a = 1;
                    tempColor1.a = 0;
                    tempColor3.a = 0;
                    tempColor4.a = 0;
                    break;
                case 3:
                    select3.sprite = selected;
                    tempColor3.a = 1;
                    tempColor1.a = 0;
                    tempColor2.a = 0;
                    tempColor4.a = 0;
                    break;
                case 4:
                    select4.sprite = selected;
                    tempColor4.a = 1;
                    tempColor1.a = 0;
                    tempColor2.a = 0;
                    tempColor3.a = 0;
                    selectedPos.y = 38;
                    break;
            }
            select1.GetComponent < Image > ().color = tempColor1;
            select2.GetComponent < Image > ().color = tempColor2;
            select3.GetComponent < Image > ().color = tempColor3;
            select4.GetComponent < Image > ().color = tempColor4;
            selectGameObject.transform.localPosition = selectedPos;
            if (position > 4)
                position = 4;
            if (Input.GetKeyDown("c")) {
                switch (position) {
                    case 1:
                        ConText.text = "要$" + recoverMoney.ToString() + "補血";
                        Conselectappear();

                        break;
                    case 2:
                        //randMission = Random.Range(1,5);
                        //questCon.SendMessage("Quest",randMission);
                        if (MissionMark.enabled == true)
                            isMission = true;
                        questCon.SendMessage("Quest", randMission);
                        //texting.SendMessage("SetQuestNum", randMission);
                        //yield WaitForSeconds(randMission == 1 ? 3 : 0.25);
                        //canMoveUpDown= false;
                        //if(EndOfPrintingQuest)
                        Conselectappear();
                        print(randMission);
                        break;
                    case 3:
                        if (!PlayerPrefs.HasKey("EqmNo") && !PlayerPrefs.HasKey("WeaponNo")) {
                            ConText.text = "埋黎睇埋黎揀，一套武器防具套裝只需$150";
                        } else {
                            ConText.text = "要$" + itemMoney.ToString() + "買藥草";
                        }
                        Conselectappear();
                        print("item");
                        break;
                    case 4:
                        BackToMap();
                        break;
                }
            }
        }

    }


    IEnumerator ConSelection() {
        if (isConSelect) {
            switch (Conposition) { //change target sprite if you want to select
                case 1:
                    Conselect1.sprite = selected;
                    conTempColor1.a = 1;
                    conTempColor2.a = 0;
                    break;
                case 2:
                    Conselect2.sprite = selected;
                    conTempColor2.a = 1;
                    conTempColor1.a = 0;
                    break;
            }
            Conselect1.GetComponent < Image > ().color = conTempColor1;
            Conselect2.GetComponent < Image > ().color = conTempColor2;
            yield return new WaitForSeconds((float)0.5);
            if (Input.GetKeyDown("c")) {
                if (position == 1) { //for 女神
                    if (Conposition == 1) {
                        heroHP = 100;
                        heroMoney -= recoverMoney;
                        recoverMoney += 100;
                        yield
                        return new WaitForSeconds((float)0.1);
                        print("HP:" + heroHP);
                        print("Money:" + recoverMoney);
                        ConText.text = "thanks";
                        yield
                        return new WaitForSeconds((float)0.25);
                        ConText.text = "";
                        girlgod.SetInteger("change", 1);
                        ConselectInvisable();

                    }
                    if (Conposition == 2) {
                        ConText.text = "QAQ";
                        yield
                        return new WaitForSeconds((float)0.25);
                        ConText.text = "";
                        girlgod.SetInteger("change", -1);
                        ConselectInvisable();

                    }
                }
                if (position == 2) { //for 村民
                    if (Conposition == 1) {
                        //yield WaitForSeconds(0.1);
                        if (MissionAccept == false && randMission <= 4) {
                            ConText.text = "mission accept";
                            questCon.SendMessage("QuestAccepted", randMission);
                            MissionAccept = true;
                        } else {
                            ConText.text = "快啲去做任務";
                            //ConText.text = "Nothing";
                        }
                        girl.SetInteger("girlchange", 1);
                        boy.SetInteger("change", 1);
                        yield
                        return new WaitForSeconds(1);
                        ConText.text = "";
                        ConselectInvisable();
                    }
                    if (Conposition == 2) {
                        if (randMission <= 2) {
                            questCon.SendMessage("GetNoStatement", randMission);
                        } else {
                            ConText.text = "TAT";
                        }
                        girl.SetInteger("girlchange", -1);
                        boy.SetInteger("change", -1);
                        yield
                        return new WaitForSeconds(1);
                        ConText.text = "";
                        ConselectInvisable();
                    }
                }
                if (position == 3 && isItemHouse) { //for 補品站
                    if (Conposition == 1) {
                        yield
                        return new WaitForSeconds((float)0.1);
                        if (!PlayerPrefs.HasKey("EqmNo") && !PlayerPrefs.HasKey("WeaponNo")) {
                            if (heroMoney >= 150) {
                                heroMoney -= 150;
                                PlayerPrefs.SetInt("WeaponNo", 1);
                                PlayerPrefs.SetInt("EqmNo", 1);
                                PlayerPrefs.Save();
                                ConText.text = "item get";
                            } else {
                                ConText.text = "Sorry, you have not enough money.";
                            }
                        }
                        yield
                        return new WaitForSeconds((float)0.25);
                        ConText.text = "";
                        ConselectInvisable();
                    }
                    if (Conposition == 2) {
                        ConText.text = "$V$";
                        yield
                        return new WaitForSeconds((float)0.25);
                        ConText.text = "";
                        ConselectInvisable();
                    }
                }
            }
        }
    }
    void SetItemEst() {
        int value = 0;
        if (PlayerPrefs.HasKey("WeaponEst")) {
            value = PlayerPrefs.GetInt("WeaponEst");
        }
        if (value == 1)
            isItemHouse = true;
        else
            isItemHouse = false;

    }
    void ConselectInvisable() {
        Conselect1.rectTransform.localScale = new Vector2(0, 0);
        Conselect2.rectTransform.localScale = new Vector2(0, 0);
        ConselectText1.text = "";
        ConselectText2.text = "";
        isConSelect = false;
    }
    IEnumerator Conselectappear() {
        yield
        return new WaitForSeconds(isMission ? (randMission > 1 ? 3 : 4) : (float)0.25);
        Conselect1.rectTransform.localScale = new Vector2(1, 1);
        Conselect2.rectTransform.localScale = new Vector2(1, 1);
        ConselectText1.text = "Yes";
        ConselectText2.text = "No";
        isConSelect = true;
        isMission = false;
    }

    void GetStatement(string s) {
        ConText.text = s;
    }

    void ShowMissionMark() {
        if (!PlayerPrefs.HasKey("CanGetQuest1") && randMission == 1) {
            MissionMark.enabled = true;
        } else if (!PlayerPrefs.HasKey("CanGetQuest2") && randMission == 2 && PlayerPrefs.GetInt("area") == 3) {
            MissionMark.enabled = true;
        } else if (PlayerPrefs.GetInt("FinishedQuest1") == 0 && PlayerPrefs.GetInt("CanGetQuest1") == 1 && randMission == 1) {
            MissionMark.enabled = true;
        } else if (!PlayerPrefs.HasKey("CanGetQuest3") && randMission == 3 && PlayerPrefs.GetInt("area") == 2) {
            MissionMark.enabled = true;
        }

    }
    void SetEndOfPrintingQuest(bool EndOfPrintingQuest) {
        this.EndOfPrintingQuest = EndOfPrintingQuest;
        canMoveUpDown = true;
        ConText.text = acceptStatement;
        Conselectappear();
    }

    void SetBG() {
        switch (area) { //change target sprite if you want to select
            case 1:
                BG = Instantiate(BG1, new Vector3(0, 0, 0), transform.rotation);
                break;
            case 2:
                BG = Instantiate(BG2, new Vector3(0, 0, 0), transform.rotation);
                break;
            case 3:
                BG = Instantiate(BG3, new Vector3(0, 0, 0), transform.rotation);
                break;
        }
    }
}