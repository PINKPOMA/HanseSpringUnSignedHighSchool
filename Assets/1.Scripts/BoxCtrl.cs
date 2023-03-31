using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxCtrl : MonoBehaviour
{
    Vector2 Box_Tr = Vector2.zero;
    private List<Color> m_ColorList = new List<Color>();
    private float m_SpawnPoint = 0;
    private int m_BoxCount = 0;
    private float Box_SpawnTime = 0.0f;

    public GameObject BoxPef = null;
    public Transform BoxPoint = null;

    // Start is called before the first frame update
    void Start()
    {
        m_ColorList.Add(Color.yellow);
        m_ColorList.Add(Color.green);
        m_ColorList.Add(Color.red);
    }

    private void Update()
    {
        if(m_BoxCount < 30)
        {
            Box_SpawnTime += Time.deltaTime;
            if (0.15f < Box_SpawnTime)
            {
                Box_SpawnTime = 0.0f;
                Box_Spawn();
            }
        }
    }

    private void Box_Spawn()
    {
        for (int i = 0; i < 3; i++)
        {
            m_BoxCount++;
            GameObject go = GameObject.Instantiate(BoxPef) as GameObject;
            go.GetComponent<Image>().color = m_ColorList[i];
            go.transform.SetParent(BoxPoint);
            Box_Update(go);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Box_Update(collision.gameObject);
    }

    private void Box_Update(GameObject _box)
    {
        m_SpawnPoint = Random.Range(0.0f, 0.8f);

        float size = (float)Random.Range(45, 75);
        _box.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
        _box.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        _box.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(10, 20);
        _box.GetComponent<Rigidbody2D>().gravityScale = Random.Range(150, 200);

        Box_Tr.y = 700.0f;
        
        if (m_SpawnPoint <= 0.3f)
            Box_Tr.x = (float)Random.Range(-440, 441);
        else
        {
            m_SpawnPoint = (float)Random.Range(0, 2);

            if (m_SpawnPoint == 0)
                Box_Tr.x = (float)Random.Range(-900, -439);
            else if (m_SpawnPoint == 1)
                Box_Tr.x = (float)Random.Range(440, 901);
        }
        

        _box.transform.localPosition = Box_Tr;
    }
}
