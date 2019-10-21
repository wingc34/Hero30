using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroMap : MonoBehaviour
{
    public float heroHP = 100;
    public float heroAttack = 10;
    public float heroDef = 10;

    public float heroMoveSpeed = 10;

    public int heroMoney = 100;
    public Animator AnimationRun;
    public AudioSource walkingSound;

    private SpriteRenderer Sprite;

    private float PosX;
    private float PosY; 
    private bool canLeft = true;
    private int countLeft = 0;
    private bool canRight = true;
    private int countRight = 0;
    private bool canUp = true;
    private int countUp = 0;
    private bool canDown = true;
    private int countDown = 0;
    private bool canBattle = false;//wheather can on cant battle after change scene
    private GameObject mapController;
    private int area = 1;//area1 = normal, 2 = flower, 3 = forest, 4 = boss
    private int WeaponEst;
    private float elaspedTime;
    private float timer = 30;
    private float Base;
    private float h;
    private float v;
    private float posDiff;
    void Start()
    {
        //PlayerPrefs.DeleteAll(); //testing at only one scene 
        if(PlayerPrefs.HasKey("area")){
            Vector2 heroPos = new Vector2(PlayerPrefs.GetFloat("HeroPosX"),PlayerPrefs.GetFloat("HeroPosY"));
            transform.position = heroPos;
            heroHP = PlayerPrefs.GetFloat("heroHP");
            heroMoney = PlayerPrefs.GetInt("heroMoney");
            area = PlayerPrefs.GetInt("area");
        }
        mapController = GameObject.Find("Controller");
        Sprite = GameObject.Find("Hero_excel").GetComponent<SpriteRenderer>();
        //this part will move to controller laster
        if (PlayerPrefs.HasKey("timer")){
            timer = PlayerPrefs.GetFloat("timer");
        }

        PosX = transform.position.x;
        PosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
    timer -= Time.deltaTime;

	/*if (timer < 0){
		Time.timeScale = 0.0;
		// this is for if it is lose, and will change to lose scene or other develop eg. Lose()
	}*/


	Base = heroMoveSpeed*Time.deltaTime;
	h = 0;
	v = 0;

	if(!(Input.GetKey("left")&&Input.GetKey("right")&&Input.GetKey("up")&&Input.GetKey("down"))){
		AnimationRun.SetInteger("Run",3);
		}

	if (Input.GetKey("left") && canLeft){
		h = -1;
		HeroRun();
		Sprite.flipX = true;
		elaspedTime += Time.deltaTime;
		//GetComponent.<Rigidbody2D>().velocity = -Vector2.right * heroMoveSpeed;
	}
	if(Input.GetKey("right") && canRight){
		h = 1;
		HeroRun();
		Sprite.flipX = false;
		elaspedTime += Time.deltaTime;
		//GetComponent.<Rigidbody2D>().velocity = Vector2.right * heroMoveSpeed;
	}
	if(Input.GetKey("up") && canUp){
		v = 1;
		HeroRun();
		elaspedTime += Time.deltaTime;
		//GetComponent.<Rigidbody2D>().velocity=Vector2.up * heroMoveSpeed;
	}
	if(Input.GetKey("down") && canDown){
		v = -1;
		HeroRun();
		elaspedTime += Time.deltaTime;
		//GetComponent.<Rigidbody2D>().velocity=-Vector2.up * heroMoveSpeed;
	}

	transform.Translate(h*Base,v*Base,0,Space.World);

	posDiff += Mathf.Sqrt((PosX - transform.position.x)*(PosX - transform.position.x)+(PosY - transform.position.y)*(PosY - transform.position.y));
	PosX = transform.position.x;
	PosY = transform.position.y;
	if(posDiff > heroMoveSpeed){
		canBattle = true;
	}
	if((posDiff > 1) && canBattle ){
		//isBoss = 2;
		int num = Random.Range(0, 50);
		if (num <= 1){
			mapController.SendMessage("setTime");
			SavePerfs();
		}else{
			//isBoss = 3;
			posDiff = 0;
		}
	}

	if(Input.GetKeyDown("left") || Input.GetKeyDown("right") || Input.GetKeyDown("up") || Input.GetKeyDown("down"))
		walkingSound.Play();
	else if (Input.GetKeyUp("left") || Input.GetKeyUp("right") || Input.GetKeyUp("up") || Input.GetKeyUp("down"))
		walkingSound.Stop();
    }
    void OnTriggerEnter2D (Collider2D collider){
        if (collider.gameObject.name == "Area2"){
            area = 2;
        }
        if (collider.gameObject.name == "Area3"){
            area = 3;
        }
    }

    void OnTriggerExit2D (Collider2D collider) {
        if (collider.gameObject.name == "Area2" || collider.gameObject.name == "Area3"){
            area = 1;
        }
    }

    void OnTriggerStay2D(Collider2D collider){
        if (collider.gameObject.tag == "est" && Input.GetKey("c")) {
                //print("recover time");
                //timer = 30;
                mapController.SendMessage("recovertime");
                mapController.SendMessage("setTime");
                //Application.LoadLevel(3);
                SavePerfsInest();
        }
        if (collider.gameObject.tag == "WeaponEst" && Input.GetKey("c")){
            //WeaponEst = 1;
            SavePerfsInest();

        }

        if (collider.gameObject.tag == "Boss" && Input.GetKey("c")) {
            area = 4;
            SavePerfsBoss();
        }
    }

    void AddwpATK(float weaponATK){
        heroAttack += weaponATK;
    }

    void AddeqmDEF(float eqmDEF){
        heroDef += eqmDEF;
    }

    void AddHP(){
        heroHP += 5;
        if (heroHP > 100){
            heroHP = 100;
        }
    }

    void SavePerfs(){
            PlayerPrefs.SetFloat("HeroPosX", transform.position.x);
            PlayerPrefs.SetFloat("HeroPosY", transform.position.y);
            PlayerPrefs.SetFloat("heroHP", heroHP);
            PlayerPrefs.SetInt("area", area);
            PlayerPrefs.SetInt("heroMoney", heroMoney);
            PlayerPrefs.SetInt("WeaponEst", WeaponEst);
            PlayerPrefs.Save();
            Application.LoadLevel(1);
    }
    void SavePerfsBoss(){
            PlayerPrefs.SetFloat("HeroPosX", transform.position.x);
            PlayerPrefs.SetFloat("HeroPosY", transform.position.y);
            PlayerPrefs.SetFloat("heroHP", heroHP);
            PlayerPrefs.SetInt("area", area);
            PlayerPrefs.SetInt("heroMoney", heroMoney);
            PlayerPrefs.SetInt("WeaponEst", WeaponEst);
            PlayerPrefs.Save();
            Application.LoadLevel(2);
    }

    void SavePerfsInest(){
            PlayerPrefs.SetFloat("HeroPosX", transform.position.x);
            PlayerPrefs.SetFloat("HeroPosY", transform.position.y);
            PlayerPrefs.SetFloat("heroHP", heroHP);
            PlayerPrefs.SetInt("area", area);
            PlayerPrefs.SetInt("heroMoney", heroMoney);
            PlayerPrefs.SetInt("WeaponEst", WeaponEst);
            PlayerPrefs.Save();
            Application.LoadLevel(3);
    }

    void CanLeftKey(int count){
        countLeft += count;
        if (countLeft == 0)
            canLeft = true;
        else
            canLeft = false;
    }
    void CanRightKey(int count){
        countRight += count;
        if (countRight == 0)
            canRight = true;
        else
            canRight = false;
    }
    void CanUpKey(int count){
        countUp += count;
        if (countUp == 0)
            canUp = true;
        else
            canUp = false;
    }
    void CanDownKey(int count){
        countDown += count;
        if (countDown == 0)
            canDown = true;
        else
            canDown = false;
    }

    void Gameover(){
        Application.LoadLevel(0);
    }
    void HeroRun(){
        AnimationRun.SetInteger("Run",0);
    }
}
