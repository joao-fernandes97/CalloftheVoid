using NaughtyAttributes;
using System;
using UnityEngine;

public class TriggerEyeArea : MonoBehaviour
{

    private PlayerMovement _playerMov = null;
    [SerializeField]private EyeHandler[] _eyeEnteties;
    private void Start()
    {
        if (_eyeEnteties.Length == 0)
            Debug.LogWarning("No eyes inserted into \"TriggerEyeArea\" Script Array");
    }

    private void OnTriggerEnter(Collider other)
    {
        _playerMov = other.gameObject.GetComponent<PlayerMovement>();
        if (_playerMov != null)
        {            
            EnableEyes();
            Debug.Log("Player entered Eye perimeter");
        }
        


    }
    private void OnTriggerExit(Collider other)
    {
        if (_playerMov != null)
        {
            _playerMov = null;
            DisableEyes();
            Debug.Log("Player exited Eye perimeter");
        }
        

    }
    private void EnableEyes()
    {
        if (_eyeEnteties.Length != 0)
            foreach (EyeHandler eye in _eyeEnteties)
                eye.EnableEye();
    }
    private void DisableEyes()
    {
        if (_eyeEnteties.Length != 0)
            foreach (EyeHandler eye in _eyeEnteties)
                eye.DisableEye();

    }
    


}
