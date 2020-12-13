using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string movementType;
    private string cameraType;
    private GameObject UI;
    private UIHandler uiHandler;
    public bool isPaused;
    private void Start()
    {
        UI = GameObject.Find("UIHandler");
        uiHandler = UI.GetComponent<UIHandler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            AudioListener.pause = true;
        }
        else if (!isPaused)
        {
            Time.timeScale = 1;
            AudioListener.pause = false;
        }
    }
    public void SetMovementType(string moveType)
    {
        movementType = moveType;
    }
}
