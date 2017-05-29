
public class mushrooms : Collectable
{
	protected override void OnRabitHit(HeroRabbit rabit)
	{
		rabit.makeBigger();
		this.CollectedHide();
	}
}