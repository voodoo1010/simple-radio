using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Music Player/Create Playlist",fileName = "Playlist_")]
public class Playlist : ScriptableObject
{
    public string PlaylistName;
    public List<Song> SongsList;
    public int SongNumber;
    public float LastSongSeek;
    public float CurrentSongLength;
    public bool CurrentlyPlaying;

    public void InitializeData()
    {
        CurrentlyPlaying = false;
        SongNumber = Random.Range(0, SongsList.Count);
        CurrentSongLength = SongsList[SongNumber].SongFile.length;
        LastSongSeek = 0;
    }
}
