using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{

    public static event Action<GameObject, int> onCollision;
    //The bullet prefab we instantiate
    //Nhà lắp ghép viên đạn mà chúng tôi khởi tạo
    public GameObject bulletPrefab;

    //Store the pooled bullets here
    //Lưu trữ các viên đạn tổng hợp tại đây
    private List<GameObject> bullets = new List<GameObject>();

    //How many bullets do we start with when the game starts
    //Chúng ta bắt đầu với bao nhiêu viên đạn khi trò chơi bắt đầu
    private const int INITIAL_POOL_SIZE = 10;

    //Sometimes it can be good to put a limit to how many bullets we can isntantiate or we might get millions of them
    //Đôi khi có thể tốt nếu đặt giới hạn số lượng đạn chúng ta có thể tạo ra hoặc chúng ta có thể nhận được hàng triệu viên đạn
    private const int MAX_POOL_SIZE = 20;


    private void Start()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Need a reference to the bullet prefab");
        }

        //Instantiate new bullets and put them in a list for later use
        //Tạo dấu đầu dòng mới và đưa chúng vào danh sách để sử dụng sau
        for (int i = 0; i < INITIAL_POOL_SIZE; i++)
        {
            GenerateBullet();
        }
    }


    //Generate a single new bullet and put it in list
    //Tạo một dấu đầu dòng mới và đưa nó vào danh sách
    private void GenerateBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform);

        newBullet.SetActive(false);

        bullets.Add(newBullet);
    }


    //Try to get a bullet
    //Cố gắng lấy một viên đạn
    public GameObject GetBullet()
    {
        //Try to find an inactive bullet
        //Cố gắng tìm một dấu đầu dòng không hoạt động
        for (int i = 0; i < bullets.Count; i++)
        {
            GameObject thisBullet = bullets[i];

            if (!thisBullet.activeInHierarchy)
            {
                return thisBullet;
            }
        }

        //We are out of bullets so we have to instantiate another bullet (if we can)
        //Chúng tôi hết đạn vì vậy chúng tôi phải tạo ra một viên đạn khác (nếu có thể)
        if (bullets.Count < MAX_POOL_SIZE)
        {
            GenerateBullet();

            //The new bullet is last in the list so get it
            //Dấu đầu dòng mới nằm cuối cùng trong danh sách, vì vậy hãy lấy nó
            GameObject lastBullet = bullets[bullets.Count - 1];

            return lastBullet;
        }

        return null;
    }

    private void OnCollisionEnter(Collision collision)
    {


        onCollision?.Invoke(collision.gameObject, 1);

        /* Destroy(this.gameObject);*/
    }
}
