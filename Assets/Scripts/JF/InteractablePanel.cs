using Unity.Cinemachine;
using UnityEngine;

public class InteractablePanel : MonoBehaviour,IInteractable
{
    [SerializeField]
    private LayerMask defaultMask;
    [SerializeField]
    private LayerMask interactableMask;
    [SerializeField]
    private CinemachineCamera UICamera;
    [SerializeField]
    private int cameraPriority = 11;
    [SerializeField]
    private int cameraPriorityReset = 1;
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private PlayerInteraction playerInteraction;
    private OrreryControl orreryControl;

    private void Start()
    {
        orreryControl = GetComponent<OrreryControl>();
    }

    public void Interact()
    {
        Debug.Log("Interacted");
        UICamera.Priority = cameraPriority;
        Cursor.lockState = CursorLockMode.Confined;
        gameObject.layer = (int)Mathf.Log(defaultMask.value, 2);
        playerMovement.enabled = false;
        //playerInteraction.enabled = false;
        orreryControl.StartInteraction();
    }

    public void ExitInteraction()
    {
        gameObject.layer = (int)Mathf.Log(interactableMask.value, 2);
        playerMovement.enabled = true;
        //playerInteraction.enabled = true;
        UICamera.Priority = cameraPriorityReset;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
