using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void playSFX(string nome)
    {
        AudioClip sfx = Resources.Load<AudioClip>("Sounds/SFX/" + nome);
        GetComponent<AudioSource>().clip = sfx;
        GetComponent<AudioSource>().PlayOneShot(sfx);

    }
}
