using UnityEngine;

public class Portal_NoWalls : MonoBehaviour
{
    [SerializeField]
    private PlanetsEnum planet;

    private Pedestal pedestal;


    private void Start()
    {
        pedestal = FindAnyObjectByType<Pedestal>();

        if (pedestal == null)
        {
            Debug.Log("Portal couldn't find the pedestal");
        }
    }

    /// <summary>
    /// A method to iterate through the planes of each planet, activating only the intended portal
    /// Also sets the pedestal to the right planet
    /// Disables all other portals
    /// </summary>
    /// <param name="planet"></param>
    public void ChangePlanet(PlanetsEnum planet)
    {
        this.planet = planet;
        pedestal.Planet = planet;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            if (child.GetComponent<Planet>().PlanetName == planet)
            {
                child.SetActive(true);
            }
            else
            {
                child.SetActive(false);
            }
        }
    }
}
