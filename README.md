# Building-and-Securing-RESTful-APIs-in-ASP.NET-Core
Building and Securing RESTful APIs in ASP.NET Core

Its important to know order of methods in configureservices does not matter but order of call in configure method does matter, because presents order pipeline of request.


## How to use Swagger

Install the nuget package and add the following code in you Start.cs file.
<pre>
app.UseSwaggerUi3WithApiExplorer(options =>
                {
                    options.GeneratorSettings
                    .DefaultPropertyNameHandling
                    = NJsonSchema.PropertyNameHandling.CamelCase;
                });
</pre>