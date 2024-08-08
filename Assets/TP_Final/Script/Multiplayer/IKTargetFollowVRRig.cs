﻿using Unity.Netcode;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform ikTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class IKTargetFollowVRRig : NetworkBehaviour
{
    [Range(0, 1)]
    public float turnSmoothness = 0.1f;
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;

    public Vector3 headBodyPositionOffset;
    public float headBodyYawOffset;

    // Update is called once per frame
    void LateUpdate()
    {
        if (IsOwner)
        {
            head.Map();
            leftHand.Map();
            rightHand.Map();

            // Rotate the body based on the camera's rotation
            float headYaw = head.vrTarget.eulerAngles.y + headBodyYawOffset;
            Quaternion bodyRotation = Quaternion.Euler(0f, headYaw, 0f);

            // Apply the body rotation along with the position offset
            transform.position = head.ikTarget.position + bodyRotation * headBodyPositionOffset;

            // Smoothly interpolate the body rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, bodyRotation, turnSmoothness);
        }
    }
}