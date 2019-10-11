using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    //public GameObject 
    private float speed;
    private float radius;
    private Camera mainCamera;
    public GameObject Explosion;
    private bool ended = false;
    void Start()
    {
        mainCamera = MainSettings.MainCamera;
        speed = MainSettings.Weapon.Bomb.Speed;
        radius = MainSettings.Weapon.Bomb.Diameter/2;
        transform.localScale = new Vector3(MainSettings.Weapon.Bomb.Size, MainSettings.Weapon.Bomb.Size, MainSettings.Weapon.Bomb.Size);
    }

    private void OnTriggerEnter(Collider collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                if (!ended)
                {
                    CreateExplosion(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(-90, 0, 0));
                }
                break;
        }
    }

    void Update()
    {
        if (MainSettings.NotPause)
        {
            Vector3 point = mainCamera.WorldToViewportPoint(new Vector2(transform.position.x, transform.position.y + radius)); //Записываем положение объекта к границам камеры, X и Y это будут как раз верхние и нижние границы камеры
            if (point.y > 1f)
            {
                if (!ended)
                {
                    CreateExplosion(Explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(-90, 0, 0));
                }
            }
            else { transform.position += transform.forward * Time.deltaTime * speed; }
        }
    }
    void CreateExplosion(GameObject weapon, Vector3 pos, Vector3 rot) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {
        ended = true;
        GameObject go = Instantiate(weapon, pos, Quaternion.Euler(rot));
        foreach (GameObject goEnemy in MainSettings.Enemylist)
        {
            if (goEnemy.tag == "Enemy") { goEnemy.GetComponent<EnemyStats>().HP = -1; }
        }
        Destroy(gameObject, 0);
        Destroy(go, 4f);
    }
}
