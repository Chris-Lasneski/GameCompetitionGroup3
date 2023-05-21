using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Slider SensitivitySlider;
    [SerializeField] private Text SensitivitySliderText;

    public float sensitivityValue = 0f;
    public PlayerInfo player;
    public GameObject selfMenu;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        SensitivitySlider.onValueChanged.AddListener((v) =>
        {
            SensitivitySliderText.text = "Camera Sensitivity: " + v.ToString("0.0");
            sensitivityValue = SensitivitySlider.value;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && player.subMenu && selfMenu.activeSelf)
        {
            player.subMenu = !player.subMenu;
            selfMenu.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }
}
