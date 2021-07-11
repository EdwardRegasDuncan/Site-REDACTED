using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecontainmentSquadController : MonoBehaviour
{
    [SerializeField] GameObject securityCameraController;
    [SerializeField] GameObject scpPositions;

    bool containmentSquadOnCooldown = false;

    enum ButtonState
    {
        Active,
        OnCooldown
    }

    ButtonState recontainmentButton;

    private void SendRecontainmentSquad()
    {
        GameObject activeCamera = securityCameraController.GetComponent<SecurityCameraController>().activeCamera;
        GameObject[] scpAtLocation = GetSCPAtLocation(activeCamera.name);
        if (scpAtLocation.Length > 0)
        {
            
        }
        else
        {
            Debug.Log("No SCPs at selected location");
        }
    }


    private GameObject[] GetSCPAtLocation(string locationName)
    {
        GameObject location = scpPositions.transform.Find(locationName).gameObject;
        return location.GetComponent<PositionRelation>().SCPsAtLocation.ToArray();
    }

}
