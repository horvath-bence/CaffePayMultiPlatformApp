using Microsoft.Maui.Storage;
using System.Text.Json;
using System.IO;

namespace CaffePayMultiPlatformApp;

public partial class CoffeePay : ContentPage
{
	string fileName = "CaffePayApp";

	Prices Prices = new Prices
	{
		Credits = 1000,
		CoffeePrice = 150
	};
	
	public CoffeePay()
	{
		InitializeComponent();
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
		ReadandDeserialize();
		if(Prices.Credits-Prices.CoffeePrice >= 0)
		{
			Prices.Credits -= Prices.CoffeePrice;

			CreditsLb.Text = $"You have {Prices.Credits}Ft for coffee.";
		}
		else
		{
			CreditsLb.Text = $"Now you don't have enough credit to pay for a coffee.\nYou have {Prices.Credits}Ft";
            SerializeSave();
        }

        SemanticScreenReader.Announce(CreditsLb.Text);
		SerializeSave();

    }
}