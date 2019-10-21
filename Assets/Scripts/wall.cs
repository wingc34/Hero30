using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wall : MonoBehaviour
{
    public GameObject block;
    public Image hintImage;
    public Text hintText;
    private bool canQ4 = false;
    private GameObject newTmp;


    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("StartQuest4") || PlayerPrefs.GetInt("StartQuest4") == 1){
		newTmp = Instantiate(block, transform.position, transform.rotation);
		canQ4 = true;
	}
        hintImage.enabled = false;
        hintText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D (Collider2D collider){
	if (collider.gameObject.tag == "Player" && canQ4 == true) {
			hintImage.enabled = true;
			hintText.enabled = true;
			if (!PlayerPrefs.HasKey("StartQuest4")){
         		PlayerPrefs.SetInt("StartQuest4", 1);
				PlayerPrefs.Save();
			}
        }
}

    void OnTriggerExit2D (Collider2D collider) {
        if (collider.gameObject.tag == "Player" && canQ4 == true) {
            hintImage.enabled = false;
            hintText.enabled = false;
        }
    }
}
