public class fruit : Collectable
{
	protected override void OnRabitHit(HeroRabbit rabit)
	{
		LevelInfo.current.addFruits(1);
		this.CollectedHide();
	}
}