using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneColorChanger : MonoBehaviour {

    [SerializeField] private Color[] m_teamColors;
    [SerializeField] private CaptureZoneActor m_captureZoneActor;
    [SerializeField] private ChangeColor m_changeColor;
    [SerializeField] private float m_progress;

	// Use this for initialization
	void Start () {
        if (!m_captureZoneActor)
        {
            m_captureZoneActor = GetComponent<CaptureZoneActor>();
            if (!m_captureZoneActor)
            {
                print("no CaptureZoneActor Found");
                this.enabled = false;
            }
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (m_captureZoneActor)
        {
            m_progress = m_captureZoneActor.capturePercentage / 100;
            if (m_captureZoneActor.owner == CaptureZoneActor.Owner.none)
            {
               m_changeColor.ChangeColorTo(Color.Lerp(m_teamColors[0], m_teamColors [1], m_progress));
            }
            else if (m_captureZoneActor.owner == CaptureZoneActor.Owner.team1)
            {
                m_changeColor.ChangeColorTo(Color.Lerp(m_teamColors[0], m_teamColors[2], m_progress));
            }
            else if (m_captureZoneActor.owner == CaptureZoneActor.Owner.team2)
            {
                m_changeColor.ChangeColorTo(Color.Lerp(m_teamColors[0], m_teamColors[3], m_progress));
            }
        }
	}
}
