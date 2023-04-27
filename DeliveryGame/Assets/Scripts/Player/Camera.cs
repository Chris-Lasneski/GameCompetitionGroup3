using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraPivot;
    public float yOffset = 1f;
    public float sensitivity = 0f;
    public Camera cam;

    public float scrollSens = 5f;
    public float scrollDamp = 6f;

    public float zoomMin = 3f;
    public float zoomMax = 25f;
    public float zoomDefault = 10f;
    public float zoomDistance;

    public float collisionSensitivity = 4.5f;

    private RaycastHit _camHit;
    private Vector3 _camDist;

    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        _camDist = cam.transform.localPosition;
        zoomDistance = zoomDefault;
        _camDist.z = zoomDistance;

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        sensitivity = GameObject.FindWithTag("Player").GetComponent<SettingsUI>().sensitivityValue;
        paused = GameObject.FindWithTag("Player").GetComponent<MenuUI>().paused;

        if (paused)
        {
            Cursor.visible = paused;
        }
        else
        {
            cameraPivot.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, player.transform.position.z);

            var rotation = Quaternion.Euler(Mathf.Clamp(cameraPivot.transform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity / 2, 0, 80),
                cameraPivot.transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * sensitivity,
                cameraPivot.transform.rotation.eulerAngles.z);

            cameraPivot.transform.rotation = rotation;

            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                var scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSens;
                scrollAmount *= zoomDistance * 0.3f;
                zoomDistance += -scrollAmount;
                zoomDistance = Mathf.Clamp(zoomDistance, zoomMin, zoomMax);
            }

            if (_camDist.z != -zoomDistance)
            {
                _camDist.z = Mathf.Lerp(_camDist.z, -zoomDistance, Time.deltaTime * scrollDamp);
            }

            cam.transform.localPosition = _camDist;

            GameObject obj = new GameObject();
            obj.transform.SetParent(cam.transform.parent);
            obj.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, cam.transform.localPosition.z - collisionSensitivity);

            if (Physics.Linecast(cameraPivot.transform.position, obj.transform.position, out _camHit))
            {
                cam.transform.position = _camHit.point;

                var localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y,
                    cam.transform.localPosition.z + collisionSensitivity);
                cam.transform.localPosition = localPosition;
            }
            Destroy(obj);

            if (cam.transform.localPosition.z > -1f)
            {
                cam.transform.localPosition = new Vector3(cam.transform.localPosition.x, cam.transform.localPosition.y, -1f);
            }
        }
    }
}
