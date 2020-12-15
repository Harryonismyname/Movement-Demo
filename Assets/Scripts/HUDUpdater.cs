using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpdater : MonoBehaviour
{
    [SerializeField] private Text movementType;
    [SerializeField] private Text cameraType;

    public void DisplayMovementType(string type)
    {
        movementType.text = "Current Movement Style: " + type;
    }
    public void DisplayCameraType(string type)
    {
        cameraType.text = "Current Camera Style: "+type;
    }
}
