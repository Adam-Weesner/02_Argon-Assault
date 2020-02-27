using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Header("X")]
    [Tooltip("In ms^-1")] [SerializeField] private float xSpeed = 18.0f;
    [SerializeField] private float maxX = 11.5f;
    [SerializeField] private float minX = -11.5f;

    [Header("Y")]
    [Tooltip("In ms^-1")] [SerializeField] private float ySpeed = 18.0f;
    [SerializeField] private float maxY = 6.5f;
    [SerializeField] private float minY = -6.5f;

    [Header("Factors")]
    [SerializeField] float posPitchFactor = -3.0f;
    [SerializeField] float posYawFactor = 3.5f;
    [SerializeField] float controlPitchFactor = -20.0f;
    [SerializeField] float controlRollFactor = -20.0f;

    private float xThrow = 0.0f;
    private float yThrow = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessLocation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float pitchDueToPos = transform.localPosition.y * posPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPos + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * posYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessLocation()
    {
        // Left/right value
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        float newXPos = Mathf.Clamp(rawNewXPos, minX, maxX);

        // Up/Down value
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;
        float newYPos = Mathf.Clamp(rawNewYPos, minY, maxY);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }
}
