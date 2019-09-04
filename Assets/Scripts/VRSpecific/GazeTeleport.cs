using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GazeTeleport : MonoBehaviour
{
    Camera _camera;

    InputDevice leftHand;

    float myTrigger;
    bool canITeleport = true;

    public GameObject GazeCanvas;

    void Start()
    {
        // Enable GazeCanvas to see 'dot' for teleport location
        GazeCanvas.SetActive(true);

        _camera = Camera.main;

        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        var allLeftHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithRole(InputDeviceRole.LeftHanded, allLeftHandDevices);

        // Assuming only one device with left handed role = no error checking
        leftHand = allLeftHandDevices[0];
    }

    void Update()
    {
        // Grab value, quick and dirty, no error checking
        leftHand.TryGetFeatureValue(CommonUsages.trigger, out myTrigger);

        if (myTrigger > 0.01)
        {
            HandleGazeTeleport();
        }
    }

    private void HandleGazeTeleport()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform != null && canITeleport == true)
            {
                Vector3 newLocation = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                transform.position = newLocation;

                // Stop teleporting for the next 1/2 second
                canITeleport = false;
                Invoke("ResetCanITeleport", 0.5f);
            }
        }
    }

    private void ResetCanITeleport()
    {
        canITeleport = true;
    }
}
