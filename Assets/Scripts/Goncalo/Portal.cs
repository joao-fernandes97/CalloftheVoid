using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private PlanetsEnum planet;

    /// <summary>
    /// A method to iterate through the planes of each planet, activating only the intended portal
    /// Disables all other portals
    /// </summary>
    /// <param name="planet"></param>
    public void ChangePlanet(PlanetsEnum planet)
    {
        this.planet = planet;

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
