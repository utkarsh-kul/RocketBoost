using UnityEngine;
using UnityEngine.InputSystem;
public class Movement : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;

    [Header("Component References")]
    Rigidbody rb;
    private AudioSource audioSource;
    [SerializeField] private AudioClip sfxThrust;
    [SerializeField] private ParticleSystem vfxThrust;
    [SerializeField] private ParticleSystem vfxRotationLeft;
    [SerializeField] private ParticleSystem vfxRotationRight;

    [Header("Movement Settings")]
    [SerializeField] private float thrustForce;
    [SerializeField] private float rotationForce;
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        ProcessThrust();
    }
    private void Update()
    {
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float rotationValue = rotation.ReadValue<float>();
        if (rotationValue != 0)
        {
            rb.freezeRotation = true;
            transform.Rotate(rotationValue * Vector3.forward * rotationForce * Time.deltaTime);
            rb.freezeRotation = false;
        }
        else
        {
            vfxRotationLeft.Stop();
            vfxRotationRight.Stop();
        }
        if (rotationValue == -1)
        {
            vfxRotationLeft.Play();

        }
        else if (rotationValue == 1)
        {
            vfxRotationRight.Play();
        }
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            vfxThrust.Play();
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(sfxThrust);
            }
            Debug.Log("Thrusting");
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
        }
        else
        {
            vfxThrust.Stop();
            audioSource.Stop();
        }
    }
}
