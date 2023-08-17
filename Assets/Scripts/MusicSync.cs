using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MusicSync : MonoBehaviourPunCallbacks
{
    public AudioSource backgroundMusic;

    [Header("Settings")]
    public bool synchronizeMusicOnStart = true;
    public double syncTimeDelay = 1.0;

    private double startTime;
    private double syncTime;
    private double lastSyncTime;

    void Start()
    {
        if (synchronizeMusicOnStart && PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SyncMusicStartTime", RpcTarget.All, PhotonNetwork.Time);
        }
    }

    [PunRPC]
    private void SyncMusicStartTime(double time)
    {
        double currentNetworkTime = PhotonNetwork.Time;
        syncTime = time;
        lastSyncTime = currentNetworkTime;

        // Calculate the time elapsed on the music when it should start playing for all clients
        double elapsedMusicTime = (currentNetworkTime - syncTime) - syncTimeDelay;

        // Play the background music on all clients at the synchronized start time
        backgroundMusic.Play();

        // Set the start time to the synchronized point in the music clip
        startTime = backgroundMusic.time - elapsedMusicTime;
    }

    void Update()
    {
        if (backgroundMusic.isPlaying && PhotonNetwork.Time - lastSyncTime > 1.0)
        {
            photonView.RPC("SyncMusicStartTime", RpcTarget.All, PhotonNetwork.Time);
        }

        // Continuously synchronize the music playback
        if (backgroundMusic.isPlaying)
        {
            // Explicitly convert the double value to a float using (float)
            backgroundMusic.time = (float)((PhotonNetwork.Time - syncTime) - syncTimeDelay + startTime);
        }
    }
}
