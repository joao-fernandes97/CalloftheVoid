using UnityEngine;

public class PlanetInsertion : MonoBehaviour
{
    [SerializeField]
    private OrreryControl orreryControl;

    public void InsertPlanet(PlanetsEnum planet)
    {
        orreryControl.InsertPlanet(planet);
    }
}
