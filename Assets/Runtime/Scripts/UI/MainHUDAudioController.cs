using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainHUDAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip buttonAudio;
    [SerializeField] private AudioClip countDownAudio;
    [SerializeField] private AudioClip countDownFineshAudio;

    private AudioSource audioSource;
    private AudioSource AudioSource => audioSource == null ?
        audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayButtonAudio()
    {
        Play(buttonAudio);
    }
    public void PlayCountDownAudio()
    {
        Play(countDownFineshAudio);
    }
    private void Play(AudioClip clip)
    {
        AudioUtility.PlayAudioCue(AudioSource, clip);
    }
}
