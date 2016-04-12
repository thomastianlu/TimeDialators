using UnityEngine;
using System.Collections;

public class ActivateCameraShake : MonoBehaviour {

    [SerializeField]
    private CameraShake _mainCamera;

    public void PlayCameraShake()
    {
        _mainCamera.PlayAnimation();
    }
}
