public interface IVatService
{
	/// <summary>
	/// Calculates the VAT of a product and returns the price without the calculated VAT.
	/// </summary>
	/// <param name="priceWithVat">A price that has the VAT included.</param>
	/// <returns>A price without the calculated VAT</returns>
	double CalculatePriceWithoutVat(double priceWithVat);
}