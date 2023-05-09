using UnityEngine;

public class HumanoidAnimationEvents : MonoBehaviour
{
    public Attack attackScript;
    
    public void BeginAttack() => attackScript.Begin();
    public void EndAttack() => attackScript.End();
}
