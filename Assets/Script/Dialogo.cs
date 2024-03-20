using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogo : MonoBehaviour
{
    public string nome;
    public Sprite sprite;
    public string text;
    public bool latoSX;
    public bool player;
    public Dialogo(string nome, string texture, string text, bool latoSX, bool player)
    {
        this.nome = nome;
        sprite = Resources.Load<Sprite>("Characters/Artwork/Idle/Portrait/" + texture);
        this.text = text;
        this.latoSX = latoSX;
        this.player = player;
    }
}
