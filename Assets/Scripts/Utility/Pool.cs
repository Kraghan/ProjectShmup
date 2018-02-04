using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{

    private List<GameObject> m_freeList;
    private GameObject m_template;


    public void Initialize(GameObject obj)
    {
        m_template = obj;
        m_freeList = new List<GameObject>(100);

        for (int i = 0; i < 100; ++i)
        {
            GameObject tmp = Instantiate(obj, transform);
            m_freeList.Add(tmp);
            tmp.SetActive(false);
        }
    }
        
    public GameObject PickUp()
    {
        int index = m_freeList.Count - 1;
        if(index < 0)
        {
            GameObject tmp = Instantiate(m_template, transform);
            m_freeList.Add(tmp);
            tmp.SetActive(false);
            index++;
        }

        GameObject obj = m_freeList[index];
        if (obj == null)
            Debug.Log("Grrrr");
        m_freeList.RemoveAt(index);
        obj.SetActive(true);

        return obj;
    }

    public GameObject Release(GameObject obj)
    {
        m_freeList.Add(obj);

        obj.transform.parent = transform;
        obj.SetActive(false);

        return obj;
    }
}
