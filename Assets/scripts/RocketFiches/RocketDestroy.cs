using System.Collections;
using System.Linq;
using UnityEngine;

public class RocketDestroy : MonoBehaviour {

    public static RocketDestroy instance;
    public string [] groundTag;
    
    public float maxSpeed;
    public string replacedObjectName;

    void Start () {
        instance = instance ?? this;
    }
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (groundTag.Any(collision.gameObject.tag.Contains)  && AllObjectData.instance.isStarted )
        {
            Destroy(false);
        }
    }
    public void Destroy(bool destroyed=true)
    {
        RocketMovement.instance.diasbleAll = true;
        //sound
        SoundRandom.instance.ResetEngineSound();

        if ((AllObjectData.instance.speed > maxSpeed)||destroyed)
        {
            ObjectPoolList.instance.GetPooledObject(replacedObjectName, gameObject.transform.position, gameObject.transform.rotation);
            foreach (var item in RocketMovement.instance.rocketTypes)
            {
                RocketMovement.instance.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                item.gameObject.SetActive(false);
            }
            //sound
            SoundManager.instance.PlaySound("SheepExplosion");
        }
        else
        {
            AllObjectData.instance.isSafeLanded = true;

            //sound
            SoundManager.instance.PlaySound("Achivement");

        }
        DestroySettings(destroyed);
        //loadMenu
    }
    private void DestroySettings(bool destroyed)
    {
        
        RocketMovement.instance.TimerStop();
        #region//save all
        PlayerStats.instance.SaveCurrentAll();
        AllObjectData.instance.rocketLanded = true;
        AllObjectData.instance.rocketDestroyed = destroyed;

        #endregion
        ////QuestButtonREset
        //QuestMainEngine.instance.questApplyedPressed = false;
        //save quest
        StartCoroutine(FinalWait());
    }

    IEnumerator FinalWait()
    {
        yield return new WaitForSeconds(ConstsLibrary.waitForSecondAfterEnd);
        RocketEngineParticles.instance.SetDefaults();//Анимация двигателей
        MenuButtonControl.instance.OnShopMenuEntered();

        //QuestButtonREset
        QuestMainEngine.instance.questApplyedPressed = false;
    }
}
