# CurrencyRatesAPIAssignment

This assignment is an implementation of a REST service that returns historical exchange rate information. The service uses a free API provided by the ECB as its data source. This API is documented at https://exchangeratesapi.io/ .

The service exposes an endpoint that accepts this input: set of dates, base currency and target currency.

The service returns this information: maximum exchange rate during the period, minimum exchange rate during the period and average exchange rate during the period.

Example usage and return values:

Given this input

· Dates: 2018-02-01, 2018-02-15, 2018-03-01

· Currency SEK->NOK

Where query string in URL is defined as 

https://localhost:44334/rate/output?dateslist=2018-02-01,2018-02-15,2018-03-01&basecurrency=SEK&targetcurrency=NOK

The service returns this information:

· A min rate of 0.9546869595 on 2018-03-01

· A max rate of 0.9815486993 on 2018-02-15

· An average rate of 0.970839476467

The service is implemented as .NET Core Web Api project and it can be easily run by building and running CurrencyRatesAPI.sln in Visual Studio.

Initial route when running solution is default controller route "/rate" and it shows latest foreign exchange reference rates. 
Route for calling implemented REST service with defined parameters (list of dates, base currency and target currency) is "/rate/output?" followed by specified query string.
