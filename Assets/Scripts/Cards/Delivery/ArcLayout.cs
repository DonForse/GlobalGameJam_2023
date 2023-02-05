using System;
using UnityEngine.UI;
using UnityEngine;

namespace Features.Common.Utilities
{
    [ExecuteAlways]
    public class ArcLayout : LayoutGroup
    {
        [SerializeField] private Vector2 pivot;
        [SerializeField] private float angle;
        //[SerializeField] private float arc;
        //[SerializeField] private bool automaticAngle;
        [SerializeField] private float spreadWidth;
        [SerializeField] private float spreadHeight;

        protected ArcLayout()
        { }

        public override void CalculateLayoutInputVertical()
        {
        }

        public override void SetLayoutHorizontal()
        {
            var halfAngle = angle / 2;
            var angleDif = angle / (rectChildren.Count - 1);
            var halfChilds = (rectChildren.Count - 1) / 2f;
            if (rectChildren.Count == 1)
            {
                rectChildren[0].position = new Vector3(pivot.x, this.rectTransform.position.y, this.rectTransform.position.z);
                rectChildren[0].eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                for (int i = 0; i < rectChildren.Count; i++)
                {
                    rectChildren[i].position = new Vector3(((halfChilds - i) * spreadWidth) + pivot.x, this.rectTransform.position.y, this.rectTransform.position.z);
                    rectChildren[i].eulerAngles = new Vector3(0f, 0f, -halfAngle + (angleDif * i));

                }
            }
        }

        public override void SetLayoutVertical()
        {
            var pairChilds = rectChildren.Count % 2 == 0;
            var height = 0f;
            if (pairChilds)
            {
                CalculatePairChildHeight();
                return;
            }
            CalculateUnevenChildHeight();   
        }

        private void CalculateUnevenChildHeight()
        {
            float height;
            var halfChild = (rectChildren.Count - 1) / 2;
            for (var i = 0; i < halfChild; i++)
            {
                height = 0 + spreadHeight * (i / (float)halfChild);

                rectChildren[i].position =
                    new Vector3(rectChildren[i].position.x,
                        this.transform.position.y + height+ pivot.y,
                        rectChildren[i].position.z);
            }

            height = spreadHeight;
            rectChildren[halfChild].position =
                new Vector3(rectChildren[halfChild].position.x,
                    this.transform.position.y + height+ pivot.y,
                    rectChildren[halfChild].position.z);

            for (var i = halfChild + 1; i < rectChildren.Count; i++)
            {
                height = spreadHeight - ((i - halfChild) / (float)halfChild) * spreadHeight;
                rectChildren[i].position =
                    new Vector3(rectChildren[i].position.x,
                        this.transform.position.y + height+ pivot.y,
                        rectChildren[i].position.z);
            }
        }

        private void CalculatePairChildHeight()
        {
            float height;
            var halfChild = (rectChildren.Count / 2);
            for (var i = 0; i < halfChild; i++)
            {
                height = 0 + spreadHeight * ((i+1) / (float)halfChild);
                
                rectChildren[i].position =
                    new Vector3(rectChildren[i].position.x,
                        this.transform.position.y + height+ pivot.y,
                        rectChildren[i].position.z);
            }

            for (var i =halfChild; i < rectChildren.Count; i++)
            {
                height = spreadHeight - ((i-halfChild) / (float)halfChild) * spreadHeight;
                rectChildren[i].position =
                    new Vector3(rectChildren[i].position.x,
                        this.transform.position.y + height + pivot.y,
                        rectChildren[i].position.z);
            }
        }
    }
}