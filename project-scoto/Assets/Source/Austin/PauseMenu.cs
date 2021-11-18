/*
 * Filename: PauseMenu.cs
 * Developer: Austin Kugler
 * Purpose:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public sealed class PauseMenu : BaseMenu
{
    public GameObject levelCounter;
    [SerializeField] private InputActionMap m_inputActionMap;
    private static PauseMenu m_instance;

    public static PauseMenu Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
        }
        return m_instance;
    }

    public override void LoadGame()
    {
        OnPauseGame(new InputAction.CallbackContext());
    }

    public override void LoadLaunchMenu()
    {
        Time.timeScale = 1;
        PlayerController.Inst().OnEnable();
        WeaponManager.Inst().OnEnable();
        SceneManager.LoadScene("LaunchMenu");
    }

    public void OnPauseGame(InputAction.CallbackContext context)
    {
        GameObject firstChild = transform.GetChild(0).gameObject;

        if (Time.timeScale > 0.0f)
        {
            Time.timeScale = 0;
            firstChild.SetActive(true);
            PlayerController.Inst().OnDisable();
            WeaponManager.Inst().OnDisable();
            levelCounter.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            firstChild.SetActive(false);
            PlayerController.Inst().OnEnable();
            WeaponManager.Inst().OnEnable();
            levelCounter.SetActive(false);
        }
    }

    public void OnEnable()
    {
        m_inputActionMap["PauseGame"].Enable();
    }

    public void OnDisable()
    {
        m_inputActionMap["PauseGame"].Disable();
    }

    public void Awake()
    {
        m_inputActionMap["PauseGame"].performed += OnPauseGame;
    }
}
