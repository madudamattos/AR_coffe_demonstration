using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreCollision : MonoBehaviour
{
    [SerializeField] Collider collider1;
    [SerializeField] Collider collider2;
    private Collider col;

    [SerializeField] Rigidbody rb1;
    [SerializeField] Rigidbody rb2;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        col = this.GetComponent<MeshCollider>();
        rb = this.GetComponent<Rigidbody>();

        Physics.IgnoreCollision(col, collider2);
        Physics.IgnoreCollision(col, collider1);
        Physics.IgnoreCollision(collider2, collider1);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.isKinematic)
        {
            rb2.isKinematic = true;
            rb1.isKinematic = true;
        } else
        {
            rb2.isKinematic = false;
            rb1.isKinematic = false;
        }
    }
}
