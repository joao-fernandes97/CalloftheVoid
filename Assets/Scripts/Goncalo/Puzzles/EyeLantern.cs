using UnityEngine;

public class EyeLantern : MonoBehaviour
{
    [SerializeField]
    private PlanetsEnum necessaryEnergy;
    [SerializeField]
    private LayerMask interactableMask;
    [SerializeField]
    private GameObject planetCollectible;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("detected something");
        //Checks for the lantern
        if (other.GetComponent<Lantern>() != null)
        {
            Lantern lantern = other.GetComponent<Lantern>();
            Debug.Log("detected lantern");
            //If the lantern has Neptune's energy, the eye dies
            if (lantern.Planet == necessaryEnergy && lantern.emitingEnergy)
            {
                Debug.Log("detected neptune");
                planetCollectible.layer = (int)Mathf.Log(interactableMask.value, 2);
                Destroy(gameObject);
            }
        }
    }
}
