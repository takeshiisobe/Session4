using ServiceReference1;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace Session4
{
    [TestClass]
    public class CountryInfoServiceTests
    {
        public readonly ServiceReference1.CountryInfoServiceSoapTypeClient countryInfo =
            new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        public void ListOfCountryNamesByCode()
        {
            var list = countryInfo.ListOfCountryNamesByCode();
            var sorted = list.OrderBy(a => a.sISOCode);
            Assert.IsTrue(list.SequenceEqual(sorted), "Country is not sorted");
            
        }

        [TestMethod]
        public void InvalidCountryCode()
        {
            var countryCode = "ABC";
            var response = countryInfo.CountryName(countryCode);
            Assert.IsTrue(response.Contains("Country not found in the database"), $"Country code can be found in the database.");
        }

        [TestMethod]
        public void LastEntry()
        {
            var countryList = countryInfo.ListOfCountryNamesByCode();
            var lastEntry = countryList.Last();
            var country = countryInfo.CountryName(lastEntry.sISOCode);
            Assert.AreEqual(lastEntry.sName, country, "Not The same");
        }
    }
}