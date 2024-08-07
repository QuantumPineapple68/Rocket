using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private AudioSource au;
    private float thrust = 150f;
    private float roatationSpeed = 200f;
    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private ParticleSystem mainThrust;
    [SerializeField] private ParticleSystem rightThrust;
    [SerializeField] private ParticleSystem leftThrust;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        au = GetComponent<AudioSource>();
    }
    
    void Update()
    {
            ProcessMove();
    }
    
    void ProcessMove()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * (thrust * Time.deltaTime));
            if (!au.isPlaying)
            {
                au.PlayOneShot(mainEngine);
                mainThrust.Play();
            }
        }
        else
        {
            au.Stop();
            mainThrust.Stop();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * (roatationSpeed * Time.deltaTime));
            if (!rightThrust.isPlaying)
            {
                leftThrust.Play();
            }
            rb.freezeRotation = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.back * (roatationSpeed * Time.deltaTime));
            if (!rightThrust.isPlaying)
            {
                rightThrust.Play();
            }
            rb.freezeRotation = false;
        }
        else
        {
            leftThrust.Stop();
            rightThrust.Stop();
        }
    }
    
}
