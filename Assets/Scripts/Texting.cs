using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//[RequireComponent(typeof(Text))]
public class Texting : MonoBehaviour {
    //public GameObject textBox;

    public TextAsset textFile;
    public TextAsset Quest1;
  
    public int currentLine;
    public int endAtLine;

    public Text _textComponent;

    private string[] DialogueString;
    private string[] FullQuestString;

    public float SecondsBetweenCharacters = 0.15f;
    public float CharacterRateMultiplier = 1f;

	public KeyCode DialogueInput = KeyCode.C;

    private bool _isStringBeingRevealed = false;
    private bool _isDialoguePlaying = false;
    private bool _isEndOfDialogue = false;
    private bool _isStartGetQuest = false;

    private int QuestNo = -1;


    public GameObject ContinueIcon;
    public GameObject estCon;

    // Use this for initialization
    void Start () {
       // _textComponent = GetComponent<Text>();
        _textComponent.text = "";
        if (Quest1 != null)
        {
            FullQuestString = Quest1.text.Split('\n');
        }
//DialogueString = new string[8];
        /*if(endAtLine == 0)
        {
            endAtLine = DialogueString.Length- 1;
        }*/
        HideIcon();

    }

    // Update is called once per frame
    void Update()
    {
        if (_isStartGetQuest) { 
        if (Input.GetKeyDown(DialogueInput))
        {
            if (!_isDialoguePlaying)
            {
                _isDialoguePlaying = true;
                StartCoroutine(StartDialogue());
                print("printQuest");
            }

        }
    }
	}

    private IEnumerator StartDialogue()
    {
        int dialogueLength = DialogueString.Length;
        int currentDialogueIndex = 0;

        while (currentDialogueIndex < dialogueLength || !_isStringBeingRevealed)
        {
            if (!_isStringBeingRevealed)
            {
                _isStringBeingRevealed = true;
                StartCoroutine(DisplayString(DialogueString[currentDialogueIndex++]));

                if(currentDialogueIndex <= dialogueLength)
                {
                    _isEndOfDialogue = true;
                }
            }
            yield return 0;
        }
        while (true)
        {
            if (Input.GetKeyDown(DialogueInput))
            {
                break;
            }

            yield return 0;
        }

        HideIcon();
        if (_isEndOfDialogue)
        {
            estCon.SendMessage("SetEndOfPrintingQuest", _isEndOfDialogue);

        }
        _isEndOfDialogue = false;
        _isDialoguePlaying = false;
    }


