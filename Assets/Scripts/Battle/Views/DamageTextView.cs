using UnityEngine;
using System.Collections;

public class DamageTextView : View {
    
    public TextMesh textMesh;
    public Color damageColor;
    public Color healColor;
    float amount;
    public bool crit = false;

	public void SetResult (AttackResult result) {
        amount = result.delta;
        crit = (result.type == AttackResult.Type.CriticalHit);
        
        if (amount > 0) {
            textMesh.color = healColor;
        } else {
            textMesh.color = damageColor;
        }
        
        textMesh.text = string.Format("{0:0}", Mathf.Abs(amount));
    }
    
}
