using B_Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    [Header(" Shots ")]
    [SerializeField] AudioSource loadUpResource;
    [SerializeField] AudioSource tingCoin;
    [SerializeField] AudioSource collectResource;
    [SerializeField] AudioSource[] hitRock;
    [SerializeField] AudioSource[] swingPickAxe;
    [SerializeField] AudioSource sigh;

    [Header(" UI ")]
    [SerializeField] AudioSource button1;

    [Header(" Continues ")]
    [SerializeField] AudioSource AmbientDay;
    [SerializeField] AudioSource AmbientNight;
    [SerializeField] AudioSource AmbientCave;
    [SerializeField] AudioSource caveSteps;
    [SerializeField] AudioSource TickTackOne;
    [SerializeField] AudioSource TickTackTwo;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopAllAmbients();
        HandleSceneAudio(scene.name);
    }


    private void HandleSceneAudio(string sceneName)
    {
        if (sceneName.Contains("Level_01"))
            PlayAmbientCave();
        else if (sceneName.Contains("Night"))
        {
            PlayAmbientNight();
        }
        else if (sceneName.Contains("Level_00"))
            PlayAmbientDay();
    }

    private void StopAllAmbients()
    {
        AmbientDay.Stop();
        AmbientNight.Stop();
        AmbientCave.Stop();
        StopCaveSteps();
    }

    public void PlayLoadUpResource() => loadUpResource.Play();

    public void PlayAmbientDay() => AmbientDay.Play();

    public void PlayAmbientNight() => AmbientNight.Play();

    public void PlayTingCoin() => tingCoin.Play();

    public void PlayButton1() => button1.Play();
    
    public void PlaySwingPickAxe()
    {
        int randomIndex = Random.Range(0, swingPickAxe.Length);
        swingPickAxe[randomIndex].Play();
    }

    public void PlayHitRock(Transform trans = null)
    {
        int randomIndex = Random.Range(0, hitRock.Length);
        var first = System.Array.Find(hitRock, t => !t.isPlaying);
        if (first != null)
            first.Play();
        if(trans != null)
            first.transform.position = trans.position;
    }

    public void PlayAmbientCave() => AmbientCave.Play();
    
    public void PlayCaveSteps()
    {
        if(!caveSteps.isPlaying)
            caveSteps.Play();
    }
    public void StopCaveSteps() => caveSteps.Stop();
    public void PlayCollectResource() => collectResource.Play();
    public void PlaySigh() => sigh.Play();

    public void PlayTickTackOne()
    {
        if (!TickTackOne.isPlaying)
            TickTackOne.Play();
    }

    public void PlayTickTackTwo()
    {
        if (!TickTackTwo.isPlaying)
            TickTackTwo.Play();
    }

    public void StopTickTackOne() => TickTackOne.Stop();
    public void StopTickTackTwo() => TickTackTwo.Stop();
}
