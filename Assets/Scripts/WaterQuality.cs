using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class WaterQuality : MonoBehaviour
{
    [SerializeField] private Material lowQualityWater;
    [SerializeField] private Material highQualityWater;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        int qualityIndex = QualitySettings.GetQualityLevel();

        SwitchWaterMaterial(qualityIndex);
    }

    private void OnEnable()
    {
        EventsHolder.OnSetQualityLevel += SwitchWaterMaterial;
    }

    private void OnDisable()
    {
        EventsHolder.OnSetQualityLevel -= SwitchWaterMaterial;
    }

    private void SwitchWaterMaterial(int value)
    {
       _renderer.material = value == 0 ? lowQualityWater : highQualityWater;
    }
}