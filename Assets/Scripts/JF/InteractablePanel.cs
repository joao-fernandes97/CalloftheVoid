using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class InteractablePanel : MonoBehaviour,IInteractable
{
    [SerializeField]
    private CinemachineCamera UICamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Interact()
    {
        Debug.Log("Interacted");
        UICamera.Priority = 11;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
