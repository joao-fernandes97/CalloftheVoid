using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PositionDetection : MonoBehaviour
{
    [SerializeField]
    private GameObject finalMachine;
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
    [SerializeField]
    private Portal portal;
    private List<Collider> colliderList = new List<Collider>(); 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        portal = FindAnyObjectByType<Portal>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Triggered");
        colliderList.Add(collider);
        
        if (colliderList.Contains(Saturn) && colliderList.Contains(Jupiter) && colliderList.Count == 2)
        {
            portal.ChangePlanet(PlanetsEnum.Eclipse);
            PortalCode.text="DARK SUN";
        }
        else if(collider == Mars)
        {
            portal.ChangePlanet(PlanetsEnum.Mars);
            PortalCode.text="XAD - XIQ";
        }else if(collider == Jupiter)
        {
            portal.ChangePlanet(PlanetsEnum.Jupiter);
            PortalCode.text="XAD - QXD";
        }else if(collider == Saturn)
        {
            portal.ChangePlanet(PlanetsEnum.Saturn);
            PortalCode.text="XAD - AIA";
        }else if(collider == Neptune)
        {
            portal.ChangePlanet(PlanetsEnum.Neptune);
            finalMachine.SetActive(true);
            PortalCode.text="XAD - QDI";
        }else
        {
            Debug.Log("Unknown Object");
        }    
    }

    private void OnTriggerExit(Collider collider)
    {
        colliderList.Remove(collider);
    }
}

