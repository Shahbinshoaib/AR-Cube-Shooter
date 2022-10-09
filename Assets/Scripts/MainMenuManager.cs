using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{


    public GameObject loading;
    public AnimationClip CameraOn;
    public AnimationClip CameraOff;
    public GameObject blackScreen;

    private Animation loadingAnim;
    private AudioSource loadionAudio;
    // Start is called before the first frame update
    void Awake()
    {
        loadingAnim = loading.GetComponent<Animation>();
        loadionAudio = loading.GetComponent<AudioSource>();
        StartCoroutine(InitilizeMainMenu());
        
    }
    IEnumerator InitilizeMainMenu()
    {
        blackScreen.SetActive(false);
        loadingAnim.clip = CameraOn;
        yield return new WaitForSeconds(2);
        loadingAnim.Play();
        loadionAudio.Play();

    }

    public void LoadGame()
    {
        StartCoroutine(InitilizeGamePlay());
    }

    IEnumerator InitilizeGamePlay()
    {
        loadingAnim.clip = CameraOff;
        loadingAnim.Play();
        loadionAudio.Play();
        yield return new WaitForSeconds(1);
   
        SceneManager.LoadScene(1);
    }
}
