using UnityEngine;

public class AirObjectEngine : MonoBehaviour {

    public int amount;
    public bool destroyableOnHit;
    public AirObjectType airObjectType;
    public AirObjectClass airObjectClass;
    /// <summary>
    /// Name of object that replaces that object if name is empty no ned to replace
    /// </summary>
    public string replacedObjectName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.CompareTag("EnemyFace") || collision.CompareTag("Enemy"))
        //{
        //    return;
        //}
        // print(" OnTriggerEnter2D"+ objectName);
        DoSound();
        if (collision.CompareTag("Player"))
        {
            //todo factory Here!!!
            switch (airObjectType)
            {
                case AirObjectType.DamageDealler:
                    RocketMovement.instance.DoDamage(amount);
                    //collision.GetComponentInParent<RocketMovement>().DoDamage(damageDeal);
                    break;
                case AirObjectType.Healer:
                    RocketMovement.instance.DoDamage(-amount);
                    break;
                case AirObjectType.Fuel:
                    RocketMovement.instance.AddFuel(amount);
                    break;
                case AirObjectType.HeatDecreaser:
                    RocketMovement.instance.DoHeat(-amount);
                    break;
                case AirObjectType.HeatIncreaser:
                    RocketMovement.instance.DoHeat(amount);
                    break;
                case AirObjectType.Money:
                    PlayerStats.instance.AddCash(amount);
                    break;
                default:
                    break;
            }
            if (destroyableOnHit)
            {
                if (!string.IsNullOrEmpty(replacedObjectName))
                {
                ObjectPoolList.instance.GetPooledObject(replacedObjectName, gameObject.transform.position, gameObject.transform.rotation);
                }
                gameObject.SetActive(false);
            }
        }
    }
    //todo factory or builder? Here!!!
    private void DoSound()
    {
        switch (airObjectClass)
        {
            case AirObjectClass.SpaceTrash:
                SoundRandom.instance.ExecuteRandomTrashCrush();
                break;
            case AirObjectClass.SpaceSheep:
                SoundRandom.instance.ExecuteRandomSmallExpolsion();
                break;
            case AirObjectClass.Cunsumable:
                SoundManager.instance.PlaySound("gotItem");
                break;
            default:
                break;

        }
        
    }
}
