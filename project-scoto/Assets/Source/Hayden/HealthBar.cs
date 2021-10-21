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
    void Start()
    {
        m_camera = GameObject.Find("Main Camera");
        m_slider = transform.Find("HealthBar").GetComponent<Slider>();
        m_enemy = gameObject.GetComponentInParent<BaseEnemy>();

        gameObject.GetComponent<Canvas>().worldCamera = m_camera.GetComponent<Camera>();
        Debug.Log("is this code getting ran?");

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = m_camera.transform.rotation;
        m_slider.value = m_enemy.GetHealthPercent();
    }
}
