using UnityEngine;

public class RocketStageBehavior : MonoBehaviour {

    public float heightTodestroyStage=50;
	// Update is called once per frame
	void Update () {
        if (AllObjectData.instance.IsNotFlyinig() || IsRangeReached())
        {
            gameObject.SetActive(false);
        }

	}
    private bool IsRangeReached()
    {
       return gameObject.transform.position.y + heightTodestroyStage < AllObjectData.instance.posY;
    }

    private void OnEnable()
    {
        SoundRandom.instance.ExecuteFallingSound();
    }
}
