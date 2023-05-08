using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public Attack attackScript;
    
    public void BeginAttack() => attackScript.Begin();
    public void EndAttack() => attackScript.End();
}
