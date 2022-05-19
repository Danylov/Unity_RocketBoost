using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrust = 1000.0f;
    [SerializeField] private float rotationThrust = 100.0f;
    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private ParticleSystem MainBooster;
    [SerializeField] private ParticleSystem LeftBooster;
    [SerializeField] private ParticleSystem RightBooster;
    private Rigidbody rb;
    private AudioSource audioSource;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space)) StartThrusting();
        else StopThrusting();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))  RotateLeft();
        else if (Input.GetKey(KeyCode.D)) RotateRight();
        else StopRotating();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying) audioSource.PlayOneShot(mainEngine);
        if (!MainBooster.isPlaying) MainBooster.Play();
    }
    
    private void StopThrusting()
    {
        audioSource.Stop();
        MainBooster.Stop();
    }
   
    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!RightBooster.isPlaying) RightBooster.Play();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!LeftBooster.isPlaying) LeftBooster.Play();
    }
 
    private void StopRotating()
    {
        RightBooster.Stop();
        LeftBooster.Stop();
    }
    
    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}