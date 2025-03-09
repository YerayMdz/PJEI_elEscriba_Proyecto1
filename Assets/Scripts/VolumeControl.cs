using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer; // Asigna el AudioMixer en el Inspector
    public Slider volumeSlider; // Asigna el Slider en el Inspector

    void Start()
    {
        if (volumeSlider != null)
        {
            float volume;
            audioMixer.GetFloat("Volume", out volume); // Obtener volumen actual
            volumeSlider.value = volume; // Sincronizar el Slider con el AudioMixer
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
}
