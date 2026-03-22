using UnityEngine;
using UnityEngine.Rendering;

using System.Collections;
using System.Collections.Generic;

namespace Ifooboo
{
    public class ObjectFadeGroup : MonoBehaviour
    {
        public List<ObjectFade> objects = new List<ObjectFade>();
        
        [Range(0f, 1.0f)]
        public float fadeOverride;
        
        [Range(0f, 1.0f)]
        public float maxFade = 1.0f;

        private List<ObjectFade> activeObjects = new List<ObjectFade>();

        private int objectCount;

        private float opacity = 1.0f;
        
        private bool active;
        
        private bool updateOpacity;
        
        private IEnumerator fadeCoroutine;

        private void Awake()
        {
            int listCount = objects.Count;

            for (int i = 0; i < listCount; i ++)
            {
                objects[i].SetFadeGroup(this);
            }

            objectCount = listCount;

            // ============================================

            activeObjects.Clear();
        }

        private void OnValidate()
        {
            FadeObjects();

            opacity = 1.0f;
        }

        private void Update()
        {
            if (updateOpacity)
            {
                FadeObjects();

                opacity = 1.0f;
            }
        }

        private void FadeObjects()
        {
            for (int i = 0; i < objectCount; i ++)
            {
                objects[i].fadeOverride = fadeOverride;

                objects[i].FadeMaterials(opacity);
            }
        }

        public void SetOpacity(float value) // called by ObjectFade.cs
        {
            if (value < opacity)
            {
                opacity = value;
            }
        }

        public void SetActive(bool value, bool useDistance, ObjectFade objectFade)
        {
            if (value)
            {
                if (!useDistance && !active)
                {
                    active = true;
                    
                    Fade(-1);
                }
                
                // ============================================
                
                updateOpacity = true && useDistance;
                
                ShowStaticMesh(false);

                if (!activeObjects.Contains(objectFade))
                {
                    if (activeObjects.Count == 0)
                    {
                        for (int i = 0; i < objectCount; i ++)
                        {
                            objects[i].SetActive(true, useDistance);
                        }
                    }

                    activeObjects.Add(objectFade);
                }
            }

            else
            {
                activeObjects.Remove(objectFade);

                if (activeObjects.Count == 0)
                {
                    if (!useDistance && active)
                    {
                        active = false;
                        
                        Fade(1);
                    }
                    
                    // ============================================
                    
                    updateOpacity = false;
                    
                    ShowStaticMesh(true);

                    opacity = useDistance ? 1.0f : opacity;

                    for (int i = 0; i < objectCount; i ++)
                    {
                        objects[i].SetActive(false, useDistance);
                    }
                }
            }
        }

        private void ShowStaticMesh(bool value)
        {
            for (int i = 0; i < objectCount; i ++)
            {
                objects[i].ShowStaticMesh(value);
            }
        }
        
        private void Fade(int sign)
        {
            if (!updateOpacity)
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
            float progress = ObjectFade.AnimationCurveProgress(opacity);
            
            sign = (sign >= 0) ? 1 : -1;
            
            // ============================================
            
            bool looping = true;
            
            while (looping)
            {
                progress += Time.deltaTime / 0.2f * sign;
                
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
                
                opacity = ObjectFade.AnimationCurveValue(progress);
                
                FadeObjects();
                
                yield return null;
            }
        }
    }
}




