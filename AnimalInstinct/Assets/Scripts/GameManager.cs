﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    CanvasGroup mainMenuCanvas;
    CanvasGroup introAnimationCanvas;
    CanvasGroup gameCanvas;
    CanvasGroup retryPanel;
    CanvasGroup winCanvas;

    GameObject levelOne;

    GameObject ownerReturnsAnimation;

    public bool atMainMenu;
    public bool atRetry;
    bool inGame;

    private bool allowCharacterMovement;

    float levelTimer = 60f;
    public float timeLeft;

    Character[] characters;

    Text timerText;

    public Transform firstLoad;

    private float timeLost;

    void Awake()
    {
        if (firstLoad)
        {
            DontDestroyOnLoad(firstLoad);
        }

        timeLost = 0f;
    }

	// Use this for initialization
	void Start ()
    {
        mainMenuCanvas= GameObject.Find("MainMenuCanvas").GetComponent<CanvasGroup>();
        introAnimationCanvas = GameObject.Find("IntroCanvas").GetComponent<CanvasGroup>();
        gameCanvas   = GameObject.Find("GameCanvas").GetComponent<CanvasGroup>();
        retryPanel = GameObject.Find("RetryPanel").GetComponent<CanvasGroup>();
        winCanvas = GameObject.Find("WinCanvas").GetComponent<CanvasGroup>();
        levelOne = GameObject.Find("LevelOne");

        ownerReturnsAnimation = GameObject.Find("OwnerReturnsAnimation");

        characters = GameObject.FindObjectsOfType<Character>();

        if (FirstLoad.firstLoad)
        {
            showMainMenu();
        }
        else
        {
            StartGame();
        }
    }

    public void EndLevel()
    {
        StartCoroutine(showBeast());
    }

    private IEnumerator showBeast()
    {

        ToggleCanvas(winCanvas, true);

        yield return new WaitForSeconds(1.5f);

        ToggleCanvas(winCanvas, false);

        SwitchCharacter(2);
        Camera.main.GetComponent<MainCamera>().SwitchPlayer();

        yield return null;
    }

    private void showMainMenu()
    {
        levelOne.SetActive(false);
        ownerReturnsAnimation.SetActive(false);

        introAnimationCanvas.alpha = 0f;
        introAnimationCanvas.interactable = false;
        introAnimationCanvas.blocksRaycasts = false;

        gameCanvas.alpha = 0f;
        gameCanvas.interactable = false;
        gameCanvas.blocksRaycasts = false;

        ToggleCanvas(retryPanel, false);

        mainMenuCanvas.alpha = 1f;
        mainMenuCanvas.interactable = true;
        mainMenuCanvas.blocksRaycasts = true;

        ToggleCanvas(winCanvas, false);

        atMainMenu = true;
        atRetry = false;
        allowCharacterMovement = false;
        FirstLoad.firstLoad = false;

    }

    public void SwitchCharacter(int type)
    {
        //cat
        if (type == 0)
        {
            foreach (Character c in characters)
            {
                if (c.type == CharacterType.Cat)
                {
                    c.canMove = true;
                    c.gameObject.tag = "Player";
                }
                else
                {
                    c.canMove = false;
                    c.gameObject.tag = "Untagged";
                }
            }
        }
        //dog
        else if(type == 1 )
        {
            foreach (Character c in characters)
            {
                if (c.type == CharacterType.Dog)
                {
                    c.canMove = true;
                    c.gameObject.tag = "Player";
                }
                else
                {
                    c.canMove = false;
                    c.gameObject.tag = "Untagged";
                }
            }

        }
        else if(type == 2)
        {
            foreach (Character c in characters)
            {
                if (c.type == CharacterType.Dragon)
                {
                    c.canMove = true;
                    c.gameObject.tag = "Player";
                }
                else
                {
                    c.canMove = false;
                    c.gameObject.tag = "Untagged";
                }
            }
        }

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
        else if(atRetry)
        {
            if (Input.anyKeyDown)
            {
                atRetry = false;
                StartGame();
                SceneManager.LoadScene(0);
            }

        }
        else if(inGame)
        {
            //allow charactermovement
            //ToggleCharacterMovement(true);

        }
    }

    private void ToggleCharacterMovement(bool on)
    {
        if (on)
        {
            /*
            foreach (Character character in characters)
            {
                character.canMove = true;
                
            }*/

            SwitchCharacter(0);

        }
        else
        {
            foreach (Character character in characters)
            {
                character.canMove = false;
            }
        }
    }

    IEnumerator LaunchSplash()
    {
        ToggleCanvas(mainMenuCanvas, false);

        ToggleCanvas(introAnimationCanvas, true);

        yield return new WaitForSeconds(.1f);

        StartGame();

    }

    private void ToggleCanvas(CanvasGroup canvas, bool on)
    {
        if (on)
        {
            canvas.alpha = 1f;
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
        }
        else
        {
            canvas.alpha = 0f;
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
        }       

    }

    private void StartGame()
    {
        ToggleCanvas(mainMenuCanvas, false);
        ToggleCanvas(introAnimationCanvas, false);
        ToggleCanvas(retryPanel, false);
        ownerReturnsAnimation.SetActive(false);
        ToggleCanvas(winCanvas, false);

        ToggleCanvas(gameCanvas, true);

        //Reset Level

        levelOne.SetActive(true);
        timerText = GameObject.Find("TimeText").GetComponent<Text>();

        StartCoroutine(StartLevelTimer());

        ToggleCharacterMovement(true);

        FirstLoad.firstLoad = false;

        inGame = true;
    }

    private IEnumerator StartLevelTimer()
    {
        float levelStartTime = Time.time;
        float levelEndTime = Time.time + levelTimer;

        timeLeft = Mathf.Abs(levelEndTime - levelStartTime);
        while(timeLeft > 0.01f)
        {
            timeLeft = Mathf.Abs(levelEndTime - Time.time);
            yield return new WaitForEndOfFrame();
            timerText.text = "Time: " + Mathf.Round(timeLeft);
            Color newBackground = timerText.transform.parent.GetComponent<Image>().color;
            newBackground.a = Mathf.Lerp(1f, 0f, ((timeLeft / levelTimer)));
           
            timerText.transform.parent.GetComponent<Image>().color = newBackground;   
        }

        yield return null;

        yield return StartCoroutine(OwnerReturns());

        //Game Ove
        //increment day by one
        //have press any key to restart

    }

    private IEnumerator OwnerReturns()
    {
        //stop movement
        ToggleCharacterMovement(false);
        //Play owner returns animation
        ownerReturnsAnimation.SetActive(true);
        yield return new WaitForSeconds(3f);
        ownerReturnsAnimation.SetActive(false);
        //display press any key to retry
        atRetry = true;

        ToggleCanvas(retryPanel, true);
    }


    public void ReduceTime()
    {
        timeLeft -= 5f;
    }
}
