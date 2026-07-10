using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionHandler : MonoBehaviour
{
    [Header("Collision Settings")]
    [SerializeField] float delayTime = 2f;

    [Header("Component Refrences")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip sfxCrash;
    [SerializeField] private AudioClip sfxFinish;
    [SerializeField] private ParticleSystem vfxCrash;
    [SerializeField] private ParticleSystem vfxFinish; 

    bool isControllable = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        if(Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isControllable = !isControllable;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(isControllable)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with Friendly");
                break;
            case "Fuel":
                Debug.Log("Fuel Collected");
                break;
            case "Finish":
                Debug.Log("Collided with Finish");
                StartFinishSequence();
                break;
            default:
                Debug.Log("Collided with something else");
                StartCrashSequence();
                break;
        }
    }
    private void StartFinishSequence()
    {
        isControllable = true;
        vfxFinish.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(sfxFinish);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayTime);
    }
    private void StartCrashSequence()
    {
        isControllable = true;
        vfxCrash.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(sfxCrash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", delayTime);
    }
    private void ReloadScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            nextSceneIndex = 0;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
