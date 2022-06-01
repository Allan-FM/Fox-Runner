using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [SerializeField] private TrackSegment[] segmentsPrefabs;
    [SerializeField] private TrackSegment  firstTrackPrefab;

    [SerializeField] private TrackSegment[] easyTrackPrefabs;
    [SerializeField] private TrackSegment[] hardTrackPrefabs;
    [SerializeField] private TrackSegment[] rewardTrackPrefab;

    [Header("Endless Generation Parameters")]
    [Space]
    [SerializeField] private int initialTrackCount = 10;
    [SerializeField] private int minTrackInFrontOfPlayer = 3;

    [Header("Level Difficulty Parameters")]
    [Range(0, 1)]
    [SerializeField] private float hardTrackChance = 0.2f;

    [SerializeField] private int minTrackBeforeReward = 10;
    [SerializeField] private int maxTrackBeforeReward = 20;
    [SerializeField]  private int minRewardTrackCount;
    [SerializeField] private int maxRewardTrackCount;

    private List<TrackSegment> currentSegments = new List<TrackSegment>();
    [SerializeField] private float minDistanceToConsiderInsadeTrack = 3;
    private bool isSpawningRewardTrack = false;
    private int rewardTrackLeftToSpawn = 0;
    private int trackSpawnedAfterLastReward = 0;
    private void Start()
    {
        SpawnTrackSegment(firstTrackPrefab, null);
        SpawnTracks(initialTrackCount);
    }
    
    private void Update()
    {
        UpdateTracks();
    }

    private void UpdateTracks()
    {
        int playerTrackIndex = FindTrackIndexWithPlayer();
        if (playerTrackIndex < 0)
        {
            return;
        }

        SpawnNewTrackIfNeeded(playerTrackIndex);
        DespawnTrackBehindPlayer(playerTrackIndex);
    }

    private void DespawnTrackBehindPlayer(int playerTrackIndex)
    {
        for (int i = 0; i < playerTrackIndex; i++)
        {
            TrackSegment track = currentSegments[i];
            Destroy(track.gameObject);
        }
        currentSegments.RemoveRange(0, playerTrackIndex);
    }

    private void SpawnNewTrackIfNeeded(int playerTrackIndex)
    {
        int tracksInFrontOfPlayer = currentSegments.Count - (playerTrackIndex + 1);
        if (tracksInFrontOfPlayer < minTrackInFrontOfPlayer)
        {
            SpawnTracks(minTrackInFrontOfPlayer - tracksInFrontOfPlayer);
        }
    }

    private int FindTrackIndexWithPlayer()
    {
        int playerTrackIndex = -1;
        for (int i = 0; i < currentSegments.Count; i++)
        {
            TrackSegment track = currentSegments[i];
            if (player.transform.position.z >= (track.Start.position.z + minDistanceToConsiderInsadeTrack)
                && player.transform.position.z <= track.End.position.z)
            {
                playerTrackIndex = i;
                break;
            }
        }

        return playerTrackIndex;
    }

    private void SpawnTracks(int trackCount)
    {
        TrackSegment previousTrack = currentSegments.Count > 0
            ? currentSegments[currentSegments.Count - 1] : null;
        for (int i = 0; i < trackCount; i++)
        {
            var track = GetRandomTrack();
            previousTrack = SpawnTrackSegment(track, previousTrack);
        }
    }
    private TrackSegment GetRandomTrack()
    {
        TrackSegment[] trackList = null;
        if(isSpawningRewardTrack)
        {
            trackList = rewardTrackPrefab;
        }
        else
        {
            trackList = Random.value <= hardTrackChance ? hardTrackPrefabs : easyTrackPrefabs;
        }
        return trackList[Random.Range(0, trackList.Length)];

    }
    private TrackSegment SpawnTrackSegment(TrackSegment track, TrackSegment previousTrack)
    {

        TrackSegment trackIntance = Instantiate(track, transform);

        if (previousTrack != null)
        {
            trackIntance.transform.position = previousTrack.End.position +
                     (trackIntance.transform.position - trackIntance.Start.position);
        }
        else
        {
            trackIntance.transform.localPosition = Vector3.zero;
        }

        foreach (var obstacleSpawner in trackIntance.ObstacleSpawners)
        {
            obstacleSpawner.SpawnObstacle();
        }

        currentSegments.Add(trackIntance);
        UpdateRewardTracking();
        return trackIntance;
    }
    private void UpdateRewardTracking()
    {
        if(isSpawningRewardTrack)
        {
            rewardTrackLeftToSpawn--;
            if(rewardTrackLeftToSpawn <= 0)
            {
                isSpawningRewardTrack = false;
                trackSpawnedAfterLastReward = 0;
            }
        }
        else
        {
            trackSpawnedAfterLastReward++;
            int requiredTrackBeforeReward = Random.Range(minTrackBeforeReward, maxTrackBeforeReward + 1);
            if(trackSpawnedAfterLastReward >= requiredTrackBeforeReward)
            {
                isSpawningRewardTrack = true;
                rewardTrackLeftToSpawn = Random.Range(minRewardTrackCount, maxRewardTrackCount + 1);
            }
        }
    }
}
