using UnityEngine;

public class PlanetCollectibles : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        //Temporary while not implemented full functionality
        Destroy(gameObject);
    }
}
