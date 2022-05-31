using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [SerializeField] private TrackSegment[] segmentsPrefabs;
    [SerializeField] private TrackSegment  firstTrackPrefab;

    [Header("Endless Generation Parameters")]
    [Space]
    [SerializeField] private int initialTrackCount = 10;
    [SerializeField] private int minTrackInFrontOfPlayer = 3;

    private List<TrackSegment> currentSegments = new List<TrackSegment>();
    [SerializeField] private float minDistanceToConsiderInsadeTrack = 3;

    private void Start()
    {
        SpawnTrackSegment(firstTrackPrefab, null);
        SpawnTracks(initialTrackCount);
    }
    
    private void Update()
    {
        int playerTrackIndex = FindTrackIndexWithPlayer();
        if (playerTrackIndex < 0)
        {
            return;
        }

        int tracksInFrontOfPlayer = currentSegments.Count - (playerTrackIndex + 1);
        if (tracksInFrontOfPlayer < minTrackInFrontOfPlayer)
        {
            SpawnTracks(minTrackInFrontOfPlayer - tracksInFrontOfPlayer);
        }
        for (int i = 0; i < playerTrackIndex; i++)
        {
            TrackSegment track = currentSegments[i];
            Destroy(track.gameObject);
        }
        currentSegments.RemoveRange(0, playerTrackIndex);
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
            int index = Random.Range(0, segmentsPrefabs.Length);
            TrackSegment track = segmentsPrefabs[index];
            previousTrack = SpawnTrackSegment(track, previousTrack);
        }
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
        return trackIntance;
    }
}
