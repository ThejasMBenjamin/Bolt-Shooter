using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image SceneLoad;
    [SerializeField] private Animator SceneAnim;
    [SerializeField] private AudioClip ClickClip; //I know thier is no need to use Clip but xD
    [SerializeField] private AudioSource MainAudioSource;
    [SerializeField] private AudioClip BgMusic;
    [SerializeField] private AudioSource ad;

    private void Start()
    {
        SceneLoad.enabled = false;
        ad.clip = ClickClip; 
        MainAudioSource.clip = BgMusic;
        MainAudioSource.loop = true;
        MainAudioSource.Play();
    }
    public void OnStartClick()
    {
        ad.Play();
        SceneLoad.enabled = true;
        StartCoroutine(LoadNextScene());

    }

    public void OnGitBtnClick()
    {
        ad.Play();
        Application.OpenURL("https://github.com/ThejasMBenjamin");
    }

    public void OnXBtnClikc()
    {
        ad.Play();
        Application.OpenURL("https://x.com/ThejasMBenjamin");
    }
    public void OnQuitClick()
    {
        ad.Play();
        Application.Quit();
    }


    IEnumerator LoadNextScene()
    {
        MainAudioSource.loop = false;
        MainAudioSource.Stop();
        SceneAnim.SetTrigger("fadeIn");
        yield return new WaitForSeconds(.8f);
        SceneAnim.SetTrigger("fadeOut");
        SceneManager.LoadScene("SampleScene");

    }

}
