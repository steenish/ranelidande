using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject projectile;
    public Transform leftController;
    public Transform rightController;
    public float projectileForce;

    private float prevTriggerLeft = 0.0f;
    private float prevTriggerRight = 0.0f;

    private void Update() {
        float inputGripLeft = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
        float inputGripRight = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
        float inputTriggerLeft = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float inputTriggerRight = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        if (inputGripLeft > 0 && prevTriggerLeft == 0.0f && inputTriggerLeft > 0) {
            //Vector3 leftPosition = trackingSpace.TransformPoint(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch));
            //Vector3 leftForward = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch) * Vector3.forward;
            FireProjectile(leftController.position, leftController.forward);
        }

        if (inputGripRight > 0 && prevTriggerRight == 0.0f && inputTriggerRight > 0) {
            //Vector3 rightPosition = trackingSpace.TransformPoint(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch));
            //Vector3 rightForward = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward;
            FireProjectile(rightController.position, rightController.forward);
        }

        prevTriggerLeft = inputTriggerLeft;
        prevTriggerRight = inputTriggerRight;
    }

    private void FireProjectile(Vector3 position, Vector3 forward) {
        GameObject projectileInstance = Instantiate(projectile, position, Quaternion.identity);
        Rigidbody rb = projectileInstance.GetComponent<Rigidbody>();
        rb.AddForce(forward.normalized * projectileForce, ForceMode.Force);
    }
}
