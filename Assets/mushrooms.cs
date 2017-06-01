
public class mushrooms : Collectable
{
	protected override void OnRabitHit(HeroRabbit rabit)
	{
		rabit.addHealth (1);
		rabit.makeBigger();
		this.CollectedHide();
	}
}