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
    private Portal portal;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        portal = FindAnyObjectByType<Portal>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Triggered");
        if(collider == Mars)
        {
            portal.ChangePlanet(PlanetsEnum.Mars);
            PortalCode.text="star - mars";
        }else if(collider == Jupiter)
        {
            portal.ChangePlanet(PlanetsEnum.Jupiter);
            PortalCode.text="star - jptr";
        }else if(collider == Saturn)
        {
            portal.ChangePlanet(PlanetsEnum.Saturn);
            PortalCode.text="star - strn";
        }else if(collider == Neptune)
        {
            portal.ChangePlanet(PlanetsEnum.Neptune);
            PortalCode.text="star - nept";
        }else
        {
            Debug.Log("Unknown Object");
        }    
    }
}

