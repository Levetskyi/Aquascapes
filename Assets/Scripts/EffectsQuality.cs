using UnityEngine;

public class EffectsQuality : MonoBehaviour
{
    [SerializeField] private GameObject effects;

    private void Awake()
    {
        int qualityIndex = QualitySettings.GetQualityLevel();

        SwitchEffectsVisibility(qualityIndex);
    }

    private void OnEnable()
    {
        EventsHolder.OnSetQualityLevel += SwitchEffectsVisibility;
    }

    private void OnDisable()
    {
        EventsHolder.OnSetQualityLevel -= SwitchEffectsVisibility;
    }

    private void SwitchEffectsVisibility(int qualityIndex)
    {
        effects.SetActive(qualityIndex == 2);
    }
}
