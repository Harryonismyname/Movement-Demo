using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;
    public Transform cameraTrack;
    public Vector3 offset;
    // Update is called once per frame
    void Update()
    {
        cameraTrack.position = player.position +new Vector3(0,3,0);
        //transform.localPosition = cameraTrack.localPosition;
        cameraTrack.rotation = player.rotation;
    }
}
