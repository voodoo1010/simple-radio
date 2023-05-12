# Simple Radio

A simple radio system implementation in Unity similar to the likes of Forza Horizon.

Pre-requisites:

- New Input System (for input)
- DoTween (for animations of the UI)

The system consists of two ScriptableObjects: `Playlist` and `Song` . Each `Playlist` contains the following data:

- Playlist Name (used to identify the name of the playlist, can be used as a radio station name)
- Songs List
- The index of the song that is being currently played (SongNumber)
- The time of the current song that was played in the current playlist before switching to a different playlist/station (LastSongSeek)
- The total length of the current song in the playlist that was being played (CurrentSongLength)
- A bool which checks if the current playlist is being played (CurrentlyPlaying)

This ScriptableObject also has a function which initializes some data for each of the playlist whenever the scene is loaded in the `Start` event function

`Song` consists of the following data:

- Name of the song (SongName)
- Name of the artist (SongArtist)
- Sprite of the album art (AlbumArt)
- AudioClip of the song (SongFile)

The project contains a prefab which has all of its data serialized in the prefab, except for the AudioSource which needs to be drag n dropped manually from the editor. Otherwise it should be plug n play for testing. Any changes/recommendations are appreciated for the project.

Note: if there is an issue related to the tweening of the UI, remember to keep the Canvas Render Mode as ‘Screen Space - Overlay’ in which you’re going to add the radio system in.