    private IEnumerator DisplayString(string stringToDisplay)
    {
        print("111 line " + stringToDisplay.Length);
        int stringLength = stringToDisplay.Length;
        int currentCharacterIndex = 0;

        HideIcon();

        _textComponent.text = "";

        while (currentCharacterIndex < stringLength)
        {
            _textComponent.text += stringToDisplay[currentCharacterIndex];
            currentCharacterIndex++;

            if(currentCharacterIndex < stringLength)
            {
                if(Input.GetKey(DialogueInput))
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters * CharacterRateMultiplier);
                }
                else
                {
                    yield return new WaitForSeconds(SecondsBetweenCharacters);
                }         
            }
            else
            {
                break;
            }
        }

        ShowIcon();

        while(true)
        {
            if (Input.GetKeyDown(DialogueInput))
            {
                break;
            }

            yield return 0;
        }

        HideIcon();

        _isStringBeingRevealed = false;
        _textComponent.text = "";
    }

    private void HideIcon()
    {
        ContinueIcon.SetActive(false);
    }
    private void ShowIcon()
    {
        ContinueIcon.SetActive(true);
    }
    public void SetQuestNum(int QuestNo)
    {
        this.QuestNo = QuestNo;
    }
    public void SetPrintQuest(bool printQuest)
    {
        _isStartGetQuest = printQuest;
    }
    public void GetStartQuest()
    {
        int i = 0;
        int j = 0;
        while (FullQuestString[j+1].Trim().Equals("end") == false)
        {
            j++;
        }

        DialogueString = new string[j];

        while (i < FullQuestString.Length)
        {
            print(FullQuestString[i].Trim().Equals("start")) ;
            if (FullQuestString[i].Trim().Equals("start")  == true)  //string.Compare(FullQuestString[i], "start") == 1)//if (FullQuestString[i] == "start")
            {
                //print(FullQuestString[i]);
                while (FullQuestString[i+1].Trim().Equals("end") == false)
                {
                    DialogueString[i] = FullQuestString[i + 1];
                    i++;
                   // if (FullQuestString[i + 1].Trim().Equals("end") == true) {
                        //break;
                   // }
                }
            }
            break;
        }
        print("Dialoguelength: " + DialogueString.Length);
        
    }

	public void GetYesQuest()
	{
		int i = 0;
		int j = 0;
		while (FullQuestString[j+1].Trim().Equals("yesend") == false)
		{
			j++;
		}

		DialogueString = new string[j];

		while (i < FullQuestString.Length)
		{
			print(FullQuestString[i].Trim().Equals("yesstart")) ;
			if (FullQuestString[i].Trim().Equals("yesstart")  == true)  //string.Compare(FullQuestString[i], "start") == 1)//if (FullQuestString[i] == "start")
			{
				//print(FullQuestString[i]);
				while (FullQuestString[i+1].Trim().Equals("yesend") == false)
				{
					DialogueString[i] = FullQuestString[i + 1];
					i++;
					// if (FullQuestString[i + 1].Trim().Equals("end") == true) {
					//break;
					// }
				}
			}
			break;
		}
		print("Dialoguelength: " + DialogueString.Length);

	}

	public void GetWinStart()
	{
		int i = 0;
		int j = 0;
		int n = 0;
		while (FullQuestString[j+1].Trim().Equals("winend") == false)
		{
			if (FullQuestString[j].Trim().Equals("winstart")  == true)  //string.Compare(FullQuestString[i], "start") == 1)//if (FullQuestString[i] == "start")
			{
				n = j;
			}
			j++;
		}

		DialogueString = new string[j-n];
		n = 0;

		while (i < FullQuestString.Length)
		{
			print(FullQuestString[i].Trim().Equals("winstart")) ;
			if (FullQuestString[i].Trim().Equals("winstart")  == true)  //string.Compare(FullQuestString[i], "start") == 1)//if (FullQuestString[i] == "start")
			{
				//print(FullQuestString[i]);
				while (FullQuestString[i+1].Trim().Equals("winend") == false)
				{
					DialogueString[i] = FullQuestString[i + 1];
					i++;
					n++;
					// if (FullQuestString[i + 1].Trim().Equals("end") == true) {
					//break;
					// }
				}
			}
			else {
				i++;
			}
			break;
		}
		print("Dialoguelength: " + DialogueString.Length);

	}

	public void GetLoseStart()
	{
		int i = 0;
		int j = 0;
		int n = 0;
		while (FullQuestString[j+1].Trim().Equals("loseend") == false)
		{
			if (FullQuestString[j].Trim().Equals("losestart")  == true)  //string.Compare(FullQuestString[i], "start") == 1)//if (FullQuestString[i] == "start")
			{
				n = j;
			}
			j++;
		}

		DialogueString = new string[j-n];
		n = 0;

		while (i < FullQuestString.Length)
		{
			print(FullQuestString[i].Trim().Equals("losestart")) ;
			if (FullQuestString [i].Trim ().Equals ("losestart") == true) {  //string.Compare(FullQuestString[i], "start") == 1)//if (FullQuestString[i] == "start")
				//print (FullQuestString [i]);
				while (FullQuestString [i + 1].Trim ().Equals ("loseend") == false) {
					DialogueString [n] = FullQuestString [i + 1];
					i++;
					n++;
					// if (FullQuestString[i + 1].Trim().Equals("end") == true) {
					//break;
					// }
				}
			} else {
				i++;
			}
			//break;
		}
		print("Dialoguelength: " + DialogueString.Length);

	}
}
