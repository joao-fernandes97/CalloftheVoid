using UnityEngine;
using UnityEngine.UIElements;

public class CameraCopy : MonoBehaviour
{
    [SerializeField] private Transform _playerCamera;
    [SerializeField] private Transform _marsCamera;
    [SerializeField] private float _cameraYOffset = -20f;
    
    [SerializeField] private bool _follow = false;
    
    void FixedUpdate()
    {
        if (_follow)
        {
            _marsCamera.SetPositionAndRotation(new Vector3(_playerCamera.position.x,
                (_playerCamera.position.y + _cameraYOffset),
                _playerCamera.position.z), _playerCamera.rotation);

        }
    }
}
