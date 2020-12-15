using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private PauseMenuController pauseMenuController;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private HUDUpdater hUDUpdater;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pauseMenuController = GameObject.Find("PauseMenuController").GetComponent<PauseMenuController>();
        hUDUpdater = GameObject.Find("HUDUpdater").GetComponent<HUDUpdater>();
    }
    private void Update()
    {
        hUDUpdater.DisplayCameraType(gameManager.cameraType);
        hUDUpdater.DisplayMovementType(gameManager.movementType);
        if (gameManager.isPaused)
        {
            pauseMenuController.ActivateMenu();
        }
        else if (!gameManager.isPaused)
        {
            pauseMenuController.DeactivateMenu();   
        }
    }
}
