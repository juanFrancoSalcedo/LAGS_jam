using UnityEngine;
using UnityEngine.Rendering;

using System.Collections;
using System.Collections.Generic;

namespace Ifooboo
{
    public class ObjectFade : MonoBehaviour
    {
        public List<Renderer> renderers = new List<Renderer>();
        
        [Range(0f, 1.0f)]
        public float fadeOverride;
        
        [Range(0f, 1.0f)]
        public float maxFade = 1.0f;
        
        readonly int hashFade = Shader.PropertyToID("_Fade");
        
        // ============================================
        
        private static Renderer staticMesh = null;

        private bool updateOpacity;

        private CameraCollider cameraCollider;

        private Transform cameraTransform;

        private Collider m_collider;

        private List<Material> materials = new List<Material>();

        private int materialCount;

        private float opacity = 1.0f;

        private bool usingRenderers = true;

        private ObjectFadeGroup fadeGroup;

        private bool usingFadeGroup;
        
        private IEnumerator fadeCoroutine;
        
        private void Awake()
        {
            FindLayer();

            // ============================================

            m_collider = GetComponent<Collider>();

            m_collider.isTrigger = true;

            // ============================================

            Renderer m_renderer = GetComponent<Renderer>();

            if (m_renderer != null && m_renderer.enabled)
            {
                renderers.Add(m_renderer);
            }

            // ============================================

            int listCount = renderers.Count;

            for (int i = 0; i < listCount; i ++)
            {
                int arrayLength = renderers[i].materials.Length;

                for (int j = 0; j < arrayLength; j ++)
                {
                    materials.Add(renderers[i].materials[j]);
                }
            }

            materialCount = materials.Count;

            // ============================================

            FadeMaterials(opacity);
        }

        private void OnDisable()
        {
            if (cameraCollider != null)
            {
                cameraCollider.objectList.Remove(this);
            }

            SetActive(false, false);
        }

        private void OnValidate()
        {
            SetOpacity();
        }

        private void Update()
        {
            if (updateOpacity && cameraTransform != null)
            {
                UpdateOpacity();
            }
        }

        private void UpdateOpacity()
        {
            opacity = 1.0f;

            if (cameraTransform != null)
            {
                Vector3 cameraPosition = cameraTransform.position;

                Vector3 closestPoint = Physics.ClosestPoint(cameraPosition, m_collider, m_collider.transform.position, m_collider.transform.rotation);

                Vector3 difference = cameraPosition - closestPoint;

                // ============================================
                
                float minSqrDistance = cameraCollider.minSqrDistance;
                float maxSqrDistance = cameraCollider.maxSqrDistance;

                float distanceSquared = difference.sqrMagnitude;

                if (distanceSquared <= maxSqrDistance)
                {
                    opacity = NormalizedValue(distanceSquared, minSqrDistance, maxSqrDistance);
                }
            }

            // ============================================
            
            SetOpacity();
        }
        
        private void SetOpacity()
        {
            if (usingFadeGroup)
            {
                fadeOverride = fadeGroup.fadeOverride;
                
                fadeGroup.SetOpacity(opacity);
            }

            else
            {
                FadeMaterials(opacity);
            }
        }

        public void FadeMaterials(float value) // called internally and by ObjectFadeGroup.cs
        {
            float fadeValue = 1.0f - value;

            fadeValue = Mathf.Max(fadeValue, fadeOverride);
            
            fadeValue = (fadeValue > maxFade) ? maxFade : fadeValue;
            
            // ============================================
            
            for (int i = 0; i < materialCount; i ++)
            {
                materials[i].SetFloat(hashFade, fadeValue);
            }

            // ============================================
            
            if (usingRenderers && value <= 0f && maxFade >= 1.0f)
            {
                ShowRenderers(false);
            }

            else if (!usingRenderers && value > 0f)
            {
                ShowRenderers(true);
            }
        }

        private void ShowRenderers(bool value)
        {
            int listCount = renderers.Count;

            for (int i = 0; i < listCount; i ++)
            {
                renderers[i].enabled = value;
            }

            usingRenderers = value;
        }

        private float NormalizedValue(float value, float min, float max)
        {
            float normalizedValue = (value - min) / (max - min);

            return normalizedValue;
        }
        
        public void SetActive(bool value, bool useDistance)
        {
            updateOpacity = value && useDistance;

            if (!value && useDistance)
            {
                opacity = 1.0f;

                FadeMaterials(opacity);
            }

            // ============================================

            if (!usingFadeGroup)
            {
                ShowStaticMesh(!value);
            }
        }

        public void ShowStaticMesh(bool value) // called internally and by ObjectFadeGroup.cs
        {
            if (staticMesh != null)
            {
                staticMesh.enabled = value;

                int listCount = renderers.Count;

                for (int i = 0; i < listCount; i ++)
                {
                    renderers[i].gameObject.SetActive(!value);
                }
            }
        }

