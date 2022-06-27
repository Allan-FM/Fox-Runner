using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip rollSound;
    [SerializeField] private AudioClip jumpSound;
    private AudioSource audioSource;

    private AudioSource AudioSource => audioSource == null ?
        audioSource = GetComponent<AudioSource>() : audioSource;
    public void PlayJumpSound()
    {
        Play(jumpSound);
    }
    public void PlayRollSound()
    {
        Play(rollSound);
    }
    private void Play(AudioClip clip)
    {
        AudioUtility.PlayAudioCue(AudioSource, clip);   
    }
}
