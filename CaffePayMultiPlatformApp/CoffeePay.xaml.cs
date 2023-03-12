using Microsoft.Maui.Storage;
using System.Text.Json;

namespace CaffePayMultiPlatformApp;

public partial class CoffeePay : ContentPage
{
	int credits = 0;
	int coffeePrice = 0;
	
	public CoffeePay()
	{
		InitializeComponent();
        credits = Preferences.Get("Credits", 1000);
        CreditsLb.Text = $"You have {credits}Ft for coffee.";
        SemanticScreenReader.Announce(CreditsLb.Text);
    }

	private void PayOneCoffee(object sender, EventArgs e)
	{
		if(!Preferences.ContainsKey("Credits") && !Preferences.ContainsKey("CoffeePrice"))
		{
            credits = 1000;
            coffeePrice = 150;
            Preferences.Set("Credits", credits);
            Preferences.Set("CoffeePrice", coffeePrice);
		}

		credits = Preferences.Get("Credits", 0);
		coffeePrice = Preferences.Get("CoffeePrice", 0);

		if(credits == 0 && coffeePrice == 0)
		{
			credits = 1000;
			coffeePrice = 150;
			Preferences.Set("Credits", credits);
			Preferences.Set("CoffeePrice", coffeePrice);
		}

		if(credits >= coffeePrice)
		{
			credits-= coffeePrice;
            CreditsLb.Text = $"Now you have {credits}Ft for coffee.";
            Preferences.Set("Credits", credits);
        }
		else
		{
            CreditsLb.Text = $"Now you don't have enough credit to pay for a coffee.\nYou have {credits}Ft and one coffee is {coffeePrice}Ft";
            credits = 1000;
            coffeePrice = 150;
            Preferences.Set("Credits", credits);
            Preferences.Set("CoffeePrice", coffeePrice);
        }

        SemanticScreenReader.Announce(CreditsLb.Text);

    }

    private void AddCredits(object sender, EventArgs e)
    {

    }
}