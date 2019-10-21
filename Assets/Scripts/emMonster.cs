using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emMonster: MonoBehaviour {
    public GameObject Monster;
    public GameObject Monster2;
    public GameObject Monster3;
    public GameObject Monster4;
    public GameObject Monster5;
    public GameObject Monster6;
    public GameObject Boss;

    public GameObject BG1;
    public GameObject BG2;
    public GameObject BG3;
    public GameObject bossBG;

    private GameObject emMon;
    private GameObject controller;
    private bool emMon2ED = false;
    private int area = 1;
    private GameObject normalMon;
    private GameObject spMon;
    private GameObject BG;
    private GameObject[] cloneNo;

    // Start is called before the first frame update
    void Start() {
        controller = GameObject.Find("Controller");
        if (PlayerPrefs.HasKey("area")) {
            area = PlayerPrefs.GetInt("area");
        }
        //PlayerPrefs.SetInt("isBoss", 3);
        PlayerPrefs.Save();
        StartCoroutine(InsMonster()); //if (isBoss ==1) , the monster scene is the temp for boss scene
    }

    // Update is called once per frame
    void Update() {

    }
    IEnumerator InsMonster() {
        yield return new WaitForSeconds((float)0.1);
        int num;
        int isMon2;
        int i;

        switch (area) { //change target sprite if you want to select
            case 1:
                BG = Instantiate(BG1, new Vector3(0, (float)-0.2, 0), transform.rotation);
                num = Random.Range(0, 2);
                cloneNo = new GameObject[num + 1];
                controller.SendMessage("GetTatolMonster", num);
                for (i = 0; i <= num; i++) {
                    isMon2 = Random.Range(1, 25);
                    if (isMon2 <= 1 && !emMon2ED) {
                        cloneNo[i] = Instantiate(Monster2, new Vector3(transform.position.x, -2, transform.position.z), transform.rotation);
                        emMon2ED = true;
                    } else {
                        cloneNo[i] = Instantiate(Monster, new Vector3(transform.position.x, -3, transform.position.z), transform.rotation);
                    }
                    //transform.position.x += 2;
                    //yield return new (0.25);
                }
                break;
            case 2:
                BG = Instantiate(BG2, new Vector3(0, 0, 0), transform.rotation);
                isMon2 = Random.Range(1, 25);
                if (isMon2 <= 1) {
                    cloneNo = new GameObject[1];
                    controller.SendMessage("GetTatolMonster", 0);
                    cloneNo[0] = Instantiate(Monster4, new Vector3(transform.position.x, (float)-1.75, transform.position.z), transform.rotation);
                } else {
                    num = Random.Range(0, 2);
                    cloneNo = new GameObject[num + 1];
                    controller.SendMessage("GetTatolMonster", num);
                    for (i = 0; i <= num; i++) {
                        cloneNo[i] = Instantiate(Monster3, transform.position, transform.rotation);
                        //transform.position.x += 2;
                        //yield WaitForSeconds(0.25);
                    }
                }
                break;
            case 3:
                BG = Instantiate(BG3, new Vector3(0, (float)-0.2, 0), transform.rotation);
                isMon2 = Random.Range(1, 100);
                cloneNo = new GameObject[1];
                if (isMon2 <= 1) {
                    controller.SendMessage("GetTatolMonster", 0);
                    cloneNo[0] = Instantiate(Monster6, new Vector3(transform.position.x, (float)-1.6, transform.position.z), transform.rotation);
                } else {
                    controller.SendMessage("GetTatolMonster", 0);
                    cloneNo[0] = Instantiate(Monster5, new Vector3(transform.position.x, (float)-1.6, transform.position.z), transform.rotation);
                }
                break;
            case 4:
                //BG = Instantiate(bossBG, new Vector3(0, -0.2, 0), transform.rotation);
                cloneNo = new GameObject[1];
                cloneNo[0] = Instantiate(Boss, new Vector3(transform.position.x, (float)-1.6, transform.position.z), transform.rotation);
                break;
        }
        controller.SendMessage("GetMonsters", cloneNo);
    }

}