using System.Collections.Generic;
using UnityEngine;

public class NewMapElementsTrigger : MonoBehaviour
{
    public GameObject SolPref;
    public List<GameObject> trucs;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(SolPref, new Vector3(0, 0, GetComponentInParent<Transform>().position.z -40), transform.rotation);
        }
    }
}
