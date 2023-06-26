using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private void Awake()
    {
        originalPosition = this.transform.position;
        originalRotation = this.transform.rotation;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundaries"))
        {
            resetTransform();
        }
    }

    public void resetTransform()
    {
        this.transform.position = originalPosition;
        this.transform.rotation = originalRotation;
    }
}
