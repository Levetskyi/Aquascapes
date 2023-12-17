using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    private TextMeshProUGUI _fps;
    private void Start()
    {
        _fps = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _fps.text = Mathf.Round((1f / Time.deltaTime)).ToString();
    }
}
