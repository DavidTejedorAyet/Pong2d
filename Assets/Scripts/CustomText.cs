using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomText : MonoBehaviour {

    private TextMeshProUGUI textMesh;
    private Animator animator;
    private void Awake() {
        textMesh = GetComponent<TextMeshProUGUI>();
        animator = GetComponent<Animator>();
    }

    public void setText(string text, bool withAnimation = false) {
        textMesh.text = text;
        
        if (withAnimation) {
            animator.SetTrigger("Change");
        }
    }

    public void setColor(Color color) { 
        textMesh.color = color;
    }
}
