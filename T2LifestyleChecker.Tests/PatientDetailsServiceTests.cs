using System.Net;
using System.Net.Http.Json;
using T2LifestyleChecker.Service;
using T2LifestyleChecker.Utils;
using Moq;
using Moq.Protected;

[TestClass]
public class PatientDetailsServiceTests
{
    [TestMethod]
    public async Task GetPatientDetails_ReturnsMatched_WhenPatientDataIsCorrect()
    {
        // Arrange
        var nhsNumber = 111222333;
        var surname = "Doe";
        var dateOfBirth = new DateOnly(2007, 1, 14);

        var patientDetails = new PatientDetails
        {
            nhsNumber = nhsNumber.ToString(),
            name = "DOE,John",
            born = "2007-01-14"
        };

        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(patientDetails)
        };

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        var httpClient = new HttpClient(handlerMock.Object);

        Environment.SetEnvironmentVariable("API_SUBSCRIPTION_KEY", "test-key");

        var service = new PatientDetailsService(httpClient);

        // Act
        var result = await service.GetPatientDetails("http://testapi.com/patient/", nhsNumber, surname, dateOfBirth);

        // Assert
        Assert.AreEqual(Constants.Matched, result);
    }

    [TestMethod]
    public async Task GetPatientDetails_ReturnsNotMatched_WhenPatientDataAreInconsistent()
    {
        // Arrange
        var nhsNumber = 111222333;
        var surname = "May";
        var dateOfBirth = new DateOnly(2007, 1, 14);

        var patientDetails = new PatientDetails
        {
            nhsNumber = nhsNumber.ToString(),
            name = "DOE,John",
            born = "2007-01-14"
        };

        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(patientDetails)
        };

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        var httpClient = new HttpClient(handlerMock.Object);

        Environment.SetEnvironmentVariable("API_SUBSCRIPTION_KEY", "test-key");

        var service = new PatientDetailsService(httpClient);

        // Act
        var result = await service.GetPatientDetails("http://testapi.com/patient/", nhsNumber, surname, dateOfBirth);

        // Assert
        Assert.AreEqual(Constants.NotMatched, result);
    }

    [TestMethod]
    public async Task GetPatientDetails_ReturnsNotEligible_WhenPatientAgeIsLessThanSixteen()
    {
        // Arrange
        var nhsNumber = 555666777;
        var surname = "May";
        var dateOfBirth = new DateOnly(2007, 1, 14);

        var patientDetails = new PatientDetails
        {
            nhsNumber = nhsNumber.ToString(),
            name = "MAY, Megan",
            born = "2010-11-14"
        };

        var mockResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = JsonContent.Create(patientDetails)
        };

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        var httpClient = new HttpClient(handlerMock.Object);

        Environment.SetEnvironmentVariable("API_SUBSCRIPTION_KEY", "test-key");

        var service = new PatientDetailsService(httpClient);

        // Act
        var result = await service.GetPatientDetails("http://testapi.com/patient/", nhsNumber, surname, dateOfBirth);

        // Assert
        Assert.AreEqual(Constants.NotEligible, result);
    }

    [TestMethod]
    public async Task GetPatientDetails_ReturnsNotFound_WhenPatientNumberNotFound()
    {
        // Arrange
        var nhsNumber = 555666771;
        var surname = "May";
        var dateOfBirth = new DateOnly(2007, 1, 14);

        var mockResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
        {};

        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        var httpClient = new HttpClient(handlerMock.Object);

        Environment.SetEnvironmentVariable("API_SUBSCRIPTION_KEY", "test-key");

        var service = new PatientDetailsService(httpClient);

        // Act
        var result = await service.GetPatientDetails("http://testapi.com/patient/", nhsNumber, surname, dateOfBirth);

        // Assert
        Assert.AreEqual(Constants.NotFound, result);
    }

}