/*
 * Filename: PauseMenu.cs
 * Developer: Austin Kugler
 * Purpose: This file includes a class for functionality related to the pause menu.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

/*
 * Class for functionality related to the game pause menu.
 */
public class PauseMenu : LaunchMenu
{
    [SerializeField] private InputActionMap m_inputActionMap;
    public GameObject levelCounter;

    /*
     * Loads the game scene itself.
     */
    public void LoadGame()
    {
        OnPauseGame(new InputAction.CallbackContext());
    }

    /*
     * Loads the launch menu
     */
    public void LoadLaunchMenu()
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
