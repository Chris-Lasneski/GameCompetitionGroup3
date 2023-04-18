using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject menu;
    public bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
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
}
