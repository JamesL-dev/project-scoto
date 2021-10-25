using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject m_camera;
    private Slider m_slider;
    private BaseEnemy m_enemy;
    private void Start()
    {
        m_camera = GameObject.Find("Main Camera");
        m_slider = GameObject.Find("_HealthBar").GetComponent<Slider>();
        m_enemy = gameObject.GetComponentInParent<BaseEnemy>();
        transform.Find("HealthBarCanvas").GetComponent<Canvas>().worldCamera = m_camera.GetComponent<Camera>();

    }

    // Update is called once per frame
    private void Update()
    {
        transform.rotation = m_camera.transform.rotation;
        m_slider.value = m_enemy.GetHealthPercent();
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
