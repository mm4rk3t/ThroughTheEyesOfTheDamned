using UnityEngine;

public static class SoundManager
{
    private static AudioSource audioSource;
    //use this for only Player/Weapon Sounds
    public static void PlayerSound(AudioClip clip)
    {
        PlaySound(clip);
    }
    //use this for only Enemy Sounds
    public static void EnemySound(AudioClip clip)
    {
        PlaySound(clip);
    }
    //use this for general sound playing
    public static void PlaySound(AudioClip clip)
    {
        if (audioSource==null)
        {
            GameObject soundGameObject = new GameObject("SoundManager");
            audioSource = soundGameObject.AddComponent<AudioSource>();
        }

        audioSource.PlayOneShot(clip);
    }

}
