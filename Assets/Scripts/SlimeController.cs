/// <summary>
/// Class to manage the Slime enemy.
/// </summary>
public class SlimeController : EnemyController {
    /// <summary>
    /// Constructor for the SlimeController class.
    /// </summary>
    public SlimeController() : base() {
        attackRange = 0.5f;
        speed = 0.75f;
    }
}
