using Unity.Cinemachine;
using UnityEngine;

public class InteractablePanel : MonoBehaviour,IInteractable
{
    [SerializeField]
    private CinemachineCamera UICamera;

    public void Interact()
    {
        Debug.Log("Interacted");
        UICamera.Priority = 11;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
