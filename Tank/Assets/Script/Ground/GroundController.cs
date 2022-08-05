using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public GameObject block;
    void Start()
    {
        for (int i = 0; i < 160; i++)
        {
            for (int j = 0; j < 160; j++)
            {
                if (i == 0 || j == 0 || i == 159 || j == 159)
                {
                    Instantiate(block, new Vector3(i, 3, j), Quaternion.identity);
                }
            }
        }
    }
}
