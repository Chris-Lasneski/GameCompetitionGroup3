using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //public Transform player;
    //public float pLerp = 0.02f;
    //public float rLerp = 0.01f;

    public GameObject player;
    public GameObject cameraPivot;
    public float yOffset = 1f;
    public float sensitivity = 10f;
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

    private bool _enabled = true;
    private bool paused = false;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _enabled = !_enabled;
            paused = !paused;
            Cursor.visible = paused;
        }
        if (_enabled)
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

            //transform.position = Vector3.Lerp(transform.position, player.position, pLerp);
            //transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, pLerp);
        }
    }
}
