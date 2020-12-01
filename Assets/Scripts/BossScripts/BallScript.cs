using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {
    GameObject parent;
    Rigidbody rb;
    Collider col;
    public float force = 5;

    bool isReleased = false;
	// Use this for initialization
	void Start () {

        rb = this.GetComponent<Rigidbody>();
        col = this.GetComponent<Collider>();
        rb.useGravity = false;
        rb.isKinematic = true;
        col.enabled = false;
        parent = GameObject.Find("ThrowPlaceholder");
    }

    public void ReleaseMe(Vector3 target)
    {
        transform.parent = null;
        rb.useGravity = true;
        rb.isKinematic = false;
        col.enabled = true;
        isReleased = true;
        transform.rotation = parent.transform.rotation;

        Vector3 targetPosition = target - transform.position;

        //add to y, to throw higher
        targetPosition.y += 5f;

        rb.AddForce((targetPosition) * force);
    }

    public void DisableBallScript()
    {
        GetComponent<BallScript>().enabled = false;
    }
}
