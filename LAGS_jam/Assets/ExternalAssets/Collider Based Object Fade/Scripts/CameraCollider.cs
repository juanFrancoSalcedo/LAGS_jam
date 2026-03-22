using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace Ifooboo
{
    [RequireComponent(typeof(Rigidbody))]
    
    public class CameraCollider : MonoBehaviour
    {
        [Range(0.25f, 3.0f)]
        public float sphereRadius = 0.5f;

        private int objectFadeLayer;

        private Rigidbody m_rigidbody;
        private CapsuleCollider m_collider;
        
        private float m_minSqrDistance = 0.01f;
        private float m_maxSqrDistance = 0.25f;
        
        private List<ObjectFade> m_objectList = new List<ObjectFade>();
        
        public float minSqrDistance { get { return m_minSqrDistance; } }
        public float maxSqrDistance { get { return m_maxSqrDistance; } }
        
        public List<ObjectFade> objectList { get { return m_objectList; } }
        
        private static bool useDistance = true;

        private void Awake()
        {
            objectFadeLayer = FindLayer(gameObject);

            m_rigidbody = GetComponent<Rigidbody>();

            m_collider = GetComponent<CapsuleCollider>();
        }

        private void OnEnable()
        {
            //ResetValues(transform, m_rigidbody, m_collider);
            
            m_collider.center = Vector3.zero;
            //m_collider.radius = sphereRadius;
            
            // ============================================

            gameObject.layer = objectFadeLayer;

            Shader.SetGlobalFloat("_UseDistanceFade", 1.0f);
            
            // ============================================
            
            float minDistance = Camera.main.nearClipPlane;

            m_minSqrDistance = minDistance * minDistance;

            if (m_minSqrDistance < 0.01f)
            m_minSqrDistance = 0.01f;
            
            // ============================================
            
            m_maxSqrDistance = sphereRadius * sphereRadius;
        }

        private void OnDisable()
        {
            ClearObjectList();

            Shader.SetGlobalFloat("_UseDistanceFade", 0f);
        }
        
        private void OnValidate()
        {
            m_maxSqrDistance = sphereRadius * sphereRadius;
            
            //if (m_collider != null) { m_collider.radius = sphereRadius; }
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
                    AddObjectFade(objectFade, m_objectList, useDistance);
                    
                    objectFade.SetCameraCollider(this);
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
                    RemoveObjectFade(objectFade, m_objectList, useDistance);
                }
            }
        }
        
        public static void AddObjectFade(ObjectFade objectFade, List<ObjectFade> objectList, bool useDistance)
        {
            objectList.Add(objectFade);

            objectFade.SetActive(true, useDistance);

            if (objectFade.IsUsingFadeGroup())
            {
                objectFade.GetFadeGroup().SetActive(true, useDistance, objectFade);
            }
        }
        
        public static void RemoveObjectFade(ObjectFade objectFade, List<ObjectFade> objectList, bool useDistance)
        {
            objectList.Remove(objectFade);
            
            objectFade.SetActive(false, useDistance);

            if (objectFade.IsUsingFadeGroup())
            {
                objectFade.GetFadeGroup().SetActive(false, useDistance, objectFade);
            }
        }
        
        public static void ResetValues(Transform target, Rigidbody rigidbody, Collider collider)
        {
            target.localPosition = Vector3.zero;
            target.localEulerAngles = Vector3.zero;

            target.localScale = Vector3.one;

            // ============================================

            rigidbody.constraints = RigidbodyConstraints.None;

            rigidbody.constraints = rigidbody.constraints | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX;
            rigidbody.constraints = rigidbody.constraints | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
            rigidbody.constraints = rigidbody.constraints | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;

            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;

            // ============================================

            collider.isTrigger = true;
        }

        public static int FindLayer(GameObject target)
        {
            int layer = LayerMask.NameToLayer("Object Fade");

            if (layer == -1)
            layer = 0;

            target.layer = layer;
            
            return layer;
        }
    }
}




