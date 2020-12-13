using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if (gameManager.isPaused)
        {
            ActivateMenu();
        }
        else if(!gameManager.isPaused)
        {
            DeactivateMenu();
        }
    }
    void ActivateMenu()
    {
        pauseMenuUI.SetActive(true);
    }
    public void DeactivateMenu()
    {
        pauseMenuUI.SetActive(false);
    }
}
