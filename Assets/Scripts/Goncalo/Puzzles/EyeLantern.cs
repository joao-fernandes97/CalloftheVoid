using UnityEngine;

public class EyeLantern : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("detected something");
        //Checks for the lantern
        if (other.GetComponent<Lantern>() != null)
        {
            Debug.Log("detected lantern");
            //If the lantern has Neptune's energy, the eye dies
            if (other.GetComponent<Lantern>().Planet == PlanetsEnum.Neptune)
            {
                Debug.Log("detected neptune");
                Destroy(gameObject);
            }
        }
    }
}
