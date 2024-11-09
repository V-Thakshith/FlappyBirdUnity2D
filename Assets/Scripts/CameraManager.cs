using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera followCam;
    [SerializeField] private Bird bird;
    [SerializeField] private CinemachineVirtualCamera idleCam;

    private void Awake()
    {
        transform.position = new Vector3(bird.transform.position.x, bird.transform.position.y, bird.transform.position.z);
        SwitchFollowCamera();
    }

    private void Update()
    {
        transform.position = new Vector3(bird.transform.position.x, transform.position.y, transform.position.z);
    }

    public void SwitchFollowCamera()
    {
        followCam.Follow = transform;
        followCam.enabled = true;
        idleCam.enabled = false;
    }

    public void SwitchToIdleCamera()
    {
        idleCam.enabled = true;
        followCam.enabled = false;
    }
}