        public void SetCameraCollider(CameraCollider cameraCollider)
        {
            this.cameraCollider = cameraCollider;

            cameraTransform = cameraCollider.transform;
        }

        private void FindLayer()
        {
            int objectFadeLayer = LayerMask.NameToLayer("Object Fade");

            if (objectFadeLayer == -1)
            objectFadeLayer = 0;

            gameObject.layer = objectFadeLayer;
        }

        public void PreserveShadows()
        {
            int listCount = renderers.Count;

            if (listCount > 0)
            {
                for (int i = 0; i < listCount; i ++)
                {
                    CreateShadow(renderers[i]);
                }
            }

            else
            {
                CreateShadow(gameObject.GetComponent<Renderer>());
            }
        }

        private void CreateShadow(Renderer renderer)
        {
            #if UNITY_EDITOR

            GameObject newObject = Instantiate(renderer.gameObject, renderer.transform);

            newObject.name = "Shadow";

            newObject.transform.localPosition = Vector3.zero;
            newObject.transform.localEulerAngles = Vector3.zero;

            newObject.transform.localScale = Vector3.one;

            // ============================================

            int childCount = newObject.transform.childCount;

            for (int i = 0; i < childCount; i ++)
            {
                GameObject childObject = newObject.transform.GetChild(0).gameObject;

                DestroyImmediate(childObject);
            }

            // ============================================

            Renderer shadowRenderer = newObject.transform.GetComponent<Renderer>();

            shadowRenderer.shadowCastingMode = ShadowCastingMode.ShadowsOnly;

            renderer.shadowCastingMode = ShadowCastingMode.Off;

            // ============================================

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            Material defaultMaterial = cube.GetComponent<Renderer>().sharedMaterial;

            Material[] sharedMaterials = shadowRenderer.sharedMaterials;

            int materialCount = sharedMaterials.Length;

            for (int i = 0; i < materialCount; i ++)
            {
                sharedMaterials[i] = defaultMaterial;
            }

            shadowRenderer.sharedMaterials = sharedMaterials;

            DestroyImmediate(cube);

            // ============================================

            ObjectFade objectFade = newObject.GetComponent<ObjectFade>();

            if (objectFade != null)
            DestroyImmediate(objectFade);

            Collider collider = newObject.GetComponent<Collider>();

            if (collider != null)
            DestroyImmediate(collider);

            // ============================================

            int shadowCount = 0;

            childCount = renderer.transform.childCount;

            for (int i = 0; i < childCount; i ++)
            {
                string childName = renderer.transform.GetChild(i).gameObject.name;

                if (childName == "Shadow")
                shadowCount ++;
            }

            if (shadowCount > 1)
            {
                string logA = "There are two or more child objects named <color=#80E7FF>Shadow</color> in <color=#80E7FF>" + renderer.gameObject.name + "</color>";
                
                string logB = ". It is advisable to remove duplicate shadows to save rendering performance.";

                Debug.Log(logA + logB);
            }

            #endif
        }

        public ObjectFadeGroup GetFadeGroup()
        {
            return fadeGroup;
        }

        public void SetFadeGroup(ObjectFadeGroup fadeGroup) // called by ObjectFadeGroup.cs
        {
            this.fadeGroup = fadeGroup;

            usingFadeGroup = true;
        }

        public bool IsUsingFadeGroup() // called by CameraCollider.cs
        {
            return usingFadeGroup;
        }
        
        public void Fade(int sign)
        {
            if (!usingFadeGroup)
            {
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                    
                    fadeCoroutine = null;
                }
                
                fadeCoroutine = IE_Fade(sign);
                
                StartCoroutine(fadeCoroutine);
            }
        }
        
        private IEnumerator IE_Fade(int sign)
        {
            float progress = AnimationCurveProgress(opacity);
            
            sign = (sign >= 0) ? 1 : -1;
            
            // ============================================
            
            bool looping = true;
            
            while (looping)
            {
                progress += Time.deltaTime / 0.25f * sign;
                
                if (sign > 0 && progress >= 1.0f)
                {
                    progress = 1.0f;
                    
                    looping = false;
                }
                
                else if (sign < 0 && progress <= 0f)
                {
                    progress = 0f;
                    
                    looping = false;
                }
                
                opacity = AnimationCurveValue(progress);
                
                SetOpacity();
                
                yield return null;
            }
        }
        
        public static float AnimationCurveValue(float progress)
        {
            float value = 0.5f * (1.0f - Mathf.Cos(Mathf.PI * progress));
            
            return value;
        }
        
        public static float AnimationCurveProgress(float value)
        {
            float progress = Mathf.Acos(1.0f - (value / 0.5f)) / Mathf.PI;
            
            return progress;
        }
    }
}




