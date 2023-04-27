using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        // can't quit in inspector so logging to check it worked
        Debug.Log("Quit the game :(");
    }
}
