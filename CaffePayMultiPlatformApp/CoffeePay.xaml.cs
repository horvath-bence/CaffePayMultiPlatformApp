using Microsoft.Maui.Storage;
using System.Text.Json;

namespace CaffePayMultiPlatformApp;

public partial class CoffeePay : ContentPage
{
	string fileName = "CaffePayApp.json";

	Prices Prices;

	int credits = 0;
	int coffeePrice = 0;
	
	public CoffeePay()
	{
		InitializeComponent();
        credits = Preferences.Get("Credits", 1000);
        CreditsLb.Text = $"You have {credits}Ft for coffee.";
        SemanticScreenReader.Announce(CreditsLb.Text);
    }

    // Serialize and save
	public void SerializeSave()
	{
		try 
		{
            //TODO
			if(Prices.Credits == 0 && Prices.CoffeePrice == 0)
			{
				Prices.Credits = 1000;
				Prices.CoffeePrice = 150;
			}
			
			var serializedData = JsonSerializer.Serialize(Prices);
            File.WriteAllText(fileName, serializedData);
        }
		catch (Exception ex)
		{

		}


	}


    // Read and deserialize
	private void ReadandDeserialize()
	{
		try
		{
            var rawData = File.ReadAllText(fileName);
            Prices = JsonSerializer.Deserialize<Prices>(rawData);
        }
		catch(Exception e)
		{
			
		}
		//return Prices;
	}


	private void PayOneCoffee(object sender, EventArgs e)
	{
		/*ReadandDeserialize();
		if(Prices == null)
		{
            Prices = new Prices
            {
                Credits = 1000,
                CoffeePrice = 150
            };
        }
		if(Prices?.Credits-Prices?.CoffeePrice >= Prices?.CoffeePrice)
		{
			Prices.Credits -= Prices.CoffeePrice;

			CreditsLb.Text = $"You have {Prices.Credits}Ft for coffee.";
		}
		else
		{
			CreditsLb.Text = $"Now you don't have enough credit to pay for a coffee.\nYou have {Prices.Credits}Ft";
			Prices.Credits = 1000;
            Prices.CoffeePrice = 150;
            
        }

        SemanticScreenReader.Announce(CreditsLb.Text);
		SerializeSave();
		*/

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
}