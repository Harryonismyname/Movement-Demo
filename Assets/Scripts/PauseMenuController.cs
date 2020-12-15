using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    public void ActivateMenu()
    {
        pauseMenuUI.SetActive(true);
    }
    public void DeactivateMenu()
    {
        pauseMenuUI.SetActive(false);
    }
}
