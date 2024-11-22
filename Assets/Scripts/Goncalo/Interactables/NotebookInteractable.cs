using UnityEngine;

public class NotebookInteractable : MonoBehaviour, IInteractable, IUsable
{
    
    [SerializeField]
    private GameObject notebookSlot;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField] private StopAndGoPuzzle _game = null;

    [SerializeField]
    private int itemNumber;
    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = FindAnyObjectByType<PlayerInventory>();
    }

    public void Interact()
    {
        //Puts the notebook on the player's inventory
        transform.parent = notebookSlot.transform;

        //Reseting the notebook's local position and rotation to make sure it always looks the same
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        transform.gameObject.layer = layerMask;
        playerInventory.NewItem(itemNumber);
        GameWin();
        
    }
    private void GameWin()
    {
        if (_game != null)
        {
            _game.GameToggle();
            Debug.Log("YOU WIN");

        } 

    }
    public void Use()
    {
        Debug.Log("Notebook used");
    }
}
