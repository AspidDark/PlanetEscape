using UnityEngine;
using Cinemachine;

public class CameraMySettingsEngine : MonoBehaviour {

    public float closestZoomValue = 8;
    public float farestZoomValue = 11;
    public float zoomSpeed = 0.3f;
    public float unZoomSpeed = 1;
    public CinemachineVirtualCamera vcam;
    public float speedZoomBorder = 15;

    private float currentZoom=8;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ((currentZoom< farestZoomValue) && (AllObjectData.instance.speed > speedZoomBorder))
        {
            ZoomOut();
        }
        else if((currentZoom> closestZoomValue)&&(AllObjectData.instance.speed< speedZoomBorder))
        {
            ZoomIn();
        }
	}
    //The same but we can variate zooum speed
    private void ZoomOut()
    {
        currentZoom = vcam.m_Lens.OrthographicSize;
        vcam.m_Lens.OrthographicSize+= unZoomSpeed * MainCount.instance.fixedDeltaTime;

    }
    //The same but we can variate zooum speed
    private void ZoomIn()
    {
        currentZoom = vcam.m_Lens.OrthographicSize;
        vcam.m_Lens.OrthographicSize -= unZoomSpeed * MainCount.instance.fixedDeltaTime;
    }
}
