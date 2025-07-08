# T2 Lifestyle Checker - API

## Mike Speight

### Basic Info

This is a .Net Core API built using FastEndpoints for the T2 Lifestyle Checker Tech Test.

This is built on .Net 8 and FastEndpoints.

The API has two endpoints:

- **getpatientdetails**, which takes NHS Number, surname and date of birth, calls out to the AireLogic provided API and then validates the provided data against the data retrieved from the API and returns a status code to the UI.

- **generaterisk**, which takes the responses to the health questions that are presented in the UI once the user has been verified and returns a risk score.

The API call to the AireLogic API is made from this API rather than directly from the UI to avoid exposing a matched response based purely on an NHS number, as if a valid number is entered, in dev tools it will be possible to get name and date of birth of patients from that.

CORS is implemented in the code but set to a wildcard for ease of use in this example scenario.

Swagger documentation is here:

https://localhost:7140/swagger/index.html

### API Key

The API key required by the AireLogic API is retrieved from an environment variable called API_SUBSCRIPTION_KEY, setup on the local machine to simulate a keyvault or similar secret store that would be implemented on the chosen hosting platform and to avoid leaving sensitive information in config files.

### Scoring Mechanism

The code to generate the risk score was implemented within the API to help facilitate the possibility of allowing the scoring matrix to be updated without requiring a new build and release of the code as it would allow for the data to be stored in and retrieved from a data store of some kind, a document database, a relational database or even an API.

### Future Improvements

- logging has not been implemented, but for a production system this is certainly something I would look to put in place, certainly for unmatched or not found requests - though care would need to be taken not to log any PII or at least redact it before it gets written to any logs.
- better Swagger documentation
- secure API with a JWT or other security
