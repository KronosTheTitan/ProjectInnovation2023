using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject healthbar;

    public void SetHealth(int pCurrentHealth, int maxHp)
    {
        float newHealth = (float)pCurrentHealth / (float)maxHp * 100.0f;
        if (slider != null)
            slider.value = (int)newHealth;
    }

    private void Update()
    {
        if(healthbar != null)
            healthbar.transform.LookAt(healthbar.transform.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
    }
}
