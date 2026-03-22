using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace Ifooboo
{
    [RequireComponent(typeof(Rigidbody))]
    
    public class ToggleFadeCollider : MonoBehaviour
    {
        private int objectFadeLayer;
        
        private Rigidbody m_rigidbody;
        private Collider m_collider;
        
        private List<ObjectFade> m_objectList = new List<ObjectFade>();
        
        private static bool useDistance = false;
        
        private void Awake()
        {   
            objectFadeLayer = CameraCollider.FindLayer(gameObject);
            
            m_rigidbody = GetComponent<Rigidbody>();
            m_collider = GetComponent<Collider>();
        }
        
        private void OnEnable()
        {
            CameraCollider.ResetValues(transform, m_rigidbody, m_collider);
            
            gameObject.layer = objectFadeLayer;
            
            Shader.SetGlobalFloat("_UseDistanceFade", 1.0f);
        }
        
        private void OnDisable()
        {
            ClearObjectList();

            Shader.SetGlobalFloat("_UseDistanceFade", 0f);
        }
        
        private void ClearObjectList()
        {
            int listCount = m_objectList.Count;

            for (int i = 0; i < listCount; i ++)
            {
                m_objectList[i].SetActive(false, useDistance);
            }

            m_objectList.Clear();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == objectFadeLayer)
            {
                ObjectFade objectFade = other.GetComponent<ObjectFade>();
                
                if (objectFade != null && !m_objectList.Contains(objectFade))
                {
                    CameraCollider.AddObjectFade(objectFade, m_objectList, useDistance);
                    
                    objectFade.Fade(-1);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == objectFadeLayer)
            {
                ObjectFade objectFade = other.GetComponent<ObjectFade>();
                
                if (objectFade != null)
                {
                    CameraCollider.RemoveObjectFade(objectFade, m_objectList, useDistance);
                    
                    objectFade.Fade(1);
                }
            }
        }
    }
}




