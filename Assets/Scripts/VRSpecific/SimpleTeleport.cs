using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SimpleTeleport : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera _camera;

    InputDevice leftHand;
    private float update;

    void Start()
    {
        _camera = Camera.main;

        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        var allLeftHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithRole(InputDeviceRole.LeftHanded, allLeftHandDevices);

        // Assuming only one device with left handed role = no error checking
        leftHand = allLeftHandDevices[0];
    }

    // Update is called once per frame
    void Update()
    {
        float myTrigger;

        // Grab value, quick and dirty, no error checking
        leftHand.TryGetFeatureValue(CommonUsages.trigger, out myTrigger);

        update += Time.deltaTime;
        if (update > 1.0f)
        {
            update = 0.0f;

            if (myTrigger > 0.1)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        // shoot a raycast from the center of our screen
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit; // output variable to get what we collided against
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform != null)
            {
                // set our location to the point we hit
                Vector3 newLocation = new Vector3(hit.point.x, 1, hit.point.z);
                transform.position = newLocation;
            }
        }
    }
}
