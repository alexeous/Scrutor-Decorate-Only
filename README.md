# About this fork

This is a stripped-down version of [Scrutor](https://github.com/khellang/Scrutor), containing only decoration functionality.

Package dependencies except `Microsoft.Extensions.DependencyInjection.Abstractions` have been cut down. The only targeted framework version is .NET Standard 2.0.

## Usage

The library adds a family of `Decorate` extension methods to `IServiceCollection` which is used to decorate already registered services.

### Example

```csharp
var collection = new ServiceCollection();

// First, add our service to the collection.
collection.AddSingleton<IDecoratedService, Decorated>();

// Then, decorate Decorated with the Decorator type.
collection.Decorate<IDecoratedService, Decorator>();

// Finally, decorate Decorator with the OtherDecorator type.
// As you can see, OtherDecorator requires a separate service, IService. We can get that from the provider argument.
collection.Decorate<IDecoratedService>((inner, provider) => new OtherDecorator(inner, provider.GetRequiredService<IService>()));

var serviceProvider = collection.BuildServiceProvider();

// When we resolve the IDecoratedService service, we'll get the following structure:
// OtherDecorator -> Decorator -> Decorated
var instance = serviceProvider.GetRequiredService<IDecoratedService>();
```
