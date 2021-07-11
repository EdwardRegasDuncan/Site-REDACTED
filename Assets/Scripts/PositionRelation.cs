using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRelation : MonoBehaviour
{
    [SerializeField] GameObject[] _connectedPositions;
    public GameObject _directionToPlayer;
    public List<GameObject> SCPsAtLocation;

    public GameObject determineNextPosition()
    {
        if(_connectedPositions.Length == 1)
        {
            return _connectedPositions[0];
        }
        else
        {
            int nextPosValue = (int)Random.Range(0, (_connectedPositions.Length));
            GameObject nextPos = _connectedPositions[nextPosValue];
            return nextPos;
        }
    }

    public void AddSCPToThisLocation(GameObject scp)
    {
        SCPsAtLocation.Add(scp);
    }
    public void RemoveSCPFromThisLocation(GameObject scp)
    {
        SCPsAtLocation.Remove(scp);
    }

}
