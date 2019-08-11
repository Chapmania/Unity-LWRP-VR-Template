using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class XRInputLeftHandTest : MonoBehaviour
{
    public Renderer myRenderer;
    InputDevice leftHand;

    // Start is called before the first frame update
    void Start()
    {
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
        Vector3 tempVector;
        Quaternion tempQuaternion;
        float myTrigger;
        float myGrip;

        // Grab value, quick and dirty, no error checking
        leftHand.TryGetFeatureValue(CommonUsages.devicePosition, out tempVector);
        leftHand.TryGetFeatureValue(CommonUsages.deviceRotation, out tempQuaternion);
        leftHand.TryGetFeatureValue(CommonUsages.trigger, out myTrigger);
        leftHand.TryGetFeatureValue(CommonUsages.grip, out myGrip);

        gameObject.transform.localPosition = tempVector;
        gameObject.transform.localRotation = tempQuaternion;

        // Change cube colour on trigger and trigger grip values
        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", new Color(myTrigger, myGrip, 1));
        myRenderer.SetPropertyBlock(block);
    }
}