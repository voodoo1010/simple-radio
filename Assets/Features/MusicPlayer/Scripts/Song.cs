using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Music Player/Create Song",fileName = "Song_")]
public class Song : ScriptableObject
{
    public string SongName;
    public string SongArtist;
    public Sprite AlbumArt;
    public AudioClip SongFile;
}
