public class almaz : Collectable
{
	string spriteName;
	protected override void OnRabitHit(HeroRabbit rabit)
	{
		crystalPanel.crystals.Crystals(spriteName);
		//LevelInfo.current.addAlmaz(1);
		this.CollectedHide();
	}
}