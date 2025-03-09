using UnityEngine;

public class AmbientSoundTrigger : MonoBehaviour
{
    private AudioSource ambientSound;

    void Start()
    {
        ambientSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ambientSound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ambientSound.Stop();
        }
    }
}
