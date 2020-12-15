using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string movementType;
    public string cameraType;
    private UIHandler uiHandler;
    public bool isPaused;
    private void Start()
    {
        uiHandler = GameObject.Find("UIHandler").GetComponent<UIHandler>();
        SetMovementType("Analogue");
        SetCameraType("Third-Person");
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        if (isPaused)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
            if (Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
        if (!isPaused)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
            if (Cursor.lockState != CursorLockMode.Locked && cameraType == "Third-Person" || cameraType == "First-Person")
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
    }
    public void SetMovementType(string moveType)
    {
        movementType = moveType;
    }
    public void SetCameraType(string camType)
    {
        cameraType = camType;
    }
}
