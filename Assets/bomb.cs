public class bomb : Collectable {
	protected override void OnRabitHit (HeroRabbit rabit)
	{
		LevelInfo.current.onRabbitDeath (rabit);
		this.CollectedHide ();
	}
}