using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour
{
    public GameObject PlayImage;
    public GameObject ExitImage;
    private Color tempColor1;
    private Color tempColor2;

    private int position = 1;

    // Start is called before the first frame update
    void Start()
    {
        tempColor1 = PlayImage.GetComponent<Image>().color;
        tempColor2 = ExitImage.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
     	if(Input.GetKeyDown("down")){
		    position += 1;
		if(position > 2)
			position = 2;
	    }
	
        if(Input.GetKeyDown("up")){
            position -= 1;
            if(position < 1)
                position = 1;
        }
		
        if(Input.GetKeyDown("c")){
            switch(position){
                case 1:
                    PlayerPrefs.DeleteAll();
                    SceneManager.LoadScene(0);
                    break;
                case 2:
                    Application.Quit();
                    break;
            }
        }
		

        switch(position){						//change target sprite if you want to select
            case 1:
                tempColor1.a = 1f;
                tempColor2.a = 0f;
                PlayImage.GetComponent<Image>().color = tempColor1;
                ExitImage.GetComponent<Image>().color = tempColor2;
                break;
            case 2:
                tempColor1.a = 0f;
                tempColor2.a = 1f;
                PlayImage.GetComponent<Image>().color = tempColor1;
                ExitImage.GetComponent<Image>().color = tempColor2;
                break;
        }
    }
}
