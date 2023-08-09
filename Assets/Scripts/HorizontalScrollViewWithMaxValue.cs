using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalScrollViewWithMaxValue : MonoBehaviour
{
    private RectTransform m_RectTransform;
    public RectTransform content;
    public float minWidth = 100.0f;
    private float m_maxWidth;
    private float m_parentWidth;
    private List<RectTransform> siblingTransforms = new List<RectTransform>();

    // Start is called before the first frame update
    void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_parentWidth = m_RectTransform.parent.GetComponent<RectTransform>().rect.width;
        for(int i = m_RectTransform.parent.childCount-1;i>0;i--)
        {
            RectTransform child = m_RectTransform.parent.GetChild(i).GetComponent<RectTransform>();
            if(child == m_RectTransform) continue;
            siblingTransforms.Add(child);
        }        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float siblingWidth = 0.0f;
        foreach (RectTransform child in siblingTransforms)
        {
            siblingWidth += child.gameObject.activeInHierarchy?child.rect.width:0.0f;
        }
        m_maxWidth = Mathf.Max(m_parentWidth - siblingWidth, minWidth);

        // Debug.Log(name + ": ParentWidth = " + m_parentWidth + "; MaxWidth = " + m_maxWidth + "; ContentWidth = " + content.rect.width + ";SizeDelta.x = " + m_RectTransform.sizeDelta.x);

        float width = (content.rect.width > m_maxWidth) ? m_maxWidth : content.rect.width;
        m_RectTransform.sizeDelta = new Vector2(width, m_RectTransform.sizeDelta.y);
    }
}
