using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigHandler : MonoBehaviour
{
    
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject thirdPersonRig;
    [SerializeField] private GameObject firstPersonRig;
    [SerializeField] private GameObject topDownRig;
    [SerializeField] private GameObject orthographicRig;
    private GameObject activeCamera;
    private string activeCamName;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        activeCamera = thirdPersonRig;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isPaused)
        {   
            if(gameManager.cameraType != activeCamName)
            {
                ToggleCamera(activeCamera);
            }
        }
    }
    private void ToggleCamera(GameObject cam)
    {
        cam.SetActive(false);
        if (gameManager.cameraType == "Third-Person")
        {
            SetCam(thirdPersonRig);
        }
        if(gameManager.cameraType == "First-Person")
        {
            SetCam(firstPersonRig);
        }
        if(gameManager.cameraType == "Top-Down")
        {
            SetCam(topDownRig);
        }
        if(gameManager.cameraType == "Orthographic")
        {
            SetCam(orthographicRig);
        }
        
    }
    private void SetCam(GameObject cam)
    {
        activeCamera = cam;
        activeCamera.SetActive(true);
        activeCamName = gameManager.cameraType;
    }
}