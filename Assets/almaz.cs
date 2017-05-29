public class almaz : Collectable
{
	protected override void OnRabitHit(HeroRabbit rabit)
	{
		LevelInfo.current.addAlmaz(1);
		this.CollectedHide();
	}
}