using UnityEngine;

public class Wall : MonoBehaviour
{
    private BoxCollider boxCollider;

    private void Start()
    {

        boxCollider = GetComponent<BoxCollider>();

    }
    private void OnTriggerExit(Collider other)
    {

        boxCollider.isTrigger=false;


    }
}
