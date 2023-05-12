using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Music Player/Create Playlist",fileName = "Playlist_")]
public class Playlist : ScriptableObject
{
    public string PlaylistName;
    public List<Song> SongsList;
    [HideInInspector] public int SongNumber;
    [HideInInspector] public float LastSongSeek;
    [HideInInspector] public float CurrentSongLength;
    [HideInInspector] public bool CurrentlyPlaying;

    public void InitializeData()
    {
        CurrentlyPlaying = false;
        SongNumber = Random.Range(0, SongsList.Count);
        CurrentSongLength = SongsList[SongNumber].SongFile.length;
        LastSongSeek = 0;
    }
}
