using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public Image HP;
    public Text TimeText;
    public Text MoneyText;
    public GameObject BG1;
    public GameObject BG2;
    public double speedBG;

    public Vector2 bg1x;
    public Vector2 bg2x;


    private int heroMoney = 100;
    private int heroHP = 100;
    private float timer = 30;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("timer")){
			timer = PlayerPrefs.GetFloat("timer");
		}
		if (PlayerPrefs.HasKey("heroHP")){
			heroHP = PlayerPrefs.GetInt("heroHP");
		}
		if (PlayerPrefs.HasKey("heroMoney")){
			heroMoney = PlayerPrefs.GetInt("heroMoney");
		}
        Vector2 size = new Vector2((float)(heroHP * 2.5),30);
		HP.rectTransform.sizeDelta = size;
    
        Vector3 pos = new Vector3(40, HP.transform.position.y, HP.transform.position.z);
		HP.transform.position = pos;
		MoneyText.text = heroMoney.ToString();
        bg1x = new Vector2(BG1.transform.position.x, BG1.transform.position.y);
        bg2x = new Vector2(BG2.transform.position.x, BG2.transform.position.y);

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        TimeText.text = timer.ToString("F2");
        MoneyText.text = heroMoney.ToString();
        if (timer < 0){
            Time.timeScale = 0;
            gameOver();
            // this is for if it is lose, and will change to lose scene or other develop eg. Lose()
        }
        bg1x.x += (float)(Time.deltaTime*speedBG);
        BG1.transform.position = bg1x;
        bg2x.x += (float)(Time.deltaTime*speedBG);
        BG2.transform.position = bg2x;
        if(bg1x.x>222)
            bg1x.x = -222;
        if(bg2x.x>222)
            bg1x.x = -222;
    }
    void recovertime(){
        timer = 30;
        print("recover time");
        //PlayerPrefs.SetFloat("timer", timer);
        //Application.LoadLevel(3);
    }
    void setTime() {
	    PlayerPrefs.SetFloat("timer", timer);
    }

    void gameOver(){
        Application.LoadLevel(0);
    }
}
