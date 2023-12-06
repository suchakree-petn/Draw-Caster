using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    // public AudioSource src;
    [SerializeField] private AudioSource  sfx_back, sfx_go, sfx_cast, sfx_castFail, sfx_c1, sfx_c2, sfx_c3, loopMusic;
    public static SoundSource Instance;
    private void Awake() 
    {
        GameObject[] soundSourceObj = GameObject.FindGameObjectsWithTag("Sound Soucre");
        if(soundSourceObj.Length > 1){
            Destroy(gameObject);
        }
        if (Instance != null && Instance != this)
            {
                DestroyImmediate(gameObject);
            }else{
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
    }
    public void PlaySfxGo()
    {
        sfx_go.Play();
    }
    public void PlaySfxBack()
    {
        sfx_back.Play();
    }
    public void PlaySfxCastSpell()
    {
        sfx_cast.Play();
    }
    public void PlaySfxCastSpellFail()
    {
        sfx_castFail.Play();
    }
    public void PlaySfxCast1()
    {
        sfx_c1.Play();
    }
    public void PlaySfxCast2()
    {
        sfx_c2.Play();
    }
    public void PlaySfxCast3()
    {
        sfx_c3.Play();
    }
}
