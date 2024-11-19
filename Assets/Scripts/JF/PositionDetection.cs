using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PositionDetection : MonoBehaviour
{
    [SerializeField]
    private TMP_Text PortalCode;
    [SerializeField]
    private Collider Mars;
    [SerializeField]
    private Collider Jupiter;
    [SerializeField]
    private Collider Saturn;
    [SerializeField]
    private Collider Neptune;
    private Portal_NoWalls portal;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        portal = FindAnyObjectByType<Portal_NoWalls>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Triggered");
        if(collider == Mars)
        {
            portal.ChangePlanet(PlanetsEnum.Mars);
            PortalCode.text="STAR - MARS";
        }else if(collider == Jupiter)
        {
            portal.ChangePlanet(PlanetsEnum.Jupiter);
            PortalCode.text="STAR - JPTR";
        }else if(collider == Saturn)
        {
            portal.ChangePlanet(PlanetsEnum.Saturn);
            PortalCode.text="STAR - STRN";
        }else if(collider == Neptune)
        {
            portal.ChangePlanet(PlanetsEnum.Neptune);
            PortalCode.text="STAR - NEPT";
        }else
        {
            Debug.Log("Unknown Object");
        }    
    }
}

