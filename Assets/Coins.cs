public class Coins : Collectable {
	protected override void OnRabitHit (HeroRabbit rabit)
	{
		LevelInfo.current.addCoins(1);
		this.CollectedHide ();
	}
}