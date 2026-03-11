using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public class PanelManager : MonoBehaviour
{
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject DeathScreen;
    [SerializeField] private Animator SceneAnim;
    [SerializeField] private Image SceneLoadImg;
    [SerializeField] private AudioClip CloseBtnClip;
    [SerializeField] private AudioClip MouseClickClip;
    [SerializeField] private Crosshair crosshair;
    private AudioSource audioSource;

    private void Start()
    {
        SceneLoadImg.enabled = false;
        PauseScreen.SetActive(false);
        DeathScreen.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame){ 
            PauseScreen.SetActive(true); Time.timeScale = 0f;
            crosshair.changeCursor = true;
        }
    }
    public void OnCloseBtnClick()
    {
        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
        audioSource.PlayOneShot(CloseBtnClip);
        crosshair.changeCursor = false;


    }

    public void OnContinueBtnClick()
    {
        Time.timeScale = 1f;
        PauseScreen.SetActive(false);
        audioSource.PlayOneShot(MouseClickClip);
        crosshair.changeCursor = false;
    }

    public void OnRestartBtnClick()
    {
        Time.timeScale = 1f;
        audioSource.PlayOneShot(MouseClickClip);
        crosshair.changeCursor = false;
        StartCoroutine(RestartAfterAudio());

    }

    public void OnHomeBtnClick()
    {
        Time.timeScale = 1f;
        audioSource.PlayOneShot(MouseClickClip);
        crosshair.changeCursor = false;
        StartCoroutine(LoadNextScene());
    }

    public void OnQuitBtnClick()
    {
        Application.Quit();
    }

    private IEnumerator RestartAfterAudio()
    {
        yield return new WaitForSecondsRealtime(.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void OnDeathScreen()
    {
        DeathScreen.SetActive(true);
        Time.timeScale = 0f;
        crosshair.changeCursor = true;
    }

    IEnumerator LoadNextScene()
    {
        Time.timeScale = 1f;
        SceneLoadImg.enabled = true;
        PauseScreen.SetActive(false);
        crosshair.changeCursor = false;
        SceneAnim.SetTrigger("fadeIn");
        yield return new WaitForSeconds(.8f);
        SceneAnim.SetTrigger("fadeOut");
        SceneManager.LoadScene("MainMenu");

    }
}
