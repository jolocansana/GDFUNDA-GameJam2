using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIFunctions : MonoBehaviour
{
    public AudioSource alarmClockSound;
    public GameObject firstPage;
    public GameObject secondPage;
    public GameObject startPanel;
    public GameObject textObject;

    private string textToType = "After a long nap, you woke up.\n\nYou are screwed. Mom is going to be home in a while and you haven't done the tasks she left for you to do.\n\nWith only little time left, you have to finish all the tasks or else you will be grounded again for second time in a month.";

    void Start()
    {
        alarmClockSound.time = 5f;
        alarmClockSound.Play();
        StartCoroutine(SwitchPage());
    }

    IEnumerator SwitchPage()
    {

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(6);

        //After we have waited 5 seconds print the time again.
        firstPage.SetActive(false);
        secondPage.SetActive(true);

        StartCoroutine(TypewriterEffect(textToType));
    }

    IEnumerator TypewriterEffect(string textToType)
    {
        float time = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            time += Time.deltaTime * 50f; // deltaTime * speed
            charIndex = Mathf.FloorToInt(time);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            textObject.GetComponent<Text>().text = textToType.Substring(0, charIndex);

            yield return null;
        }

        textObject.GetComponent<Text>().text = textToType;
        startPanel.SetActive(true);
    }

    public void PlayGame(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
