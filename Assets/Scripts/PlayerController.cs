using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(CollisionHandler))]
public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject[] guns;
    [SerializeField] private ParticleSystem particle_explosion;

    [Header("X")]
    [Tooltip("In ms^-1")] [SerializeField] private float xSpeed = 18.0f;
    [SerializeField] private float maxX = 11.5f;
    [SerializeField] private float minX = -11.5f;

    [Header("Y")]
    [Tooltip("In ms^-1")] [SerializeField] private float ySpeed = 18.0f;
    [SerializeField] private float maxY = 6.5f;
    [SerializeField] private float minY = -6.5f;

    [Header("Screen Position")]
    [SerializeField] float posPitchFactor = -3.0f;
    [SerializeField] float posYawFactor = 3.5f;

    [Header("Control Throw")]
    [SerializeField] float controlPitchFactor = -20.0f;
    [SerializeField] float controlRollFactor = -20.0f;

    [Header("Death")]
    public float timeUntilReset = 2f;

    private float xThrow = 0.0f;
    private float yThrow = 0.0f;

    [HideInInspector] public bool isAlive = true;


    private void Awake()
    {
        AddRequiredComponents();
    }

    private void AddRequiredComponents()
    {
        var rigidBody = gameObject.AddComponent<Rigidbody>();
        rigidBody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            ProcessLocation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            /*foreach (GameObject gun in guns)
            {
                gun.GetComponent<ParticleSystem>().Play();
            }*/
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
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

    // Called by CollisionHandler
    private void StartDeathSequence()
    {
        if (isAlive)
        {
            isAlive = false;

            SetGunsActive(false);

            Instantiate(particle_explosion, gameObject.transform);

            Invoke(nameof(ResetScene), timeUntilReset);
        } else
        {
            // TODO consider hit FX
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var emission = gun.GetComponent<ParticleSystem>().emission;
            emission.enabled = isActive;
        }
    }

    private void ResetScene()
    {
        SceneManagement.Instance.LoadScene();
    }
}
