using UnityEngine;
using System.Collections;

public class DamageTextView : View {
    
    public TextMesh textMesh;
    public Color damageColor;
    public Color healColor;
    float amount;

	public void SetAmount (float _amount) {
        amount = _amount;
        if (amount > 0) {
            textMesh.color = healColor;
        } else {
            textMesh.color = damageColor;
        }
        
        textMesh.text = string.Format("{0:0}", Mathf.Abs(amount));
    }
    
}
