using B_Extensions;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header(" Shots ")]
    [SerializeField] AudioSource loadUpResource;
    [SerializeField] AudioSource tingCoin;
    [SerializeField] AudioSource[] hitRock;

    [Header(" UI ")]
    [SerializeField] AudioSource button1;

    [Header(" Continues ")]
    [SerializeField] AudioSource AmbientDay;
    [SerializeField] AudioSource AmbientNight;
    [SerializeField] AudioSource AmbientCave;
    [SerializeField] AudioSource caveSteps;

    public void PlayLoadUpResource() => loadUpResource.Play();

    public void PlayAmbientDay() => AmbientDay.Play();

    public void PlayAmbientNight() => AmbientNight.Play();

    public void PlayTingCoin() => tingCoin.Play();

    public void PlayButton1() => button1.Play();

    public void PlayHitRock()
    {
        int randomIndex = Random.Range(0, hitRock.Length);
        hitRock[randomIndex].Play();
    }

    public void PlayAmbientCave() => AmbientCave.Play();
    public void PlayCaveSteps()
    {
        if(!caveSteps.isPlaying)
            caveSteps.Play();
    }

    public void StopCaveSteps() => caveSteps.Stop();
}
