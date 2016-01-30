using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    CanvasGroup mainMenuCanvas;
    CanvasGroup introAnimationCanvas;

    GameObject levelOne;

    public bool atMainMenu;

	// Use this for initialization
	void Start ()
    {
        
        mainMenuCanvas= GameObject.Find("MainMenuCanvas").GetComponent<CanvasGroup>();
        introAnimationCanvas = GameObject.Find("IntroCanvas").GetComponent<CanvasGroup>();
        levelOne = GameObject.Find("LevelOne");
        

        showMainMenu();
    }

    private void showMainMenu()
    {
        levelOne.SetActive(false);

        introAnimationCanvas.alpha = 0f;
        introAnimationCanvas.interactable = false;
        introAnimationCanvas.blocksRaycasts = false;

        mainMenuCanvas.alpha = 1f;
        mainMenuCanvas.interactable = true;
        mainMenuCanvas.blocksRaycasts = true;

        atMainMenu = true;

    }

    // Update is called once per frame
    void Update ()
    {
        CheckInput();
	
	}

    private void CheckInput()
    {
        if(atMainMenu)
        {

            if(Input.anyKeyDown)
            {
                atMainMenu = false;
                StartCoroutine(LaunchSplash());
            }
        }
    }

    IEnumerator LaunchSplash()
    {
        mainMenuCanvas.alpha = 0f;
        mainMenuCanvas.interactable = false;
        mainMenuCanvas.blocksRaycasts = false;

        introAnimationCanvas.alpha = 1f;
        introAnimationCanvas.interactable = true;
        introAnimationCanvas.blocksRaycasts = true;

        yield return new WaitForSeconds(.1f);

        StartGame();

    }

    private void StartGame()
    {
        introAnimationCanvas.alpha = 0f;
        introAnimationCanvas.interactable = false;
        introAnimationCanvas.blocksRaycasts = false;

        levelOne.SetActive(true);
    }
}
