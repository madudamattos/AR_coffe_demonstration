using UnityEngine;

public class SnapMug : MonoBehaviour
{
    public Transform refPoint;
    public float snapDistance = 10f;

    void Update()
    {
        //Snap();
    }

    void Snap()
    {
        float distanceToRef = Vector3.Distance(transform.position, refPoint.position);

        Debug.Log("snap");
        if (distanceToRef < snapDistance)
        {
            Debug.Log("snap2");
            transform.position = refPoint.position;
        }
    }
}
