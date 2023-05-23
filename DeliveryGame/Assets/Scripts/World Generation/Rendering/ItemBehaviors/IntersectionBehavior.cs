using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class IntersectionBehavior : MonoBehaviour
{
    public static bool toggle = false;
    private static float elapsedTime = 0;
    private static float greenTime = 8;
    private static float yellowTime = 2;
	

    public static void addTime(float dt) {
        elapsedTime += dt;
        if (elapsedTime > greenTime + yellowTime) {
            elapsedTime -= greenTime + yellowTime;
            toggle = !toggle;

        }
    }
	void Start(){
	
	}	


    private bool oppositeToggle;
    private int lightState = 0;
    private int oppositeLightState = 0;

    public GameObject[] greenLights;
    public GameObject[] yellowLights;
    public GameObject[] redLights;

    public GameObject[] oppositeGreenLights;
    public GameObject[] oppositeYellowLights;
    public GameObject[] oppositeRedLights;
	public GameObject obstacle1;
	public GameObject obstacle2;
	public GameObject obstacle3;
	public GameObject obstacle4;
	

    public GameObject[] detectors;
    public GameObject[] oppositeDetectors;

    public void set(bool oppositeToggle) {
        this.oppositeToggle = oppositeToggle;
        lightState = oppositeToggle ? toggle ? 0 : elapsedTime <= greenTime ? 2 : 1 : toggle ? elapsedTime <= greenTime ? 2 : 1 : 0;
        oppositeLightState = oppositeToggle ? toggle ? elapsedTime <= greenTime ? 2 : 1 : 0 : toggle ? 0 : elapsedTime <= greenTime ? 2 : 1;

        obstacle1.SetActive(!oppositeToggle);
        obstacle2.SetActive(!oppositeToggle);

        obstacle3.SetActive(oppositeToggle);
        obstacle4.SetActive(oppositeToggle);
        setLightState();
        setOppositeLightState();
    }

    private void Update() {
		
        if (oppositeToggle) {
			
            //red when toggle is true
            if (!toggle) {
                int newLightState = elapsedTime <= greenTime ? 2 : 1;
                int newOppositeLightState = 0;
                if (lightState != newLightState) {
                    lightState = newLightState;
                    setLightState();

                }
                if (oppositeLightState != newOppositeLightState) {
                    oppositeLightState = newOppositeLightState;
                    setOppositeLightState();

                }
            }
            else {
                int newLightState = 0;
                int newOppositeLightState = elapsedTime <= greenTime ? 2 : 1;
                if (lightState != newLightState) {
                    lightState = newLightState;
                    setLightState();

                }
                if (oppositeLightState != newOppositeLightState) {
                    oppositeLightState = newOppositeLightState;
                    setOppositeLightState();

                }
            }
        }
        else {
            //green when toggle is true
            if (toggle) {
                int newLightState = elapsedTime <= greenTime ? 2 : 1;
                int newOppositeLightState = 0;
                if (lightState != newLightState) {
                    lightState = newLightState;
                    setLightState();
                }
                if (oppositeLightState != newOppositeLightState) {
                    oppositeLightState = newOppositeLightState;
                    setOppositeLightState();
                }
            }
            else {
                int newLightState = 0;
                int newOppositeLightState = elapsedTime <= greenTime ? 2 : 1;
                if (lightState != newLightState) {
                    lightState = newLightState;
                    setLightState();
                }

                if (oppositeLightState != newOppositeLightState) {
                    oppositeLightState = newOppositeLightState;
                    setOppositeLightState();
                }
            }
        }
    }
	void toggleObstacles(){
		
		obstacle1.SetActive(!obstacle1.activeSelf);
		obstacle2.SetActive(!obstacle2.activeSelf);
		obstacle3.SetActive(!obstacle3.activeSelf);
		obstacle4.SetActive(!obstacle4.activeSelf);

	}

    private void setLightState() {

        if (lightState == 0) { //red light

            redLights[0].SetActive(true);
            redLights[1].SetActive(true);
            foreach (GameObject detector in detectors)
                detector.SetActive(true);
            yellowLights[0].SetActive(false);
            yellowLights[1].SetActive(false);
            greenLights[0].SetActive(false);
            greenLights[1].SetActive(false);
            toggleObstacles();

        }
        else
        if (lightState == 1) { //yellow light
            redLights[0].SetActive(false);
            redLights[1].SetActive(false);
            foreach (GameObject detector in detectors)
                detector.SetActive(false);
            yellowLights[0].SetActive(true);
            yellowLights[1].SetActive(true);
            greenLights[0].SetActive(false);
            greenLights[1].SetActive(false);
        }
        else
        if (lightState == 2) { //green light
            redLights[0].SetActive(false);
            redLights[1].SetActive(false);
            foreach (GameObject detector in detectors)
                detector.SetActive(false);
            yellowLights[0].SetActive(false);
            yellowLights[1].SetActive(false);
            greenLights[0].SetActive(true);
            greenLights[1].SetActive(true);
        }

    }

    private void setOppositeLightState() {

        if (oppositeLightState == 0) { //red light
            oppositeRedLights[0].SetActive(true);
            oppositeRedLights[1].SetActive(true);
            foreach (GameObject detector in oppositeDetectors)
                detector.SetActive(true);
            oppositeYellowLights[0].SetActive(false);
            oppositeYellowLights[1].SetActive(false);
            oppositeGreenLights[0].SetActive(false);
            oppositeGreenLights[1].SetActive(false);
            toggleObstacles();

        }
        else
        if (oppositeLightState == 1) { //yellow light
            oppositeRedLights[0].SetActive(false);
            oppositeRedLights[1].SetActive(false);
            foreach (GameObject detector in oppositeDetectors)
                detector.SetActive(false);
            oppositeYellowLights[0].SetActive(true);
            oppositeYellowLights[1].SetActive(true);
            oppositeGreenLights[0].SetActive(false);
            oppositeGreenLights[1].SetActive(false);
        }
        else
        if (oppositeLightState == 2) { //green light
            oppositeRedLights[0].SetActive(false);
            oppositeRedLights[1].SetActive(false);
            foreach (GameObject detector in oppositeDetectors)
                detector.SetActive(false);
            oppositeYellowLights[0].SetActive(false);
            oppositeYellowLights[1].SetActive(false);
            oppositeGreenLights[0].SetActive(true);
            oppositeGreenLights[1].SetActive(true);
        }
    }
}
