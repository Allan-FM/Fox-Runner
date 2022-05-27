using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTrackGenerator : MonoBehaviour
{
    [SerializeField] private TrackSegment[] segmentsPrefabs;
    private List<TrackSegment> currentSegments = new List<TrackSegment>();
    private void Start()
    {
        TrackSegment initialTrack = Instantiate(segmentsPrefabs[0], transform);
        currentSegments.Add(initialTrack);

        TrackSegment previousTrack = initialTrack;
        foreach(var trackPrefabs in segmentsPrefabs)
        {
            TrackSegment trackInstance = Instantiate(trackPrefabs, transform);
            trackInstance.transform.position = previousTrack.End.position
                + (trackInstance.transform.position - trackInstance.Start.position);


            currentSegments.Add(trackInstance);
            previousTrack = trackInstance;
        }
    }
}
