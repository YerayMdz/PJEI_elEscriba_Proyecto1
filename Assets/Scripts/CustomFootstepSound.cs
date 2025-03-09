using UnityEngine;

public class CustomFootstepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;

    public float walkInterval = 0.4f;
    public float runInterval = 0.3f;
    public float movementThreshold = 0.1f; // Evita sonidos al estar casi parado

    private Rigidbody rb;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (audioSource == null)
        {
            Debug.LogError("Falta AudioSource.");
        }
    }

    void Update()
    {
        float speed = rb.velocity.magnitude;

        if (speed > movementThreshold)
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                PlayFootstep();
                timer = Input.GetKey(KeyCode.LeftShift) ? runInterval : walkInterval;
            }
        }
        else
        {
            timer = 0f; // Reset si estás quieto
        }
    }

    void PlayFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            int index = Random.Range(0, footstepSounds.Length);
            audioSource.PlayOneShot(footstepSounds[index]);
        }
    }
}
