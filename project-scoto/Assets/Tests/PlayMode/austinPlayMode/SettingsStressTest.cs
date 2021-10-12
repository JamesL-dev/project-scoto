using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

public class SettingsStressTest : MonoBehaviour
{
    float fps = 0;

    [UnityTest]
    public IEnumerator AntiAliasingStressTest()
    {
        SceneManager.LoadScene("Game");
        yield return new WaitForSeconds(5);

        int updates = 0;
        while (true) // updates < 10000
        {
            fps = 1.0f / Time.deltaTime;

            QualitySettings.SetQualityLevel(5, true);
            yield return null;
            QualitySettings.SetQualityLevel(1, true);

            if (fps < 30)
            {
                Debug.Log("Anti-aliasing stress test <30 FPS after " + updates + " updates");
                break;
            }

            updates++;
        }
        Debug.Log("Current update: " + updates);
        yield return null;
    }
}
