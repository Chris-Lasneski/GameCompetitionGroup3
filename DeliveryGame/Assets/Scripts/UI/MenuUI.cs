using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public GameObject menu;
    public bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);

        //Button btn = exitButton.GetComponent<Button>();
        //btn.onClick.AddListener(ExitTaskOnClick);
        //Button rbtn = resumeButton.GetComponent<Button>();
        //rbtn.onClick.AddListener(ResumeButtonClick);
        //Button sbtn = settingsButton.GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            PauseHandler(paused);
        }
    }

    public void PauseHandler( bool p)
    {
        if (paused)
        {
            menu.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}