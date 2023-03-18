using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Slider SensitivitySlider;
    [SerializeField] private Text SensitivitySliderText;

    public float sensitivityValue = 0f;

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
        
    }
}
