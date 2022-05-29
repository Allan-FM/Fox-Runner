using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private TrackSegment[] segmentsPrefabs;
    [SerializeField] private TrackSegment  firstTrackPrefab;
    [SerializeField] private int initialTrackCount = 10;

    private List<TrackSegment> currentSegments = new List<TrackSegment>();
    private void Start()
    {
        TrackSegment initialTrack = Instantiate(firstTrackPrefab, transform);
        currentSegments.Add(initialTrack);

        TrackSegment previousTrack = initialTrack;
        for(int i = 0; i < initialTrackCount; i++)
        {
            int index = Random.Range(0, segmentsPrefabs.Length);
            TrackSegment track = segmentsPrefabs[index];
            TrackSegment trackIntance = Instantiate(track, transform);
            trackIntance.transform.position = previousTrack.End.position + 
                (trackIntance.transform.position - trackIntance.Start.position);

            foreach(var obstacleSpawner in trackIntance.ObstacleSpawners)
            {
                obstacleSpawner.SpawnObstacle();
            }

            previousTrack = trackIntance;
        }
    }
}
