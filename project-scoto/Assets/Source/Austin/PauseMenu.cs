/*
 * Filename: PauseMenu.cs
 * Developer: Austin Kugler
 * Purpose: Includes functionality for player interaction with the pause menu.
 * Software Pattern: Singleton
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

/*
 * Singleton superclass for the pause menu.
 *
 * Member variables:
 * m_levelCount -- A counter to display the current level number.
 * m_inputActionMap -- A mapping for inputs that occur while the pause menu is active.
 * m_instance -- The singleton instance of PauseMenu.
 */
public sealed class PauseMenu : BaseMenu
{
    public GameObject m_levelCounter;
    [SerializeField] private InputActionMap m_inputActionMap;
    private static PauseMenu m_instance;

    /*
     * Gets a reference to the instance of the singleton; otherwise creates the necessary.
     *
     * Returns:
     * PauseMenu -- Reference to the singleton instance.
     */
    public static PauseMenu Inst() {
        if (m_instance == null)
        {
            m_instance = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
        }
        return m_instance;
    }

    /*
     * Loads the game from a paused state.
     */
    public override void LoadGame()
    {
        OnPauseGame(new InputAction.CallbackContext());
    }

    /*
     * Loads the launch menu from a paused state.
     */
    public override void LoadLaunchMenu()
    {
        LevelGeneration.Inst().SetLevelNum(1);
        Time.timeScale = 1;
        PlayerController.Inst().OnEnable();
        WeaponManager.Inst().OnEnable();
        SceneManager.LoadScene("LaunchMenu");
    }

    /*
     * Toggles the games pause state.
     *
     * Parameters:
     * context -- Information regarding input actions.
     */
    public void OnPauseGame(InputAction.CallbackContext context)
    {
        GameObject firstChild = transform.GetChild(0).gameObject;

        if (Time.timeScale > 0.0f)
        {
            Time.timeScale = 0;
            firstChild.SetActive(true);
            PlayerController.Inst().OnDisable();
            WeaponManager.Inst().OnDisable();
            m_levelCounter.SetActive(true);
            GameObject.Find("Player").GetComponent<AudioSource>().Pause();
        }
        else
        {
            Time.timeScale = 1;
            firstChild.SetActive(false);
            PlayerController.Inst().OnEnable();
            WeaponManager.Inst().OnEnable();
            m_levelCounter.SetActive(false);
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
