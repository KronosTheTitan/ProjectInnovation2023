using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetHealth(int pCurrentHealth, int maxHp)
    {
        float newHealth = (float)pCurrentHealth / (float)maxHp * 100.0f;
        if (slider != null)
            slider.value = (int)newHealth;
    }

    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
    }
}
