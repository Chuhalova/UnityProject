public class almaz : Collectable
{
	string spriteName;
	protected override void OnRabitHit(HeroRabbit rabit)
	{
		crystalPanel.crystals.Crystals(this.name);
		//LevelInfo.current.addAlmaz(1);
		this.CollectedHide();
	}
}