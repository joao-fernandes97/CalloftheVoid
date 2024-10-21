using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField]
    private PlanetsEnum planetName;

    public PlanetsEnum PlanetName {  get { return planetName; } }
}
