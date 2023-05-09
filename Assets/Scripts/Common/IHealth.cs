using UnityEngine.Events;

public interface IHealth
{
    /// <summary>Used in <see cref="WaveManager"/> to check for enemies left in each wave.</summary>
    public event UnityAction<IHealth> Dead;
    
    public void ChangeHealth(int healthChange);
}