public class bomb : Collectable {
	protected override void OnRabitHit (HeroRabbit rabit)
	{
		rabit.removeHealth (1);
		this.CollectedHide ();
	}
}