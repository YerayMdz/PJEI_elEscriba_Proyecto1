using UnityEngine;
using UnityEngine.UI;

public class BrightnessSlider : MonoBehaviour
{
    public Light mainLight; // Asigna la luz direccional en el Inspector
    public Slider brightnessSlider; // Asigna el Slider en el Inspector

    void Start()
    {
        if (brightnessSlider != null && mainLight != null)
        {
            brightnessSlider.minValue = 0.2f; // Valor mínimo de brillo
            brightnessSlider.maxValue = 3f; // Valor máximo de brillo
            brightnessSlider.value = mainLight.intensity; // Sincronizar con la luz actual

            brightnessSlider.onValueChanged.AddListener(AdjustBrightness); // Agregar evento
        }
    }

    public void AdjustBrightness(float value)
    {
        if (mainLight != null)
        {
            mainLight.intensity = value;
        }
    }
}
