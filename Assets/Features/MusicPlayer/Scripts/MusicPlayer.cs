using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<Playlist> musicPlaylists;
    [SerializeField] private InputActionReference nextButton;
    [SerializeField] private Image albumArt;
    [SerializeField] private TextMeshProUGUI playlistName;
    [SerializeField] private TextMeshProUGUI songName;
    [SerializeField] private TextMeshProUGUI artistName;
    [SerializeField] private RectTransform playerUI;
    [SerializeField] private CanvasGroup musicPlayerGroup;
    private int _songIndex;
    private int _playlistIndex;
    private float _lastAnchoredPosition;

    void Start()
    {
        foreach (var playlist in musicPlaylists)
        {
            playlist.InitializeData();
        }
        _lastAnchoredPosition = playerUI.position.x;
        _playlistIndex = Random.Range(0, musicPlaylists.Count);
        SetSong(_playlistIndex);
        playerUI.DOMoveX(_lastAnchoredPosition - 400f, 0.5f).SetEase(Ease.OutExpo);
        audioSource.Play();
        musicPlayerGroup.DOFade(0f, 0.5f).SetEase(Ease.OutExpo).SetDelay(3f);
    }
    void OnEnable()
    {
        nextButton.action.performed += PlayNextPlaylist;
        nextButton.action.Enable();
    }
    void OnDisable()
    {
        nextButton.action.performed -= PlayNextPlaylist;
        nextButton.action.Disable();
    }
    void Update()
    {
        PlaySongInBackground();
        PlayNext();
    }

    void PlayNext()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Stop();
            musicPlaylists[_playlistIndex].SongNumber++;
            musicPlaylists[_playlistIndex].LastSongSeek = 0;
            if (musicPlaylists[_playlistIndex].SongNumber > musicPlaylists[_playlistIndex].SongsList.Count - 1)
            {
                musicPlaylists[_playlistIndex].SongNumber = 0;
            }
            SetSong(_playlistIndex);
            musicPlaylists[_playlistIndex].CurrentSongLength = audioSource.clip.length;
            Sequence seq = DOTween.Sequence();
            seq.Append(musicPlayerGroup.DOFade(1f, 0.5f).SetEase(Ease.OutExpo)).Append(musicPlayerGroup.DOFade(0f, 0.5f).SetEase(Ease.OutExpo).SetDelay(3f));
            audioSource.Play();
        }
    }
    void PlaySongInBackground()
    {
        for (int i = 0; i < musicPlaylists.Count; i++)
        {
            if (!musicPlaylists[i].CurrentlyPlaying)
            {
                RunInBackground(musicPlaylists[i]);
            }
        }
    }
    private void RunInBackground(Playlist playlist)
    {
        if (playlist.LastSongSeek < playlist.CurrentSongLength)
        {
            playlist.LastSongSeek += Time.deltaTime;
        }
        else
        {
            playlist.SongNumber++;
            if (playlist.SongNumber > playlist.SongsList.Count - 1)
            {
                playlist.SongNumber = 0;
            }
            playlist.LastSongSeek = 0;
        }
    }
    void PlayNextPlaylist(InputAction.CallbackContext context)
    {
        DOTween.KillAll();
        musicPlayerGroup.alpha = 1f;
        musicPlaylists[_playlistIndex].LastSongSeek = audioSource.time;
        musicPlaylists[_playlistIndex].CurrentlyPlaying = false;
        audioSource.Stop();
        _playlistIndex++;
        if (_playlistIndex == musicPlaylists.Count)
        {
            _playlistIndex = 0;
        }
        playerUI.position = new Vector2(_lastAnchoredPosition, playerUI.position.y);
        playerUI.DOMoveX(_lastAnchoredPosition - 400f, 0.5f).SetEase(Ease.OutExpo);
        SetSong(_playlistIndex);
        audioSource.Play();
        musicPlayerGroup.DOFade(0f, 0.5f).SetEase(Ease.OutExpo).SetDelay(3f);
    }
    private void SetSong(int playListNum)
    {
        musicPlaylists[playListNum].CurrentlyPlaying = true;
        var songNum = musicPlaylists[playListNum].SongNumber;
        playlistName.text = musicPlaylists[playListNum].PlaylistName;
        songName.text = musicPlaylists[playListNum].SongsList[songNum].SongName;
        artistName.text = musicPlaylists[playListNum].SongsList[songNum].SongArtist;
        albumArt.sprite = musicPlaylists[playListNum].SongsList[songNum].AlbumArt;
        audioSource.clip = musicPlaylists[playListNum].SongsList[songNum].SongFile;
        audioSource.time = musicPlaylists[playListNum].LastSongSeek;
        musicPlaylists[playListNum].CurrentSongLength = audioSource.clip.length;
    }
}